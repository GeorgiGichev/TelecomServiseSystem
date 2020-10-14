namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TelecomServiceSystem.Data.Common.Models;

    public class ServiceInfo : BaseDeletableModel<int>
    {
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

        [ForeignKey(nameof(ServiseNumber))]
        public int ServiceNumberId { get; set; }

        public virtual ServiseNumber ServiseNumber { get; set; }

        public string ICC { get; set; }

        public string IMEI { get; set; }

        public int ContractDuration { get; set; }

        public DateTime Expirеs { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }

        public Address Address { get; set; }
    }
}
