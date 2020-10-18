namespace TelecomServiceSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Data.Orders;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Services.Data.ServiceNumber;
    using TelecomServiceSystem.Services.Data.Services;
    using TelecomServiceSystem.Web.Infrastructure.Extensions;
    using TelecomServiceSystem.Web.ViewModels.Orders;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IServiceService serviceService;
        private readonly IServiceNumberService numberService;
        private readonly IServiceInfoService serviceInfoService;

        public OrdersController(IOrderService orderService, IServiceService serviceService, IServiceNumberService numberService, IServiceInfoService serviceInfoService)
        {
            this.orderService = orderService;
            this.serviceService = serviceService;
            this.numberService = numberService;
            this.serviceInfoService = serviceInfoService;
        }

        public IActionResult ChooseServiceType(string customerId)
        {
            var model = new ChooseServiceViewModel { CustomerId = customerId };
            return this.View(model);
        }

        public async Task<IActionResult> Create(string customerId, string serviceType)
        {
            var model = new OrderInputViewModel
            {
                ServiceType = serviceType,
            };

            model.Services = await this.serviceService.GetServiceNamesByType<ServiceViewModel>(serviceType);
            model.Numbers = await this.numberService.GetFreeNumbers<ServiceNumberViewModel>(serviceType, null);

            if (serviceType == "mobile")
            {
                model.MobileServiceInfo = new MobileServiceInfoViewModel
                {
                    ICC = await this.serviceInfoService.GetICC(),
                    CustomerId = customerId,
                };
            }
            else
            {
                model.FixedServiceInfo = new FixedServiceInfiViewModel();
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderInputViewModel model)
        {
            await this.CreateOrder(model.ServiceType, model);
            return this.View();
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
            }
        }
    }
}
