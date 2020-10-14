namespace TelecomServiceSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Data.Orders;
    using TelecomServiceSystem.Web.ViewModels.Orders;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult ChooseServiceType(string customerId)
        {
            var model = new ChooseServiceViewModel { CustomerId = customerId };
            return this.View(model);
        }

        public IActionResult Create(string customerId, int serviceType)
        {
            var model = new OrderInputViewModel();
            model.Services = new ServiceViewModel { ServiseType = serviceType };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            //await this.orderService.CreateOrderAsync();
            return this.View();
        }
    }
}
