namespace TelecomServiceSystem.Web.ViewModels.Tasks
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class InstallationInfoViewModel : IMapFrom<ServiceInfo>
    {
        public string OrderId { get; set; }

        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public string AddressCityName { get; set; }

        public string AddressStreet { get; set; }

        public string AddressNeighborhood { get; set; }

        public int AddressNumber { get; set; }

        public string AddressEntrance { get; set; }

        public int? AddressFloor { get; set; }

        public int? AddressApartment { get; set; }

        public string ServiceName { get; set; }

        public string ServiceNumberNumber { get; set; }
    }
}
