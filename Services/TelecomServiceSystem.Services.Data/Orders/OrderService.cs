namespace TelecomServiceSystem.Services.Data.Orders
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Services.Data.ServiceNumber;
    using TelecomServiceSystem.Services.Mapping;

    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepo;
        private readonly IServiceInfoService serviceInfoService;
        private readonly IServiceNumberService serviceNumberService;

        public OrderService(IDeletableEntityRepository<Order> orderRepo, IServiceInfoService serviceInfoService, IServiceNumberService serviceNumberService)
        {
            this.orderRepo = orderRepo;
            this.serviceInfoService = serviceInfoService;
            this.serviceNumberService = serviceNumberService;
        }

        public async Task<Toutput> CreateAsync<Toutput, Tinput>(Tinput order, Toutput serviceInfo)
        {
            var orderFromModel = order.To<Order>();
            var orderToAdd = new Order
            {
                UserId = orderFromModel.UserId,
                Status = Status.ForExecution,
            };
            await this.orderRepo.AddAsync(orderToAdd);
            await this.orderRepo.SaveChangesAsync();
            var info = await this.serviceInfoService.CreateAsync(orderToAdd.Id, serviceInfo);

            return info.To<Toutput>();
        }

        public async Task FinishOrderAsync<T>(T model)
        {
            var infoModel = model.To<ServiceInfo>();
            var order = await this.orderRepo.All().FirstOrDefaultAsync(o => o.Id == infoModel.OrderId);
            order.Status = Status.Finished;
            order.FinishedOn = DateTime.UtcNow;
            this.orderRepo.Update(order);
            await this.serviceNumberService.SetNumberAsHiredAsync(infoModel.ServiceNumberId);
            await this.serviceInfoService.SetServiceAsActiveAsync(infoModel.Id);
        }
    }
}
