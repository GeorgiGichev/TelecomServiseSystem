namespace TelecomServiceSystem.Web.ViewModels.Services
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceCancelationViewModel : IMapFrom<ServiceInfo>, IMapTo<ServiceInfo>
    {
        public int Id { get; set; }

        public string ServiceNumberNumber { get; set; }

        public string ServiceName { get; set; }

        public decimal ServicePrice { get; set; }

        public DateTime Expirеs { get; set; }

        public string CustomerId { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
