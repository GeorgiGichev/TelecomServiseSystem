namespace TelecomServiceSystem.Web.ViewModels.Administration.Employees
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class EditViewModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
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
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid EGN")]
        [Display(Name = "EGN")]
        public string EGN { get; set; }

        public string PhoneNumber { get; set; }

        public string PictureURL { get; set; }

        public DateTime CreatedOn { get; set; }

        public IFormFile NewImage { get; set; }
    }
}
