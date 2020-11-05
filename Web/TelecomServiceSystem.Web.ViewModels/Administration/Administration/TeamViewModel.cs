namespace TelecomServiceSystem.Web.ViewModels.Administration.Administration
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class TeamViewModel : IMapFrom<Team>
    {
        public int Id { get; set; }

        public string Name => $"{this.CityName}_{this.Id}";

        public string CityName { get; set; }

        public int EmployeesCount { get; set; }

        public int TeamCapacity { get; set; }

        public int FreePositions => this.TeamCapacity - this.EmployeesCount;
    }
}
