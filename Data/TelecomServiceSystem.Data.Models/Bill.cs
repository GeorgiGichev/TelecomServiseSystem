namespace TelecomServiceSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Common.Models;

    public class Bill : BaseDeletableModel<int>
    {
        [Required]
        public string CusotmerId { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        public string URL { get; set; }
    }
}
