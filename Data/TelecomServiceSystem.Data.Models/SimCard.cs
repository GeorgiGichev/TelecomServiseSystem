namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class SimCard
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(22)]
        public string ICC { get; set; }
    }
}
