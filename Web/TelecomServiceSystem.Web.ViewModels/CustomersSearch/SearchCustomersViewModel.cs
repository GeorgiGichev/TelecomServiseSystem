namespace TelecomServiceSystem.Web.ViewModels.CustomersSearch
{
    using System.Collections.Generic;

    public class SearchCustomersViewModel
    {
        public SearchCustomerInputModel Input { get; set; }

        public ICollection<SearchCustomersOutpuModel> CustomersList { get; set; } = new HashSet<SearchCustomersOutpuModel>();
    }
}
