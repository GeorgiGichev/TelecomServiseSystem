namespace TelecomServiceSystem.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Common.Validators;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.CloudinaryService;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Web.ViewModels.Addresses;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IAddressService addressService;
        private readonly IUploadService uploadService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IAddressService addressService,
            IUploadService uploadService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.addressService = addressService;
            this.uploadService = uploadService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.Input = new InputModel
            {
                Cities = await this.addressService.GetCitiesByCountryAsync<CityViewModel>(GlobalConstants.CountryOfUsing) as ICollection<CityViewModel>,
            };
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = this.Input.Email.Split("@")[0],
                    Email = this.Input.Email,
                    FirstName = this.Input.FirstName,
                    MiddleName = this.Input.MiddleName,
                    LastName = this.Input.LastName,
                    EGN = this.Input.EGN,
                    CityId = this.Input.CityId,
                    PictureURL = await this.uploadService.UploadImageAsync(this.Input.Image),
                    PhoneNumber = this.Input.Phone,
                };
                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(user, this.Input.Role);
                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        this.Input.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", (email: this.Input.Email, returnUrl: returnUrl));
                    }
                    else
                    {
                        if (await this.userManager.IsInRoleAsync(user, "Engineer"))
                        {
                            return this.Redirect($"/Administration/Employee/AllFreeTeams?employeeId={user.Id}&cityId={user.CityId}");
                        }

                        return this.Redirect("/Administration/Employee");
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

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
            [RegularExpression(@"^Admin$|^Seller$|^Engineer$", ErrorMessage = "Invalid role")]
            [Display(Name = "Role")]
            public string Role { get; set; }

            public int CityId { get; set; }

            public string Phone { get; set; }

            [Required]
            [AllowedExtensions(new string[] { ".jpeg", ".png", ".jpg" })]
            [MaxFileSize(4 * 1024 * 1024)]
            public IFormFile Image { get; set; }

            public ICollection<CityViewModel> Cities { get; set; } = new HashSet<CityViewModel>();
        }
    }
}
