namespace TelecomServiceSystem.Web.ViewModels.Customers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Web.ViewModels.Addresses;

    public class CustomersAddressInputModel : IMapTo<Address>, IMapFrom<Address>
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Neighborhood")]
        public string Neighborhood { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Number")]
        public int Number { get; set; }

        [MaxLength(20)]
        [Display(Name = "Entrance")]
        public string Entrance { get; set; }

        [Display(Name = "Floor")]
        public int? Floor { get; set; }

        [Display(Name = "Apartment")]
        public int? Apartment { get; set; }

        public bool IsMainAddress { get; set; }

        public string CustomerId { get; set; }

        public ICollection<CityViewModel> Cities { get; set; } = new HashSet<CityViewModel>();
    }
}
