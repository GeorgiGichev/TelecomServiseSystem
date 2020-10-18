namespace TelecomServiceSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Common.Models;

    public class Device : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(15)]
        public string IMEI { get; set; }

        [Required]
        [MaxLength(10)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(20)]
        public string Model { get; set; }
    }
}
