namespace TelecomServiceSystem.Services.Data.ServiceNumber
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<T>> GetFreeNumbersAsync<T>(string serviceType, string serviceName)
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
                .Where(n => n.IsFree && n.Number.StartsWith(startOfNumber) && !n.ServiceInfos.Any())
                .Take(10)
                .To<T>()
                .ToListAsync();
        }

        public async Task SetNumberAsHiredAsync(int numberId)
        {
            var number = await this.numbersRepo.All().FirstOrDefaultAsync(n => n.Id == numberId);
            number.IsFree = false;
            this.numbersRepo.Update(number);
            await this.numbersRepo.SaveChangesAsync();
        }
    }
}
