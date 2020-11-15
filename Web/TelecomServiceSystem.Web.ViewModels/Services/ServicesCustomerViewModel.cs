namespace TelecomServiceSystem.Web.ViewModels.Services
{
    using System.Collections.Generic;

    public class ServicesCustomerViewModel
    {
        public string CustomerId { get; set; }

        public ICollection<ServicesViewModel> Services { get; set; } = new HashSet<ServicesViewModel>();
    }
}
