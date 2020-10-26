namespace TelecomServiceSystem.Web.ViewModels.Addresses
{
    using AutoMapper;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class InstalationAddressViewModel : IMapFrom<Address>
    {
        public string Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public int Number { get; set; }

        public string Entrance { get; set; }

        public int? Floor { get; set; }

        public int? Apartment { get; set; }

        public string FullAddress => this.ToString();

        public override string ToString()
        {
            var street = this.Street != null ? $"str. {this.Street}" : string.Empty;
            var neighborhood = this.Neighborhood != null ? $"dist {this.Neighborhood}" : string.Empty;
            var entrance = this.Entrance != null ? this.Entrance : string.Empty;
            var floor = this.Floor != null ? $"fl.{this.Floor.ToString()}" : string.Empty;
            var apartment = this.Apartment != null ? $"ap.{this.Apartment.ToString()}" : string.Empty;
            return $"{this.City}, {street}, {neighborhood}, {this.Number} {entrance} {floor} {apartment}";
        }
    }
}
