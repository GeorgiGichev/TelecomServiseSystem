namespace TelecomServiceSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Data.Orders;
    using TelecomServiceSystem.Web.ViewModels.Orders;

    public class OrdersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderService orderService;

        public OrdersController(UserManager<ApplicationUser> userManager, IOrderService orderService)
        {
            this.userManager = userManager;
            this.orderService = orderService;
        }

        public IActionResult ChooseServiceType(string customerId)
        {
            var model = new ChooseServiceViewModel { CustomerId = customerId };
            return this.View(model);
        }

        //[HttpPost]
        //public IActionResult ChooseServiceType(ChooseServiceViewModel model)
        //{

        //    return this.View(model);
        //}

        public IActionResult Create(string customerId, string serviceType)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            //await this.orderService.CreateOrderAsync();
            return this.View();
        }
    }
}
