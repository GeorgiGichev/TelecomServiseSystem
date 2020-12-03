namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using System;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class BillModel : IMapFrom<Bill>, IMapTo<Bill>
    {
        public DateTime CreatedOn { get; set; }

        public string Url { get; set; }
    }
}
