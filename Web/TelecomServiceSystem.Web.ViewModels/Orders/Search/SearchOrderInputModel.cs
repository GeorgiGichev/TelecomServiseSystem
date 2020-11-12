namespace TelecomServiceSystem.Web.ViewModels.Orders.Search
{
    using System;

    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class SearchOrderInputModel : IMapTo<InputOrderSearchModel>
    {
        public string Status { get; set; }

        public DateTime? FinishedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string ServiceName { get; set; }

        public string ServiceType { get; set; }

        public string UserId { get; set; }
    }
}
