namespace TelecomServiceSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Data.Bills;
    using TelecomServiceSystem.Web.ViewModels.Invoices;

    public class InvoicesController : BaseController
    {
        private readonly IBillsService billsService;

        public InvoicesController(IBillsService billsService)
        {
            this.billsService = billsService;
        }

        public async Task<IActionResult> All(string customerId)
        {
            var model = await this.billsService.GetAllByCustomerIdAsync<InvoiceViewModel>(customerId);
            return this.View(model);
        }
    }
}
