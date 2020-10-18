namespace TelecomServiceSystem.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class OrderInputViewModel
    {
        public string ServiceType { get; set; }

        public OrderViewModel Order { get; set; }

        public MobileServiceInfoViewModel MobileServiceInfo { get; set; }

        public FixedServiceInfiViewModel FixedServiceInfo { get; set; }

        public IEnumerable<ServiceNumberViewModel> Numbers { get; set; }

        public IEnumerable<ServiceViewModel> Services { get; set; } = new HashSet<ServiceViewModel>();

    }
}
