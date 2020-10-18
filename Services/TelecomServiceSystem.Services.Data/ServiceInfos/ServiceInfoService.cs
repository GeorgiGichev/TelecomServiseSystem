namespace TelecomServiceSystem.Services.Data.ServiceInfos
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;

    public class ServiceInfoService : IServiceInfoService
    {
        private readonly IDeletableEntityRepository<ServiceInfo> serviseInfoRepo;
        private readonly IDeletableEntityRepository<SimCard> simRepo;

        public ServiceInfoService(IDeletableEntityRepository<ServiceInfo> serviseInfoRepo, IDeletableEntityRepository<SimCard> simRepo)
        {
            this.serviseInfoRepo = serviseInfoRepo;
            this.simRepo = simRepo;
        }

        public async Task<ServiceInfo> CreateAsync(string orderId)
        {
            await this.serviseInfoRepo.AddAsync(new ServiceInfo
            {
                OrderId = orderId,
            });
            await this.serviseInfoRepo.SaveChangesAsync();

            return await this.serviseInfoRepo.All()
                .FirstOrDefaultAsync(si => si.OrderId == orderId);
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
