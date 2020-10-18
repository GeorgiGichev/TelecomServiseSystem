namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using TelecomServiceSystem.Data.Common.Models;

    public class SimCard : BaseDeletableModel<int>
    {

        [Required]
        [MaxLength(22)]
        public string ICC { get; set; }
    }
}
