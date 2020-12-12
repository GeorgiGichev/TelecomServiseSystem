namespace TelecomServiceSystem.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using TelecomServiceSystem.Data;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;
    using TelecomServiceSystem.Data.Repositories;
    using TelecomServiceSystem.Services.Data.Orders;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Services.Data.ServiceNumber;
    using TelecomServiceSystem.Services.Data.Tests.TestsModels;
    using Xunit;

    public class OrderServiceTests : TestsBase
    {
        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var orderRepo = new EfDeletableEntityRepository<Order>(dbContext);

            var moqServiceInfoService = new Mock<IServiceInfoService>();

            var fackeInfo = new ServiceInfo();
            moqServiceInfoService.Setup(x => x.CreateAsync<ServiceInfoModel>(It.IsAny<string>(), It.IsAny<ServiceInfoModel>(), It.IsAny<string>())).Returns(Task.FromResult(fackeInfo));

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new OrderService(
                orderRepo,
                moqServiceInfoService.Object,
                moqServiceNumberService.Object);

            var userId = Guid.NewGuid().ToString();
            await service.CreateAsync<ServiceInfoModel, OrderModel>(
                new OrderModel
                {
                    UserId = userId,
                },
                new ServiceInfoModel());

            var order = await orderRepo.AllAsNoTracking()
                .FirstOrDefaultAsync();
            var count = await orderRepo.AllAsNoTracking()
                .CountAsync();

            Assert.Equal(userId, order.UserId);
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task FinishOrderAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var orderRepo = new EfDeletableEntityRepository<Order>(dbContext);

            var moqServiceInfoService = new Mock<IServiceInfoService>();

            var fackeInfo = new ServiceInfo();
            moqServiceInfoService.Setup(x => x.CreateAsync<ServiceInfoModel>(It.IsAny<string>(), It.IsAny<ServiceInfoModel>(), It.IsAny<string>())).Returns(Task.FromResult(fackeInfo));

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new OrderService(
                orderRepo,
                moqServiceInfoService.Object,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();

            await orderRepo.AddAsync(new Order
            {
                Id = orderId,
                Status = Status.ForExecution,
            });
            await orderRepo.SaveChangesAsync();

            var model = new ServiceInfoModel
            {
                OrderId = orderId,
            };

            await service.FinishOrderAsync<ServiceInfoModel>(model);

            var order = await orderRepo.All()
                .FirstOrDefaultAsync();

            Assert.Equal(Status.Finished, order.Status);
        }
    }
}
