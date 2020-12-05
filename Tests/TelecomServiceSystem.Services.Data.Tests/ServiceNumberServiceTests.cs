namespace TelecomServiceSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Repositories;
    using TelecomServiceSystem.Services.Data.ServiceNumber;
    using TelecomServiceSystem.Services.Data.Tests.TestsModels;
    using Xunit;

    public class ServiceNumberServiceTests : TestsBase
    {
        [Fact]
        public async Task GetFreeNumbersAsyncShouldWorkCorrectlyWhitTypeMobile()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var numberRepo = new EfDeletableEntityRepository<ServiceNumber>(dbContext);

            var service = new ServiceNumbersService(numberRepo);

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "866666666",
                IsFree = true,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "866666661",
                IsFree = false,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "N.123456",
                IsFree = true,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "TV.123456",
                IsFree = true,
            });

            await numberRepo.SaveChangesAsync();

            var numbers = await service.GetFreeNumbersAsync<ServiceNumberModel>("mobile", string.Empty);

            Assert.Single(numbers);
            Assert.Collection(
                numbers,
                x => Assert.Equal("866666666", x.Number));
        }

        [Fact]
        public async Task GetFreeNumbersAsyncShouldWorkCorrectlyWhitTypeFixNet()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var numberRepo = new EfDeletableEntityRepository<ServiceNumber>(dbContext);

            var service = new ServiceNumbersService(numberRepo);

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "866666666",
                IsFree = true,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "866666661",
                IsFree = false,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "N.123456",
                IsFree = true,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "N.123451",
                IsFree = false,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "TV.123456",
                IsFree = true,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "TV.123451",
                IsFree = false,
            });

            await numberRepo.SaveChangesAsync();

            var numbers = await service.GetFreeNumbersAsync<ServiceNumberModel>("fix", "net");

            Assert.Single(numbers);
            Assert.Collection(
                numbers,
                x => Assert.Equal("N.123456", x.Number));
        }

        [Fact]
        public async Task GetFreeNumbersAsyncShouldWorkCorrectlyWhitTypeFixTv()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var numberRepo = new EfDeletableEntityRepository<ServiceNumber>(dbContext);

            var service = new ServiceNumbersService(numberRepo);

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "866666666",
                IsFree = true,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "866666661",
                IsFree = false,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "N.123456",
                IsFree = true,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "N.123451",
                IsFree = false,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "TV.123456",
                IsFree = true,
            });

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "TV.123451",
                IsFree = false,
            });

            await numberRepo.SaveChangesAsync();

            var numbers = await service.GetFreeNumbersAsync<ServiceNumberModel>("fix", "tv");

            Assert.Single(numbers);
            Assert.Collection(
                numbers,
                x => Assert.Equal("TV.123456", x.Number));
        }

        [Fact]
        public async Task SetNumberAsFreeAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var numberRepo = new EfDeletableEntityRepository<ServiceNumber>(dbContext);

            var service = new ServiceNumbersService(numberRepo);

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "866666661",
                IsFree = false,
            });

            await numberRepo.SaveChangesAsync();

            await service.SetNumberAsFreeAsync(1);

            var number = await numberRepo.All()
                .FirstOrDefaultAsync(x => x.Id == 1);

            Assert.True(number.IsFree);
        }

        [Fact]
        public async Task SetNumberAsHiredAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var numberRepo = new EfDeletableEntityRepository<ServiceNumber>(dbContext);

            var service = new ServiceNumbersService(numberRepo);

            await numberRepo.AddAsync(new ServiceNumber
            {
                Number = "866666661",
                IsFree = true,
            });

            await numberRepo.SaveChangesAsync();

            await service.SetNumberAsHiredAsync(1);

            var number = await numberRepo.All()
                .FirstOrDefaultAsync(x => x.Id == 1);

            Assert.False(number.IsFree);
        }
    }
}
