namespace TelecomServiceSystem.Web.ViewModels.Customers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Web.ViewModels.Addresses;

    public class CustomerEditViewModel : IMapTo<Customer>, IMapFrom<Customer>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [MaxLength(20)]
        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        public int DocumentType { get; set; }

        [Required]
        [MinLength(5)]
        [Display(Name = "Personal Number")]
        public string PersonalNumber { get; set; }

        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public ICollection<CustomerAddressViewModel> Addresses { get; set; }
    }
}
