namespace TelecomServiceSystem.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Services.Data.Customers;
    using TelecomServiceSystem.Services.Data.Orders;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Services.Data.ServiceNumber;
    using TelecomServiceSystem.Services.Data.Services;
    using TelecomServiceSystem.Services.HtmlToPDF;
    using TelecomServiceSystem.Services.ViewRrender;
    using TelecomServiceSystem.Web.Infrastructure.Extensions;
    using TelecomServiceSystem.Web.ViewModels.Addresses;
    using TelecomServiceSystem.Web.ViewModels.Customers;
    using TelecomServiceSystem.Web.ViewModels.Orders;

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

        public OrdersController(IOrderService orderService, IServiceService serviceService, IServiceNumberService numberService, IServiceInfoService serviceInfoService, IViewRenderService viewRenderService, IHtmlToPdfConverter htmlToPdfConverter, IWebHostEnvironment environment, ICustomerService customerService, IAddressService addressService)
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
            }
            else
            {
                model.FixedServiceInfo = new FixedServiceInfiViewModel
                {
                    CustomerId = customerId,
                };
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderInputViewModel model)
        {
            await this.CreateOrder(model.ServiceType, model);
            string customerId = model.ServiceType == "mobile" ? model.MobileServiceInfo.CustomerId : model.FixedServiceInfo.CustomerId;
            var customerModel = await this.customerService.GetByIdAsync<CustomerEditViewModel>(customerId);
            return this.RedirectToAction("Edit", "Customers", customerModel);
        }

        public async Task<IActionResult> GetPdf(OrderViewModel input)
        {
            // var model = this.serviceInfoService.GetByOrderId(input);
            var htmlData = await this.viewRenderService.RenderToStringAsync("~/Views/Orders/GetPdf.cshtml", input);
            var fileContents = this.htmlToPdfConverter.Convert(this.environment.ContentRootPath, htmlData, "A4", "Portrait");
            return this.File(fileContents, "application/pdf");
        }

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

                model.OrderId = model.MobileServiceInfo.OrderId;

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

                model.OrderId = model.FixedServiceInfo.OrderId;
            }
        }
    }
}
