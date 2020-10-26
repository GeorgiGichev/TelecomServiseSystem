﻿namespace TelecomServiceSystem.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class OrderInputViewModel
    {
        public string OrderId { get; set; }

        public string ServiceType { get; set; }

        public MobileServiceInfoViewModel MobileServiceInfo { get; set; }

        public FixedServiceInfiViewModel FixedServiceInfo { get; set; }

        public IEnumerable<ServiceNumberViewModel> Numbers { get; set; } = new HashSet<ServiceNumberViewModel>();

        public IEnumerable<ServiceViewModel> Services { get; set; } = new HashSet<ServiceViewModel>();
    }
}
