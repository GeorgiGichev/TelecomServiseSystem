namespace TelecomServiceSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Services.CloudinaryService;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Services.Data.Customers;
    using TelecomServiceSystem.Services.Data.Orders;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Services.Data.ServiceNumber;
    using TelecomServiceSystem.Services.Data.Services;
    using TelecomServiceSystem.Services.Data.Tasks;
    using TelecomServiceSystem.Services.HtmlToPDF;
    using TelecomServiceSystem.Services.ViewRrender;
    using TelecomServiceSystem.Web.Hubs;
    using TelecomServiceSystem.Web.Infrastructure.Extensions;
    using TelecomServiceSystem.Web.ViewModels.Addresses;
    using TelecomServiceSystem.Web.ViewModels.ContractTemplates;
    using TelecomServiceSystem.Web.ViewModels.Orders;
    using TelecomServiceSystem.Web.ViewModels.Orders.Search;
    using TelecomServiceSystem.Web.ViewModels.Services;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.SellerRoleName)]
    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IServiceService serviceService;
        private readonly IServiceNumberService numberService;
        private readonly IServiceInfoService serviceInfoService;
        private readonly IViewRenderService viewRenderService;
        private readonly IHtmlToPdfConverter htmlToPdfConverter;
        private readonly IWebHostEnvironment environment;
        private readonly IAddressService addressService;
        private readonly ITasksService tasksService;
        private readonly IHubContext<TaskHub, ITaskHub> context;
        private readonly ICustomerService customerService;
        private readonly IUploadService uploadService;

        public OrdersController(IOrderService orderService, IServiceService serviceService, IServiceNumberService numberService, IServiceInfoService serviceInfoService, IViewRenderService viewRenderService, IHtmlToPdfConverter htmlToPdfConverter, IWebHostEnvironment environment, IAddressService addressService, ITasksService tasksService, IHubContext<TaskHub, ITaskHub> context, ICustomerService customerService, IUploadService uploadService)
        {
            this.orderService = orderService;
            this.serviceService = serviceService;
            this.numberService = numberService;
            this.serviceInfoService = serviceInfoService;
            this.viewRenderService = viewRenderService;
            this.htmlToPdfConverter = htmlToPdfConverter;
            this.environment = environment;
            this.addressService = addressService;
            this.tasksService = tasksService;
            this.context = context;
            this.customerService = customerService;
            this.uploadService = uploadService;
        }

        public async Task<IActionResult> ChooseServiceType(string customerId)
        {
            if (!await this.customerService.ExistAsync(customerId))
            {
                return this.NotFound();
            }

            var model = new ChooseServiceViewModel { CustomerId = customerId };
            return this.View(model);
        }

        public async Task<IActionResult> GetFreeNumbersAsJson(string serviceName)
        {
            var numbers = await this.numberService.GetFreeNumbersAsync<ServiceNumberViewModel>("fix", serviceName);
            return this.Json(numbers);
        }

        public async Task<IActionResult> AddressesAsJson(string customerId)
        {
            if (!await this.customerService.ExistAsync(customerId))
            {
                return this.NotFound();
            }

            var addresses = await this.addressService.GetByCustomerIdAsync<InstalationAddressViewModel>(customerId);
            return this.Json(addresses);
        }

        public async Task<IActionResult> Create(string customerId, string serviceType)
        {
            if (!await this.customerService.ExistAsync(customerId))
            {
                return this.NotFound();
            }

            var model = new OrderInputModel
            {
                ServiceType = serviceType,
            };

            model.Services = await this.serviceService.GetServiceNamesByTypeAsync<ServiceViewModel>(serviceType);

            if (serviceType == "mobile")
            {
                model.Numbers = await this.numberService.GetFreeNumbersAsync<ServiceNumberViewModel>(serviceType, null);
                model.MobileServiceInfo = new MobileServiceInfoViewModel
                {
                    ICC = await this.serviceInfoService.GetICCAsync(),
                    CustomerId = customerId,
                };

                return this.View("Mobile", model);
            }

            model.FixedServiceInfo = new FixedServiceInfiViewModel
            {
                CustomerId = customerId,
            };

            return this.View("Fixed", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model.ServiceType.ToLower() == "mobile" ? "Mobile" : "Fixed", model);
            }

            await this.CreateOrder(model.ServiceType, model);
            string customerId = model.ServiceType == "mobile" ? model.MobileServiceInfo.CustomerId : model.FixedServiceInfo.CustomerId;
            return this.RedirectToAction("AllByCustomer", "Services", new
            {
                Id = customerId,
            });
        }

        public async Task<IActionResult> GetPdf(string customerId, string serviceType, string address, int duration, string number, string service)
        {
            var model = await this.customerService.GetByIdAsync<ContarctViewModel>(customerId);
            model.Plan = await this.serviceService.GetByNameAsync<ServiceContractViewModel>(service);
            model.ContractDuration = duration;
            model.Number = number;
            model.ServiceAddress = address;
            model.ServiceType = serviceType;
            model.UserId = this.User.GetId();
            model.Address = await this.addressService.GetMainAddressAsync<AddressViewModel>(customerId);
            var htmlData = await this.viewRenderService.RenderToStringAsync("~/Views/Orders/GetPdf.cshtml", model);
            var fileContents = this.htmlToPdfConverter.Convert(this.environment.ContentRootPath, htmlData, "A4", "Portrait");
            return this.File(fileContents, "application/pdf");
        }

        public async Task<IActionResult> Tracking()
        {
            string userId = null;
            if (this.User.IsInRole(GlobalConstants.SellerRoleName))
            {
                userId = this.User.GetId();
            }

            var model = new SearchOrderModel
            {
                Orders = await this.GetCustomersAsync(new SearchOrderInputModel
                {
                    UserId = userId,
                }),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Tracking(SearchOrderModel model)
        {
            string userId = null;
            if (this.User.IsInRole(GlobalConstants.SellerRoleName))
            {
                userId = this.User.GetId();
            }

            model.InputModel.UserId = userId;
            model.Orders = await this.GetCustomersAsync(model.InputModel);
            return this.View(model);
        }

        private async Task<HashSet<SearchOrderViewModel>> GetCustomersAsync(SearchOrderInputModel model)
            => (await this.serviceInfoService.GetBySearchCriteriaAsync<SearchOrderViewModel, SearchOrderInputModel>(model)).ToHashSet();

        private async Task CreateOrder(string serviceType, OrderInputModel model)
        {
            if (serviceType == "mobile")
            {
                model.MobileServiceInfo = await this.orderService
                    .CreateAsync<MobileServiceInfoViewModel, OrderViewModel>(
                    new OrderViewModel
                    {
                        UserId = this.User.GetId(),
                        DocumentUrl = await this.uploadService.UploadImageAsync(model.Image),
                    },
                    model.MobileServiceInfo);

                await this.orderService.FinishOrderAsync<MobileServiceInfoViewModel>(model.MobileServiceInfo);
            }
            else
            {
                model.FixedServiceInfo = await this.orderService
                    .CreateAsync<FixedServiceInfiViewModel, OrderViewModel>(
                    new OrderViewModel
                    {
                        UserId = this.User.GetId(),
                        DocumentUrl = await this.uploadService.UploadImageAsync(model.Image),
                    },
                    model.FixedServiceInfo);

                await this.tasksService.CreateAsync(model.FixedServiceInfo.OrderId, model.InstalationSlotId);
                await this.context.Clients.All.ReceiveTasksUpdate();
            }
        }
    }
}
