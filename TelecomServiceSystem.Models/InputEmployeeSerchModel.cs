using TelecomServiceSystem.Data.Models;
using TelecomServiceSystem.Services.Mapping;

namespace TelecomServiceSystem.Services.Models
{
    public class InputEmployeeSerchModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
