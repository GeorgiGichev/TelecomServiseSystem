namespace TelecomServiceSystem.Services.Data.ServiceNumber
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceNumbersService : IServiceNumberService
    {
        private readonly IDeletableEntityRepository<ServiceNumber> numbersRepo;

        public ServiceNumbersService(IDeletableEntityRepository<ServiceNumber> numbersRepo)
        {
            this.numbersRepo = numbersRepo;
        }

        public async Task<IEnumerable<T>> GetFreeNumbers<T>(string serviceType, string serviceName)
        {
            string startOfNumber = string.Empty;
            if (serviceType == "mobile")
            {
                startOfNumber = "86";
            }
            else
            {
                if (serviceName.Contains("Net"))
                {
                    startOfNumber = "N.";
                }
                else
                {
                    startOfNumber = "TV.";
                }
            }

            return await this.numbersRepo.All()
                .Where(n => n.IsFree && n.Number.StartsWith(startOfNumber))
                .Take(10)
                .To<T>()
                .ToListAsync();
        }
    }
}
