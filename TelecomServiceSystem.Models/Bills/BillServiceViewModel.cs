namespace TelecomServiceSystem.Services.Models.Bills
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class BillServiceViewModel : IMapFrom<ServiceInfo>
    {
        public string ServiceName { get; set; }

        public string ServiceNumberNumber { get; set; }

        public decimal ServicePrice { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CancellationDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime Expires { get; set; }
    }
}
