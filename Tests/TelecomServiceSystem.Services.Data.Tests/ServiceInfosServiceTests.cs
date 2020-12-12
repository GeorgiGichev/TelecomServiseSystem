namespace TelecomServiceSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Data;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;
    using TelecomServiceSystem.Data.Repositories;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Services.Data.ServiceNumber;
    using TelecomServiceSystem.Services.Data.Tests.TestsModels;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;
    using Xunit;

    public class ServiceInfosServiceTests : TestsBase
    {
        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();
            var model = new ServiceInfoModel();

            var serviceInfo = await service.CreateAsync<ServiceInfoModel>(orderId, model, string.Empty);

            var count = await serviceInfoRepo.AllAsNoTracking()
                .CountAsync();

            var serviceInfoForComp = await serviceInfoRepo.All()
                .FirstOrDefaultAsync();

            Assert.Equal(serviceInfo.Id, serviceInfoForComp.Id);
            Assert.Equal(serviceInfo.OrderId, serviceInfoForComp.OrderId);
            Assert.Equal(orderId, serviceInfoForComp.OrderId);
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task GetByOrderIdAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();
            var model = new ServiceInfoModel();

            var createdServiceInfo = await service.CreateAsync<ServiceInfoModel>(orderId, model, string.Empty);

            var serviceInfo = await service.GetByOrderIdAsync<ServiceInfoModel>(orderId);

            Assert.Equal(orderId, serviceInfo.OrderId);
            Assert.Equal(createdServiceInfo.Id, serviceInfo.Id);
        }

        [Fact]
        public async Task GetICCAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var iccList = new List<string>
            {
                "89359032201234567895",
                "89359032201234567890",
            };

            await simRepo.AddAsync(new SimCard
            {
                ICC = "89359032201234567895",
            });

            await simRepo.AddAsync(new SimCard
            {
                ICC = "89359032201234567890",
            });
            await simRepo.SaveChangesAsync();

            var sim = await service.GetICCAsync();
            var allSims = await simRepo.All().Where(x => !x.IsDeleted).ToListAsync();

            Assert.Single(allSims);
            Assert.Contains(sim, iccList);
        }

        [Fact]
        public async Task GetICCAsyncShouldReturnMessageWhenRepoIsEmpty()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var sim = await service.GetICCAsync();

            var count = await simRepo.All()
                .CountAsync();

            Assert.Equal(0, count);
            Assert.Contains(sim, GlobalConstants.NoAvailableSimMessage);
        }

        [Fact]
        public async Task SetServiceAsActiveAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();

            await simRepo.AddAsync(new SimCard
            {
                ICC = "89359032201234567890",
            });
            await simRepo.SaveChangesAsync();

            var model = new ServiceInfoModel
            {
                ICC = "89359032201234567890",
            };

            var serviceInfo = await service.CreateAsync<ServiceInfoModel>(orderId, model, string.Empty);

            await service.SetServiceAsActiveAsync(serviceInfo.Id);

            var serviceInfoForComp = await serviceInfoRepo.All()
                .FirstOrDefaultAsync(x => x.Id == serviceInfo.Id);

            var sims = await simRepo.All().ToListAsync();

            Assert.True(serviceInfoForComp.IsActive);
            Assert.Empty(sims);
        }

        [Fact]
        public async Task GetBySearchCriteriaAsyncShouldWorkCorrectlyWithServiceType()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();

            await serviceInfoRepo.AddAsync(new ServiceInfo
            {
                OrderId = orderId,
                Service = new Service
                {
                    ServiceType = ServiceType.Mobile,
                },
            });
            await serviceInfoRepo.SaveChangesAsync();

            var serviceInfo = (await serviceInfoRepo.All()
                .FirstOrDefaultAsync()).To<ServiceInfoSearchModel>();
            if (serviceInfo.OrderStatus == "0")
            {
                serviceInfo.OrderStatus = null;
            }

            serviceInfo.OrderCreatedOn = null;
            var serviceInfos = await service.GetBySearchCriteriaAsync<InputOrderSearchModel, ServiceInfoSearchModel>(serviceInfo);

            Assert.Single(serviceInfos);
        }

        [Fact]
        public async Task GetAllByCustomerIdAsyncShouldWorkCorrectlyWithServiceType()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var customerId = Guid.NewGuid().ToString();
            var orderId = Guid.NewGuid().ToString();
            var model = new ServiceInfoModel
            {
                CustomerId = customerId,
                IsActive = true,
            };
            var createdServiceInfo = await service.CreateAsync<ServiceInfoModel>(orderId, model, string.Empty);

            var serviceInfos = await service.GetAllByCustomerIdAsync<ServiceInfoModel>(customerId);

            Assert.Single(serviceInfos);
            Assert.Collection(
                serviceInfos,
                x => Assert.Equal(customerId, x.CustomerId));
        }

        [Fact]
        public async Task GetByIdAsyncShouldWorkCorrectlyWithServiceType()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var customerId = Guid.NewGuid().ToString();
            var orderId = Guid.NewGuid().ToString();
            var model = new ServiceInfoModel();
            var createdServiceInfo = await service.CreateAsync<ServiceInfoModel>(orderId, model, string.Empty);

            var serviceInfos = await service.GetByIdAsync<ServiceInfoModel>(1);

            Assert.Equal(1, serviceInfos.Id);
        }

        [Fact]
        public async Task RenewAsyncShouldWorkCorrectlyWithServiceType()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();
            var model = new ServiceInfoModel
            {
                ContractDuration = 12,
            };
            var createdServiceInfo = await service.CreateAsync<ServiceInfoModel>(orderId, model, string.Empty);

            var serviceInfo = await service.GetByIdAsync<ServiceInfoModel>(1);
            serviceInfo.ContractDuration = 24;
            await service.RenewAsync<ServiceInfoModel>(serviceInfo, string.Empty);

            serviceInfo = await service.GetByIdAsync<ServiceInfoModel>(1);

            Assert.Equal(24, serviceInfo.ContractDuration);
        }

        [Fact]
        public async Task ContractCancelAsyncShouldWorkCorrectlyWithServiceType()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();
            var model = new ServiceInfoModel
            {
                IsActive = true,
            };
            var createdServiceInfo = await service.CreateAsync<ServiceInfoModel>(orderId, model, string.Empty);

            await service.ContractCancelAsync(1, string.Empty);

            var serviceInfo = await service.GetByIdAsync<ServiceInfoModel>(1);

            Assert.False(serviceInfo.IsActive);
        }

        [Fact]
        public async Task ExistByOrderIdAsyncShouldReturnTrueWhenExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();
            var model = new ServiceInfoModel();
            var createdServiceInfo = await service.CreateAsync<ServiceInfoModel>(orderId, model, string.Empty);

            Assert.True(await service.ExistByOrderIdAsync(orderId));
        }

        [Fact]
        public async Task ExistByOrderIdAsyncShouldReturnFalseWhenDoesNotExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();
            var model = new ServiceInfoModel();
            var createdServiceInfo = await service.CreateAsync<ServiceInfoModel>(orderId, model, string.Empty);

            Assert.False(await service.ExistByOrderIdAsync(Guid.NewGuid().ToString()));
        }

        [Fact]
        public async Task ExistByIdAsyncShouldReturnTrueWhenExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();
            var model = new ServiceInfoModel();
            var createdServiceInfo = await service.CreateAsync<ServiceInfoModel>(orderId, model, string.Empty);

            Assert.True(await service.ExistByIdAsync(1));
        }

        [Fact]
        public async Task ExistByIdAsyncShouldReturnFalseWhenDoesNotExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();
            var model = new ServiceInfoModel();
            var createdServiceInfo = await service.CreateAsync<ServiceInfoModel>(orderId, model, string.Empty);

            Assert.False(await service.ExistByIdAsync(2));
        }

        [Fact]
        public async Task GetBySearchCriteriaAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceInfoRepo = new EfDeletableEntityRepository<ServiceInfo>(dbContext);

            var simRepo = new EfDeletableEntityRepository<SimCard>(dbContext);

            var moqServiceNumberService = new Mock<IServiceNumberService>();

            var service = new ServiceInfoService(
                serviceInfoRepo,
                simRepo,
                moqServiceNumberService.Object);

            var orderId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();

            await serviceInfoRepo.AddAsync(new ServiceInfo
            {
                OrderId = orderId,
                Order = new Order
                {
                    Id = orderId,
                    FinishedOn = DateTime.UtcNow,
                    UserId = userId,
                    Status = Status.Finished,
                },
                Service = new Service
                {
                    ServiceType = ServiceType.Mobile,
                    Name = "ASD",
                },
            });
            await serviceInfoRepo.SaveChangesAsync();

            var serviceInfo = (await serviceInfoRepo.All()
                .FirstOrDefaultAsync()).To<ServiceInfoSearchModel>();

            var serviceInfos = await service.GetBySearchCriteriaAsync<ServiceInfoModel, ServiceInfoSearchModel>(serviceInfo);

            Assert.Single(serviceInfos);
            Assert.Collection(
                serviceInfos,
                x => Assert.Equal(orderId, x.OrderId));
        }
    }
}
