namespace TelecomServiceSystem.Web.ViewModels.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceCreateInputModel : IMapTo<Service>
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string ServiceType { get; set; }

        [Range(0, 199)]
        public decimal Price { get; set; }

        public ICollection<string> Types { get; set; } = new HashSet<string>();
    }
}
