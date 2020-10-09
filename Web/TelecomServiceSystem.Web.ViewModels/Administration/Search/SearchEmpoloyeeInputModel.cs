namespace TelecomServiceSystem.Web.ViewModels.Administration.Search
{
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class SearchEmpoloyeeInputModel : IMapTo<InputEmployeeSerchModel>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }
    }
}
