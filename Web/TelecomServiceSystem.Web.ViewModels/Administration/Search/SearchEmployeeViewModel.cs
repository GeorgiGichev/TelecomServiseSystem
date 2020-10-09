namespace TelecomServiceSystem.Web.ViewModels.Administration.Search
{
    using System.Collections.Generic;

    public class SearchEmployeeViewModel
    {
        public SearchEmpoloyeeInputModel Input { get; set; }

        public ICollection<SearhEmployeeOutputModel> EmployeesList { get; set; } = new HashSet<SearhEmployeeOutputModel>();
    }
}
