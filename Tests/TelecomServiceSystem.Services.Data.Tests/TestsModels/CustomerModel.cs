namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class CustomerModel : IMapFrom<Customer>, IMapTo<Customer>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int DocumentType { get; set; }

        public string PersonalNumber { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
