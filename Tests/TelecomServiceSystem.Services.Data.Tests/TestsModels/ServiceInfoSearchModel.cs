namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using System;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class ServiceInfoSearchModel : IMapFrom<ServiceInfo>, IMapTo<InputOrderSearchModel>
    {
        public string Status => this.OrderStatus;

        public string OrderStatus { get; set; }

        public DateTime? FinishedOn => this.OrderFinishedOn;

        public DateTime? OrderFinishedOn { get; set; }

        public DateTime? CreatedOn => this.OrderCreatedOn;

        public DateTime? OrderCreatedOn { get; set; }

        public string ServiceName { get; set; }

        public string ServiceType => this.ServiceServiceType;

        public string ServiceServiceType { get; set; }

        public string UserId => this.OrderUserId;

        public string OrderUserId { get; set; }
    }
}
