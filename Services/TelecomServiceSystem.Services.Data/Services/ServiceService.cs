namespace TelecomServiceSystem.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceService : IServiceService
    {
        private readonly IDeletableEntityRepository<Service> serviceRepo;

        public ServiceService(IDeletableEntityRepository<Service> serviceRepo)
        {
            this.serviceRepo = serviceRepo;
        }

        public async Task<IEnumerable<T>> GetServiceNamesByTypeAsync<T>(string type)
        {
            return await this.serviceRepo.All()
               .Where(s => s.ServiceType == Enum.Parse<ServiceType>(type, true))
               .To<T>()
               .ToListAsync();
        }

        public async Task CreateAsync<T>(T model)
        {
            var service = model.To<Service>();
            if (await this.serviceRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Name.ToLower() == service.Name.ToLower()) == null)
            {
                await this.serviceRepo.AddAsync(service);
                await this.serviceRepo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<string>> GetAllTypesAsync()
        {
            var result = await this.serviceRepo.AllAsNoTracking()
                .Select(x => x.ServiceType.ToString())
                .Distinct()
                .ToListAsync();
            return result;
        }
    }
}
