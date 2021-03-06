﻿namespace TelecomServiceSystem.Data.Models
{
    using System;

    using TelecomServiceSystem.Data.Common.Models;

    public class InstalationSlot : BaseDeletableModel<int>
    {
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public DateTime StartingTime { get; set; }

        public DateTime EndingTime { get; set; }
    }
}
