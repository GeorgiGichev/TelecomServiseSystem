namespace TelecomServiceSystem.Web.ViewModels.Orders
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>, IMapTo<Order>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public int ServiceInfoId { get; set; }

        public string DocumentUrl { get; set; }
    }
}
