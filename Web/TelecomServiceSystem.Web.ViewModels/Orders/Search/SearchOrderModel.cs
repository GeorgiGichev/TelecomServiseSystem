namespace TelecomServiceSystem.Web.ViewModels.Orders.Search
{
    using System.Collections.Generic;

    public class SearchOrderModel
    {
        public SearchOrderInputModel InputModel { get; set; }

        public ICollection<SearchOrderViewModel> Orders { get; set; } = new HashSet<SearchOrderViewModel>();
    }
}
