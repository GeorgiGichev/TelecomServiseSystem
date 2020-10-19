namespace TelecomServiceSystem.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class FixedServiceInfiViewModel : IMapFrom<ServiceInfo>, IMapTo<ServiceInfo>
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }

        public int ServiceId { get; set; }

        [RegularExpression(@"^12|24$", ErrorMessage = "Contract duration can be 12 or 24 months!")]
        public int ContractDuration { get; set; }

        public int ServiceNumberId { get; set; }

        public int AddressId { get; set; }

        public string OrderId { get; set; }
    }
}
