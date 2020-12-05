namespace TelecomServiceSystem.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Repositories;
    using TelecomServiceSystem.Services.Data.Employees;
    using TelecomServiceSystem.Services.Data.Tests.TestsModels;
    using TelecomServiceSystem.Services.Mapping;
    using Xunit;

    public class EmployeesServiceTests : TestsBase
    {
        [Fact]
        public async Task GetBySearchCriteriaAsyncShouldWorkCorrectlyWithPersonalNumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new EmployeesService(userRepo);

            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                EGN = "1234567890",
            });
            await userRepo.SaveChangesAsync();

            var model = new EmployeeSearchModel
            {
                EGN = "1234567890",
            };

            var customers = await service.GetBySearchCriteriaAsync<EmployeeModel, EmployeeSearchModel>(model);

            Assert.Single(customers);
            Assert.Collection(
                customers,
                x => Assert.Equal("1234567890", x.EGN));
        }

        [Fact]
        public async Task GetBySearchCriteriaAsyncShouldWorkCorrectlyWithFirstName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new EmployeesService(userRepo);

            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                EGN = "1234567890",
            });
            await userRepo.SaveChangesAsync();

            var model = new EmployeeSearchModel
            {
                FirstName = "ivan",
            };

            var customers = await service.GetBySearchCriteriaAsync<EmployeeModel, EmployeeSearchModel>(model);

            Assert.Single(customers);
            Assert.Collection(
                customers,
                x => Assert.Equal("ivan", x.FirstName));
        }

        [Fact]
        public async Task GetBySearchCriteriaAsyncShouldWorkCorrectlyWithMiddleName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new EmployeesService(userRepo);

            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                EGN = "1234567890",
            });
            await userRepo.SaveChangesAsync();

            var model = new EmployeeSearchModel
            {
                MiddleName = "ivanov",
            };

            var customers = await service.GetBySearchCriteriaAsync<EmployeeModel, EmployeeSearchModel>(model);

            Assert.Single(customers);
            Assert.Collection(
                customers,
                x => Assert.Equal("ivanov", x.MiddleName));
        }

        [Fact]
        public async Task GetBySearchCriteriaAsyncShouldWorkCorrectlyWithLastName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new EmployeesService(userRepo);

            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                EGN = "1234567890",
            });
            await userRepo.SaveChangesAsync();

            var model = new EmployeeSearchModel
            {
                LastName = "ivanov",
            };

            var customers = await service.GetBySearchCriteriaAsync<EmployeeModel, EmployeeSearchModel>(model);

            Assert.Single(customers);
            Assert.Collection(
                customers,
                x => Assert.Equal("ivanov", x.LastName));
        }

        [Fact]
        public async Task GetBySearchCriteriaAsyncShouldWorkCorrectlyWithIsDelete()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new EmployeesService(userRepo);

            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                EGN = "1234567890",
                IsDeleted = true,
            });
            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "gosho",
                LastName = "goshov",
                MiddleName = "goshov",
                EGN = "1234567891",
            });
            await userRepo.SaveChangesAsync();

            var model = new EmployeeSearchModel
            {
                IsDeleted = true,
            };

            var customers = await service.GetBySearchCriteriaAsync<EmployeeModel, EmployeeSearchModel>(model);

            Assert.Single(customers);
            Assert.Collection(
                customers,
                x => Assert.Equal("ivanov", x.LastName));
        }

        [Fact]
        public async Task GetBySearchCriteriaAsyncShouldWorkCorrectlyWithIsDeleteFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new EmployeesService(userRepo);

            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                EGN = "1234567890",
                IsDeleted = true,
            });
            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "gosho",
                LastName = "goshov",
                MiddleName = "goshov",
                EGN = "1234567891",
            });
            await userRepo.SaveChangesAsync();

            var model = new EmployeeSearchModel
            {
                IsDeleted = false,
            };

            var customers = await service.GetBySearchCriteriaAsync<EmployeeModel, EmployeeSearchModel>(model);

            Assert.Single(customers);
            Assert.Collection(
                customers,
                x => Assert.Equal("goshov", x.LastName));
        }

        [Fact]
        public async Task GetByIdAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new EmployeesService(userRepo);

            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                EGN = "1234567890",
            });
            await userRepo.SaveChangesAsync();

            var customerId = (await userRepo.AllAsNoTracking()
                .FirstOrDefaultAsync()).Id;

            var customer = await service.GetByIdAsync<EmployeeModel>(customerId);

            Assert.Equal("ivan", customer.FirstName);
            Assert.Equal("1234567890", customer.EGN);
            Assert.Equal("ivanov", customer.LastName);
        }

        [Fact]
        public async Task ExistAsyncShouldReturnTrueWhenExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new EmployeesService(userRepo);

            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                EGN = "1234567890",
            });
            await userRepo.SaveChangesAsync();

            var customerId = (await userRepo.AllAsNoTracking()
                .FirstOrDefaultAsync()).Id;

            Assert.True(await service.ExistAsync(customerId));
        }

        [Fact]
        public async Task ExistAsyncShouldReturnFalseWhenDoesNotExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new EmployeesService(userRepo);

            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                EGN = "1234567890",
            });
            await userRepo.SaveChangesAsync();

            Assert.False(await service.ExistAsync(Guid.NewGuid().ToString()));
        }

        [Fact]
        public async Task EditAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new EmployeesService(userRepo);

            await userRepo.AddAsync(new ApplicationUser
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                EGN = "1234567890",
            });
            await userRepo.SaveChangesAsync();

            var customerId = (await userRepo.AllAsNoTracking()
                .FirstOrDefaultAsync()).Id;

            var customer = (await service.GetByIdAsync<EmployeeModel>(customerId)).To<EmployeeModel>();
            var newName = "Pesho";
            var newEGN = "0987654321";
            customer.FirstName = newName;
            customer.EGN = newEGN;

            await service.EditAsync<EmployeeModel>(customer);

            customer = (await service.GetByIdAsync<EmployeeModel>(customerId)).To<EmployeeModel>();

            Assert.Equal(newName, customer.FirstName);
            Assert.Equal(newEGN, customer.EGN);
        }
    }
}
