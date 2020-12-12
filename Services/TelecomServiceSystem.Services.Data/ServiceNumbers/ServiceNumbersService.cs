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

        public async Task<string> GetByIdAsync(int numberId)
        {
            return (await this.numbersRepo.All()
                .FirstOrDefaultAsync(x => x.Id == numberId)).Number;
        }

        public async Task<string> GetByServiceInfoId(int serviceInfoId)
        {
            return await this.numbersRepo.All()
                .Where(x => x.ServiceInfos.Any(y => y.Id == serviceInfoId))
                .Select(x => x.Number)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetFreeNumbersAsync<T>(string serviceType, string serviceName)
        {
            string startOfNumber = string.Empty;
            if (serviceType.ToLower() == "mobile")
            {
                startOfNumber = "86";
            }
            else
            {
                if (serviceName.ToLower().Contains("net"))
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

        public async Task SetNumberAsFreeAsync(int numberId)
        {
            var number = await this.numbersRepo.All().FirstOrDefaultAsync(x => x.Id == numberId);
            number.IsFree = true;
            this.numbersRepo.Update(number);
            await this.numbersRepo.SaveChangesAsync();
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
