namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class EmployeeSearchModel : IMapFrom<InputEmployeeSerchModel>, IMapTo<InputEmployeeSerchModel>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
