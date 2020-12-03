namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using System.Collections.Generic;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class AddressModel : IMapTo<Address>, IMapFrom<Address>
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public int Number { get; set; }

        public string Entrance { get; set; }

        public int? Floor { get; set; }

        public int? Apartment { get; set; }

        public bool IsMainAddress { get; set; }

        public string CustomerId { get; set; }

        public ICollection<CityModel> Cities { get; set; } = new HashSet<CityModel>();
    }
}
