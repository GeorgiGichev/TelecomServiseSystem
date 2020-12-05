namespace TelecomServiceSystem.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Repositories;
    using TelecomServiceSystem.Services.Data.Bills;
    using TelecomServiceSystem.Services.Data.Tests.TestsModels;
    using Xunit;

    public class BillsServiceTests : TestsBase
    {
        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var billRepo = new EfDeletableEntityRepository<Bill>(dbContext);

            var service = new BillsService(billRepo);

            var customerId = Guid.NewGuid().ToString();
            var url = Guid.NewGuid().ToString();

            await service.CreateAsync(customerId, url);

            var bill = await billRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == 1);

            var count = await billRepo.AllAsNoTracking().CountAsync();

            Assert.Equal(1, count);
            Assert.Equal(customerId, bill.CusotmerId);
            Assert.Equal(url, bill.URL);
        }

        [Fact]
        public async Task GetAllByCustomerIdAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var billRepo = new EfDeletableEntityRepository<Bill>(dbContext);

            var service = new BillsService(billRepo);

            var customerId = Guid.NewGuid().ToString();
            var url = Guid.NewGuid().ToString();

            await service.CreateAsync(customerId, url);

            var bills = await service.GetAllByCustomerIdAsync<BillModel>(customerId);

            Assert.Single(bills);
            Assert.Collection(
                bills,
                x => Assert.Equal(url, x.Url));
        }
    }
}
