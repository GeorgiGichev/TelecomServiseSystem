namespace TelecomServiceSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Services.Data.Customers;
    using TelecomServiceSystem.Services.Data.Orders;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Services.Data.ServiceNumber;
    using TelecomServiceSystem.Services.Data.Services;
    using TelecomServiceSystem.Services.Data.Tasks;
    using TelecomServiceSystem.Services.HtmlToPDF;
    using TelecomServiceSystem.Services.ViewRrender;
    using TelecomServiceSystem.Web.Infrastructure.Extensions;
    using TelecomServiceSystem.Web.ViewModels.Addresses;
    using TelecomServiceSystem.Web.ViewModels.Orders;
    using TelecomServiceSystem.Web.ViewModels.Orders.Search;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IServiceService serviceService;
        private readonly IServiceNumberService numberService;
        private readonly IServiceInfoService serviceInfoService;
        private readonly IViewRenderService viewRenderService;
        private readonly IHtmlToPdfConverter htmlToPdfConverter;
        private readonly IWebHostEnvironment environment;
        private readonly ICustomerService customerService;
        private readonly IAddressService addressService;
        private readonly ITasksService tasksService;

        public OrdersController(IOrderService orderService, IServiceService serviceService, IServiceNumberService numberService, IServiceInfoService serviceInfoService, IViewRenderService viewRenderService, IHtmlToPdfConverter htmlToPdfConverter, IWebHostEnvironment environment, ICustomerService customerService, IAddressService addressService, ITasksService tasksService)
        {
            this.orderService = orderService;
            this.serviceService = serviceService;
            this.numberService = numberService;
            this.serviceInfoService = serviceInfoService;
            this.viewRenderService = viewRenderService;
            this.htmlToPdfConverter = htmlToPdfConverter;
            this.environment = environment;
            this.customerService = customerService;
            this.addressService = addressService;
            this.tasksService = tasksService;
        }

        public IActionResult ChooseServiceType(string customerId)
        {
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
            var addresses = await this.addressService.GetByCustomerId<InstalationAddressViewModel>(customerId);
            return this.Json(addresses);
        }

        public async Task<IActionResult> Create(string customerId, string serviceType)
        {
            var model = new OrderInputViewModel
            {
                ServiceType = serviceType,
            };

            model.Services = await this.serviceService.GetServiceNamesByType<ServiceViewModel>(serviceType);

            if (serviceType == "mobile")
            {
                model.Numbers = await this.numberService.GetFreeNumbersAsync<ServiceNumberViewModel>(serviceType, null);
                model.MobileServiceInfo = new MobileServiceInfoViewModel
                {
                    ICC = await this.serviceInfoService.GetICC(),
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
        public async Task<IActionResult> Create(OrderInputViewModel model)
        {
            await this.CreateOrder(model.ServiceType, model);
            string customerId = model.ServiceType == "mobile" ? model.MobileServiceInfo.CustomerId : model.FixedServiceInfo.CustomerId;
            return this.RedirectToAction("AllByCustomer", "Services", new
            {
                Id = customerId,
            });
        }

        public async Task<IActionResult> GetPdf(OrderViewModel input)
        {
            //var model = this.serviceInfoService.GetByOrderId(input);
            var htmlData = await this.viewRenderService.RenderToStringAsync("~/Views/Orders/GetPdf.cshtml", null);
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

        private async Task CreateOrder(string serviceType, OrderInputViewModel model)
        {
            if (serviceType == "mobile")
            {
                model.MobileServiceInfo = await this.orderService
                    .CreateAsync<MobileServiceInfoViewModel, OrderViewModel>(
                    new OrderViewModel
                    {
                        UserId = this.User.GetId(),
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
                    },
                    model.FixedServiceInfo);

                await this.tasksService.CreateAsync(model.FixedServiceInfo.OrderId, model.InstalationSlotId);
            }
        }
    }
}
