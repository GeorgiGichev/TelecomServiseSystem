namespace TelecomServiceSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Common.Models;

    public class SimCard : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(22)]
        public string ICC { get; set; }
    }
}
