namespace TelecomServiceSystem.Services.Models.Bills
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class BillViewModel : IMapFrom<Customer>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Name => $"{this.FirstName} {this.LastName}";

        public decimal Amount => this.ServicesInfo.Sum(x => x.ServicePrice);

        public decimal VAT => this.Amount - this.AmountWithoutVAT;

        public decimal AmountWithoutVAT => this.Amount * 0.8m;

        public ICollection<BillServiceViewModel> ServicesInfo { get; set; } = new HashSet<BillServiceViewModel>();
    }
}
