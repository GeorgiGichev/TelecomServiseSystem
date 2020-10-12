namespace TelecomServiceSystem.Web.ViewModels.Customers
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class CustomerInputModel : IMapFrom<CreateCustomerInputModel>, IMapTo<Customer>
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
