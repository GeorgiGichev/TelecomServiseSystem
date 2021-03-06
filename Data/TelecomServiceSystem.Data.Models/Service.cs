﻿namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Text;

    using TelecomServiceSystem.Data.Common.Models;
    using TelecomServiceSystem.Data.Models.Enums;

    public class Service : BaseDeletableModel<int>
    {
        public Service()
        {
            this.ServicesInfo = new HashSet<ServiceInfo>();
        }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public ServiceType ServiceType { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<ServiceInfo> ServicesInfo { get; set; }
    }
}
