namespace TelecomServiceSystem.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using TelecomServiceSystem.Common.Validators;

    public class OrderInputModel
    {
        public string OrderId { get; set; }

        public string ServiceType { get; set; }

        public int InstalationSlotId { get; set; }

        [Required]
        [AllowedExtensions(new string[] { ".jpeg", ".png", ".jpg" })]
        [MaxFileSize(4 * 1024 * 1024)]
        public IFormFile Image { get; set; }

        public MobileServiceInfoViewModel MobileServiceInfo { get; set; }

        public FixedServiceInfiViewModel FixedServiceInfo { get; set; }

        public IEnumerable<ServiceNumberViewModel> Numbers { get; set; } = new HashSet<ServiceNumberViewModel>();

        public IEnumerable<ServiceViewModel> Services { get; set; } = new HashSet<ServiceViewModel>();
    }
}
