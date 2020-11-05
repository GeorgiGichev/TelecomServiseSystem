using System.Collections.Generic;

namespace TelecomServiceSystem.Web.ViewModels.Administration.Administration
{
    public class TeamAllViewModel
    {
        public int CityId { get; set; }

        public string EmployeeId { get; set; }

        public ICollection<TeamViewModel> Teams { get; set; } = new HashSet<TeamViewModel>();
    }
}
