namespace TelecomServiceSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Services.Billing;
    using TelecomServiceSystem.Services.Data.Bills;
    using TelecomServiceSystem.Web.ViewModels.Invoices;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.SellerRoleName)]
    public class InvoicesController : BaseController
    {
        private readonly IBillsService billsService;
        private readonly IBillingService billingService;

        public InvoicesController(IBillsService billsService, IBillingService billingService)
        {
            this.billsService = billsService;
            this.billingService = billingService;
        }

        public async Task<IActionResult> All(string customerId)
        {
            // await this.billingService.CreateAsync();
            var model = new InvoicesCustomerViewModel
            {
                CustomerId = customerId,
                Invoices = await this.billsService.GetAllByCustomerIdAsync<InvoiceViewModel>(customerId),
            };
            return this.View(model);
        }
    }
}
