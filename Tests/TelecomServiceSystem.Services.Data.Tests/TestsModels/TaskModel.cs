namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using System;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class TaskModel : IMapFrom<EnginieringTask>, IMapTo<EnginieringTask>
    {
        public string OrderId { get; set; }

        public int TeamId { get; set; }

        public DateTime InstalationDate { get; set; }
    }
}
