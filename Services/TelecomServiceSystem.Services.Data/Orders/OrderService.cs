namespace TelecomServiceSystem.Services.Data.Orders
{
    using System;
    using System.Threading.Tasks;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;
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

        public async Task<Toutput> CreateAsync<Toutput, Tinput>(Tinput order, Toutput serviceInfo)
        {
            var orderFromModel = order.To<Order>();
            var orderToAdd = new Order
            {
                UserId = orderFromModel.UserId,
                Status = Enum.Parse<Status>("ForExecution"),
            };

            var info = await this.seriveInfoService.CreateAsync(orderToAdd.Id, serviceInfo);
            orderToAdd.ServicesInfos.Add(info);
            await this.orderRepo.AddAsync(orderToAdd);
            await this.orderRepo.SaveChangesAsync();

            return serviceInfo.To<Toutput>();
        }
    }
}
