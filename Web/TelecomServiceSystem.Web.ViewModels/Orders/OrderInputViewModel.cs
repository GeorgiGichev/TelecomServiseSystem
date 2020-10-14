namespace TelecomServiceSystem.Web.ViewModels.Orders
{
    public class OrderInputViewModel
    {
        public OrderViewModel Order { get; set; }

        public MobileServiceInfoViewModel MobileServiceInfo { get; set; }

        public FixedServiceInfiViewModel FixedServiceInfo { get; set; }

        public ServiceNumberViewModel ServiceNumbers { get; set; }

        public ServiceViewModel Services { get; set; }
    }
}
