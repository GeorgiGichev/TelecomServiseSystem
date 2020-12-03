namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class CityModel : IMapFrom<City>, IMapTo<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
