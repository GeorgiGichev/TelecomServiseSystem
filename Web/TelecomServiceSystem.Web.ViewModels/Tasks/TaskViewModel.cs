namespace TelecomServiceSystem.Web.ViewModels.Tasks
{
    using System;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class TaskViewModel : IMapFrom<EnginieringTask>
    {
        public int Id { get; set; }

        public DateTime InstalationDate { get; set; }

        public string OrderId { get; set; }

        public string TeamCityName { get; set; }

        public int TeamId { get; set; }

        public string TeamName => $"{this.TeamCityName}_{this.TeamId}";
    }
}
