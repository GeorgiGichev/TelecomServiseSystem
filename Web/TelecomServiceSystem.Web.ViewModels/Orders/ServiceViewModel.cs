namespace TelecomServiceSystem.Web.ViewModels.Orders
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceViewModel : IMapFrom<Service>, IMapTo<Service>
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
