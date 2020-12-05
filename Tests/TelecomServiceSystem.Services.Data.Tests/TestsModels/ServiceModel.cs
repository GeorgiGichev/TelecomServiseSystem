namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceModel : IMapFrom<Service>, IMapTo<Service>
    {
        public string Name { get; set; }

        public int ServiceType { get; set; }
    }
}
