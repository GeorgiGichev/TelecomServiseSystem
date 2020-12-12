namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TelecomServiceSystem.Data.Common.Models;

    public class ServiceInfo : BaseDeletableModel<int>
    {
        public ServiceInfo()
        {
            this.Documents = new HashSet<Document>();
        }

        [Required]
        [ForeignKey(nameof(Order))]
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        [ForeignKey(nameof(ServiceNumber))]
        public int ServiceNumberId { get; set; }

        public virtual ServiceNumber ServiceNumber { get; set; }

        public string ICC { get; set; }

        public string IMEI { get; set; }

        public int ContractDuration { get; set; }

        public DateTime Expirеs { get; set; }

        public DateTime? CancellationDate { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey(nameof(Address))]
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
