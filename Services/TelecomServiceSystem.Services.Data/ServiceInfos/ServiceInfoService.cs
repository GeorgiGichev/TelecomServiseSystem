namespace TelecomServiceSystem.Services.Data.ServiceInfos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class ServiceInfoService : IServiceInfoService
    {
        private readonly IDeletableEntityRepository<ServiceInfo> serviseInfoRepo;
        private readonly IDeletableEntityRepository<SimCard> simRepo;

        public ServiceInfoService(IDeletableEntityRepository<ServiceInfo> serviseInfoRepo, IDeletableEntityRepository<SimCard> simRepo)
        {
            this.serviseInfoRepo = serviseInfoRepo;
            this.simRepo = simRepo;
        }

        public async Task<ServiceInfo> CreateAsync<T>(string orderId, T model)
        {
            var serviceInfoToAdd = model.To<ServiceInfo>();
            serviceInfoToAdd.OrderId = orderId;
            serviceInfoToAdd.Expirеs = DateTime.UtcNow.AddMonths(serviceInfoToAdd.ContractDuration);
            await this.serviseInfoRepo.AddAsync(serviceInfoToAdd);
            await this.serviseInfoRepo.SaveChangesAsync();

            return serviceInfoToAdd;
        }

        public async Task<T> GetByOrderIdAsync<T>(string orderId)
        {
            return await this.serviseInfoRepo.All()
                .Where(si => si.OrderId == orderId)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetICC()
        {
            if (this.simRepo.All().Count() == 0)
            {
                return GlobalConstants.NoAvailableSimMessage;
            }

            var sim = await this.simRepo.All()
              .FirstOrDefaultAsync();

            this.simRepo.Delete(sim);
            return sim.ICC;
        }

        public async Task SetServiceAsActiveAsync(int serviceInfoId)
        {
            var serviceInfo = await this.serviseInfoRepo.All().FirstOrDefaultAsync(si => si.Id == serviceInfoId);
            serviceInfo.IsActive = true;
            this.serviseInfoRepo.Update(serviceInfo);
            if (serviceInfo.ICC != null)
            {
                var sim = await this.simRepo.All().FirstOrDefaultAsync(s => s.ICC == serviceInfo.ICC);
                this.simRepo.HardDelete(sim);
            }

            await this.serviseInfoRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TOutput>> GetBySearchCriteriaAsync<TOutput, TQuery>(TQuery query)
        {
            IQueryable<ServiceInfo> orders = this.serviseInfoRepo.All();

            var order = query.To<InputOrderSearchModel>();

            if (order.Status != null)
            {
                orders = orders.Where(c => c.Order.Status == Enum.Parse<Status>(order.Status));
            }

            if (order.ServiceType != null)
            {
                orders = orders.Where(c => c.Service.ServiceType == Enum.Parse<ServiceType>(order.ServiceType));
            }

            if (order.FinishedOn.HasValue)
            {
                var date = order.FinishedOn.Value.Date;
                orders = orders.Where(c => c.Order.FinishedOn.Value.Date == date);
            }

            if (order.CreatedOn.HasValue)
            {
                var date = order.CreatedOn.Value.Date;
                orders = orders.Where(c => c.Order.CreatedOn.Date == date);
            }

            if (order.ServiceName != null)
            {
                orders = orders.Where(c => c.Service.Name.ToLower().Contains(order.ServiceName.ToLower()));
            }

            var result = await orders.To<TOutput>()
                .ToListAsync();

            return result;
        }
    }
}
