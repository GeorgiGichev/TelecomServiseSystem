namespace TelecomServiceSystem.Services.Data.Bills
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class BillsService : IBillsService
    {
        private readonly IDeletableEntityRepository<Bill> billRepo;

        public BillsService(IDeletableEntityRepository<Bill> billRepo)
        {
            this.billRepo = billRepo;
        }

        public async Task Create(string customerId, string url)
        {
            var bill = new Bill
            {
                CusotmerId = customerId,
                URL = url,
            };

            await this.billRepo.AddAsync(bill);
            await this.billRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllByCustomerIdAsync<T>(string customerId)
        {
            return await this.billRepo.All()
                .Where(x => x.CusotmerId == customerId)
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();
        }
    }
}
