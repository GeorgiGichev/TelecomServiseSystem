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

        public async Task<T> GetByNameAsync<T>(string name)
        {
            return (await this.serviceRepo.All()
                .FirstOrDefaultAsync(x => x.Name == name))
                .To<T>();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return (await this.serviceRepo.All()
                .FirstOrDefaultAsync(x => x.Id == id))
                .To<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            if (this.serviceRepo.All().Any())
            {
                return await this.serviceRepo.All().To<T>().ToListAsync();
            }
            else
            {
                return new List<T>();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var service = await this.serviceRepo.All()
                .FirstOrDefaultAsync(x => x.Id == id);
            this.serviceRepo.Delete(service);
            await this.serviceRepo.SaveChangesAsync();
        }
    }
}
