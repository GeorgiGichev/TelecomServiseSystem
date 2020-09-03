namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Device
    {
        public int Id { get; set; }

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
