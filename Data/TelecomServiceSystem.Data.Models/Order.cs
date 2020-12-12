namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TelecomServiceSystem.Data.Common.Models;
    using TelecomServiceSystem.Data.Models.Enums;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ServicesInfos = new HashSet<ServiceInfo>();
        }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime? FinishedOn { get; set; }

        public Status Status { get; set; }

        [Required]
        public string DocumentUrl { get; set; }

        public int? EnginieringTaskId { get; set; }

        public virtual EnginieringTask EnginieringTask { get; set; }

        public virtual ICollection<ServiceInfo> ServicesInfos { get; set; }
    }
}
