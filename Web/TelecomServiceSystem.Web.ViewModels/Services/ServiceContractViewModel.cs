namespace TelecomServiceSystem.Web.ViewModels.Services
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceContractViewModel : IMapFrom<Service>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
