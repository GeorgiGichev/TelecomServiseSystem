namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using TelecomServiceSystem.Data.Common.Models;
    using TelecomServiceSystem.Data.Models.Enums;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ServiceInfoId { get; set; }

        public virtual ServiceInfo ServiceInfo { get; set; }

        public DateTime? FinishedOn { get; set; }

        public Status Status { get; set; }
    }
}
