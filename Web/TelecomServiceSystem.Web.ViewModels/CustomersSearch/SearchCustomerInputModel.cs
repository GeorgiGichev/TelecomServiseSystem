namespace TelecomServiceSystem.Web.ViewModels.CustomersSearch
{
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class SearchCustomerInputModel : IMapTo<InputCustomerSearchModel>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }
    }
}
