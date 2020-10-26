namespace TelecomServiceSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Common.Models;

    public class Address : BaseDeletableModel<int>
    {
        public Address()
        {
            this.ServicesInfos = new HashSet<ServiceInfo>();
        }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(100)]
        public string Neighborhood { get; set; }

        public int Number { get; set; }

        [MaxLength(20)]
        public string Entrance { get; set; }

        public int? Floor { get; set; }

        public int? Apartment { get; set; }

        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public bool IsMainAddress { get; set; } = false;

        public virtual ICollection<ServiceInfo> ServicesInfos { get; set; }
    }
}
