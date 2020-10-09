namespace TelecomServiceSystem.Services.Models
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class InputCustomerSearchModel : IMapFrom<Customer>, IMapTo<Customer>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

    }
}
