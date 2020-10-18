namespace TelecomServiceSystem.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceNumberViewModel : IMapFrom<ServiceNumber>, IMapTo<ServiceNumber>
    {
        public string Id { get; set; }

        public string Number { get; set; }
    }
}
