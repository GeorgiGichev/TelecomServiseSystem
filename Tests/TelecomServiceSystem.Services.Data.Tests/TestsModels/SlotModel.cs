namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using System;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class SlotModel : IMapFrom<InstalationSlot>, IMapTo<InstalationSlot>
    {
        public int TeamId { get; set; }

        public DateTime StartingTime { get; set; }

        public DateTime EndingTime { get; set; }
    }
}
