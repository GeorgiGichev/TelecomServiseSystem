namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class CustomerEditModel : IMapFrom<Customer>, IMapTo<Customer>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
