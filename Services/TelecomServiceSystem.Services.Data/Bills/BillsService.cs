namespace TelecomServiceSystem.Services.Data.Bills
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;

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
    }
}
