namespace TelecomServiceSystem.Services.Data.ServiceInfos
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

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

            return await this.serviseInfoRepo.All()
                .FirstOrDefaultAsync(si => si.OrderId == orderId);
        }

        public async Task<T> GetByOrderId<T>(string orderId)
        {
            return (await this.serviseInfoRepo.All()
                .FirstOrDefaultAsync(si => si.OrderId == orderId))
                .To<T>();
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
    }
}
