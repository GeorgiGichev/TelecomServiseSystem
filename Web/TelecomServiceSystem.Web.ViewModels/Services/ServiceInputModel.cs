namespace TelecomServiceSystem.Web.ViewModels.Services
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceInputModel : IMapFrom<ServiceInfo>, IMapTo<ServiceInfo>
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public string CustomerId { get; set; }

        public int ContractDuration { get; set; }

        public string ServiceServiceType { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public string DocumentUrl { get; set; }
    }
}
