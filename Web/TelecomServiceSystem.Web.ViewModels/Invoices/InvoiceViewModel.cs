namespace TelecomServiceSystem.Web.ViewModels.Invoices
{
    using System;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class InvoiceViewModel : IMapFrom<Bill>
    {
        public DateTime CreatedOn { get; set; }

        public string Url { get; set; }
    }
}
