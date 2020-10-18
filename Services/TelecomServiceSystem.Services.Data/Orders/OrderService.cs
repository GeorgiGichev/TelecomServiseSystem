namespace TelecomServiceSystem.Services.Data.Orders
{
    using System.Threading.Tasks;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Services.Mapping;

    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepo;
        private readonly IServiceInfoService seriveInfoService;

        public OrderService(IDeletableEntityRepository<Order> orderRepo, IServiceInfoService seriveInfoService)
        {
            this.orderRepo = orderRepo;
            this.seriveInfoService = seriveInfoService;
        }

        public async Task<Toutput> CreateAsync<Toutput, Tinput>(Tinput model)
        {
            var orderToAdd = model.To<Order>();
            var serviceInfo = await this.seriveInfoService.CreateAsync(orderToAdd.Id);
            orderToAdd.ServiceInfoId = serviceInfo.Id;
            await this.orderRepo.AddAsync(orderToAdd);
            await this.orderRepo.SaveChangesAsync();

            return serviceInfo.To<Toutput>();
        }
    }
}
