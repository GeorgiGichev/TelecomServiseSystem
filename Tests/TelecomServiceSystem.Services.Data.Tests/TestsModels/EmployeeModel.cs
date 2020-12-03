namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class EmployeeModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }

        public bool IsDeleted { get; set; }
    }
}
