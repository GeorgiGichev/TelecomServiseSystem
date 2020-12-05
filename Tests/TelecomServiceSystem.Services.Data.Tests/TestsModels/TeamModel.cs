namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class TeamModel : IMapFrom<Team>, IMapTo<Team>
    {
        public int CityId { get; set; }
    }
}
