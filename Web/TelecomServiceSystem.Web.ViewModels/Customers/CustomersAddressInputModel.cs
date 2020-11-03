namespace TelecomServiceSystem.Web.ViewModels.Customers
{
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class CustomersAddressInputModel : IMapTo<Address>, IMapFrom<Address>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Neighborhood")]
        public string Neighborhood { get; set; }

        [Display(Name = "Number")]
        public int Number { get; set; }

        [MaxLength(20)]
        [Display(Name = "Entrance")]
        public string Entrance { get; set; }

        [Display(Name = "Floor")]
        public int? Floor { get; set; }

        [Display(Name = "Apartment")]
        public int? Apartment { get; set; }

        public bool? IsMainAddress { get; set; }

        public string CustomerId { get; set; }
    }
}
