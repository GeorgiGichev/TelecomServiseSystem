namespace TelecomServiceSystem.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;
    using TelecomServiceSystem.Data.Repositories;
    using TelecomServiceSystem.Services.Data.Services;
    using TelecomServiceSystem.Services.Data.Tests.TestsModels;
    using Xunit;

    public class ServiceServiceTests : TestsBase
    {
        [Fact]
        public async Task GetServiceNamesByTypeAsyncShouldWorkCorrectlyWhitTypeMobile()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceRepo = new EfDeletableEntityRepository<Service>(dbContext);

            var service = new ServiceService(serviceRepo);

            await serviceRepo.AddAsync(new Service
            {
                ServiceType = ServiceType.Mobile,
                Name = "ASD",
            });

            await serviceRepo.AddAsync(new Service
            {
                ServiceType = ServiceType.Fix,
                Name = "ASDD",
            });

            await serviceRepo.SaveChangesAsync();

            var services = await service.GetServiceNamesByTypeAsync<ServiceModel>("mobile");

            Assert.Single(services);
            Assert.Collection(
                services,
                x => Assert.Equal("ASD", x.Name));
        }

        [Fact]
        public async Task GetServiceNamesByTypeAsyncShouldWorkCorrectlyWhitTypeFix()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceRepo = new EfDeletableEntityRepository<Service>(dbContext);

            var service = new ServiceService(serviceRepo);

            await serviceRepo.AddAsync(new Service
            {
                ServiceType = ServiceType.Mobile,
                Name = "ASD",
            });

            await serviceRepo.AddAsync(new Service
            {
                ServiceType = ServiceType.Fix,
                Name = "ASDD",
            });

            await serviceRepo.SaveChangesAsync();

            var services = await service.GetServiceNamesByTypeAsync<ServiceModel>("fix");

            Assert.Single(services);
            Assert.Collection(
                services,
                x => Assert.Equal("ASDD", x.Name));
        }

        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceRepo = new EfDeletableEntityRepository<Service>(dbContext);

            var service = new ServiceService(serviceRepo);

            var model = new ServiceModel
            {
                ServiceType = 1,
                Name = "ASD",
            };

            await service.CreateAsync<ServiceModel>(model);

            var serviceInDB = await serviceRepo.All()
                .FirstOrDefaultAsync(x => x.Name == "ASD");

            Assert.Equal("ASD", serviceInDB.Name);
        }

        [Fact]
        public async Task GetAllTypesAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var serviceRepo = new EfDeletableEntityRepository<Service>(dbContext);

            var service = new ServiceService(serviceRepo);

            await serviceRepo.AddAsync(new Service
            {
                ServiceType = ServiceType.Mobile,
                Name = "ASD",
            });

            await serviceRepo.AddAsync(new Service
            {
                ServiceType = ServiceType.Fix,
                Name = "ASDD",
            });

            await serviceRepo.SaveChangesAsync();

            var types = await service.GetAllTypesAsync();

            Assert.Equal(2, types.Count());
            Assert.Contains("Mobile", types);
            Assert.Contains("Fix", types);
        }
    }
}
