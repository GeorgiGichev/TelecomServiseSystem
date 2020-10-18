namespace TelecomServiceSystem.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class MobileServiceInfoViewModel : IMapFrom<ServiceInfo>, IMapTo<ServiceInfo>
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }

        public int ServiceId { get; set; }

        [Required]
        [MinLength(22 , ErrorMessage = "ICC required 22 symbols")]
        [StringLength(22)]
        public string ICC { get; set; }

        [RegularExpression(@"^12 | 24$", ErrorMessage = "Contract duration can be 12 or 24 months!")]
        public int ContractDuration { get; set; }

        [MinLength(15, ErrorMessage = "IMEI required 22 symbols")]
        [StringLength(15)]
        public string IMEI { get; set; }

        public int ServiceNumberId { get; set; }

        public string OrderId { get; set; }
    }
}
