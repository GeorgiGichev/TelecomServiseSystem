namespace TelecomServiceSystem.Web.ViewModels.Administration.Employees
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using TelecomServiceSystem.Common.Validators;
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

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string PictureURL { get; set; }

        public DateTime CreatedOn { get; set; }

        [AllowedExtensions(new string[] { ".jpeg", ".png", ".jpg" })]
        [MaxFileSize(4 * 1024 * 1024)]
        public IFormFile NewImage { get; set; }
    }
}
