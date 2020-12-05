namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceNumberModel : IMapFrom<ServiceNumber>, IMapTo<ServiceNumber>
    {
        public string Id { get; set; }

        public string Number { get; set; }
    }
}
