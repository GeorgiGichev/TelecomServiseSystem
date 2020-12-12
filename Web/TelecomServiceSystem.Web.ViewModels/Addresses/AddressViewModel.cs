namespace TelecomServiceSystem.Web.ViewModels.Addresses
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class AddressViewModel : IMapFrom<Address>, IMapTo<Address>
    {
        public string CityName { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public int Number { get; set; }

        public string Entrance { get; set; }

        public int? Floor { get; set; }

        public int? Apartment { get; set; }
    }
}
