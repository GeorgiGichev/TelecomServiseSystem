﻿namespace TelecomServiceSystem.Web.ViewModels.Orders.Search
{
    using System;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class SearchOrderViewModel : IMapFrom<ServiceInfo>
    {
        public string OrderId { get; set; }

        public string OrderStatus { get; set; }

        public DateTime? OrderFinishedOn { get; set; }

        public string OrderUserUsername { get; set; }

        public DateTime? OrderCreatedOn { get; set; }

        public string ServiceName { get; set; }

        public string OrderDocumentUrl { get; set; }
    }
}
