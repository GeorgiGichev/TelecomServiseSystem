namespace TelecomServiceSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Billing;
    using TelecomServiceSystem.Services.Data.Bills;
    using TelecomServiceSystem.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IBillingService billingService;
        private readonly IBillsService billsService;

        public HomeController(IBillingService billingService, IBillsService billsService)
        {
            this.billingService = billingService;
            this.billsService = billsService;
        }

        public async Task<IActionResult> Index()
        {
            await this.billingService.CreateAsync();
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult Blazor()
        {
            return this.View("_Host");
        }
    }
}
