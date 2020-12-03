namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using System;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class ServiceInfoModel : IMapFrom<ServiceInfo>, IMapTo<ServiceInfo>
    {
        public int Id { get; set; }

        public string OrderId { get; set; }

        public string ICC { get; set; }

        public string ServiceType { get; set; }

        public string CustomerId { get; set; }

        public bool IsActive { get; set; }

        public int ContractDuration { get; set; }
    }
}
