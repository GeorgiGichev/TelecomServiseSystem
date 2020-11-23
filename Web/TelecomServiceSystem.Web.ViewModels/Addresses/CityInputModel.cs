namespace TelecomServiceSystem.Web.ViewModels.Addresses
{
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class CityInputModel : IMapTo<City>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
