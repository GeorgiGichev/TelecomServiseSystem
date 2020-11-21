namespace TelecomServiceSystem.Data.Models
{
    using System;

    using TelecomServiceSystem.Data.Common.Models;

    public class EnginieringTask : BaseDeletableModel<int>
    {
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public DateTime InstalationDate { get; set; }
    }
}
