namespace TelecomServiceSystem.Web.ViewModels.ContractTemplates
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Web.ViewModels.Addresses;

    public class CancellationViewModel : IMapFrom<Customer>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string FullName => $"{this.FirstName} {this.MiddleName} {this.LastName}";

        public string PersonalNumber { get; set; }

        public AddressViewModel Address { get; set; }

        public string Number { get; set; }

        public string Plan { get; set; }

        public string ServiceAddress { get; set; }

        public string UserId { get; set; }
    }
}
