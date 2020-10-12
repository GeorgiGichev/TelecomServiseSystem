namespace TelecomServiceSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Common.Models;

    public class Address : BaseDeletableModel<int>
    {
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

        public Customer Customer { get; set; }
    }
}
