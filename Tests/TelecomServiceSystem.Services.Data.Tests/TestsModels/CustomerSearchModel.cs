namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class CustomerSearchModel : IMapFrom<InputCustomerSearchModel>, IMapTo<InputCustomerSearchModel>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }
    }
}
