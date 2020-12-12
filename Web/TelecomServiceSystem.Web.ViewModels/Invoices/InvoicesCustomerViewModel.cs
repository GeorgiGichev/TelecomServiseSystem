namespace TelecomServiceSystem.Web.ViewModels.Invoices
{
    using System.Collections.Generic;

    public class InvoicesCustomerViewModel
    {
        public string CustomerId { get; set; }

        public IEnumerable<InvoiceViewModel> Invoices { get; set; }
    }
}
