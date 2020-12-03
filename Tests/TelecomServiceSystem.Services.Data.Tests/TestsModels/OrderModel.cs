namespace TelecomServiceSystem.Services.Data.Tests.TestsModels
{
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class OrderModel : IMapFrom<Order>, IMapTo<Order>
    {
        public string Id { get; set; }

        public int ServiceInfoId { get; set; }

        public string UserId { get; set; }
    }
}
