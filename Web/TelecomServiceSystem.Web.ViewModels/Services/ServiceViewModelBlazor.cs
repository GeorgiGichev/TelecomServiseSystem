namespace TelecomServiceSystem.Web.ViewModels.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceViewModelBlazor : IMapFrom<Service>, IMapTo<Service>
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
