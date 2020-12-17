namespace TelecomServiceSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Repositories;
    using TelecomServiceSystem.Services.Data.Customers;
    using TelecomServiceSystem.Services.Data.Tests.TestsModels;
    using Xunit;

    public class CustomerServiceTests : TestsBase
    {
        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var customerRepo = new EfDeletableEntityRepository<Customer>(dbContext);

            var service = new CustomerService(customerRepo);

            var id = await service.CreateAsync<CustomerModel>(new CustomerModel
            {
                FirstName = "ivan",
                LastName = "ivanov",
            });

            var customer = await customerRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            Assert.Equal(id, customer.Id);
            Assert.Equal("ivan", customer.FirstName);
            Assert.Equal("ivanov", customer.LastName);
        }

        [Fact]
        public async Task EditAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var customerRepo = new EfDeletableEntityRepository<Customer>(dbContext);

            var service = new CustomerService(customerRepo);

            var id = await service.CreateAsync<CustomerModel>(new CustomerModel
            {
                FirstName = "ivan",
                LastName = "ivanov",
            });

            var model = new CustomerEditModel
            {
                Id = id,
                FirstName = "Dragan",
                LastName = "Draganov",
            };

            await service.EditAsync<CustomerEditModel>(model);

            var customer = await customerRepo.All()
               .FirstOrDefaultAsync(x => x.Id == id);

            Assert.Equal(id, customer.Id);
            Assert.Equal("Dragan", customer.FirstName);
            Assert.Equal("Draganov", customer.LastName);
        }

        [Fact]
        public async Task ExistAsyncShouldReturnTrueWhenCustomerExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var customerRepo = new EfDeletableEntityRepository<Customer>(dbContext);

            var service = new CustomerService(customerRepo);

            var id = await service.CreateAsync<CustomerModel>(new CustomerModel
            {
                FirstName = "ivan",
                LastName = "ivanov",
            });

            Assert.True(await service.ExistAsync(id));
        }

        [Fact]
        public async Task ExistAsyncShouldReturnFalseWhenCustomerDoesNotExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var customerRepo = new EfDeletableEntityRepository<Customer>(dbContext);

            var service = new CustomerService(customerRepo);

            var id = await service.CreateAsync<CustomerModel>(new CustomerModel
            {
                FirstName = "ivan",
                LastName = "ivanov",
            });

            Assert.False(await service.ExistAsync(Guid.NewGuid().ToString()));
        }

        [Fact]
        public async Task GetAllForBillingAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var customerRepo = new EfDeletableEntityRepository<Customer>(dbContext);

            var service = new CustomerService(customerRepo);

            await customerRepo.AddAsync(new Customer
            {
                FirstName = "Ivan",
                ServicesInfo = new HashSet<ServiceInfo>
                {
                    new ServiceInfo
                    {
                        Id = 1,
                        IsActive = true,
                    },
                },
            });
            await customerRepo.SaveChangesAsync();

            var customers = await service.GetAllForBillingAsync<CustomerModel>();

            Assert.Single(customers);
        }

        [Fact]
        public async Task GetByIdAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var customerRepo = new EfDeletableEntityRepository<Customer>(dbContext);

            var service = new CustomerService(customerRepo);

            var name = "ivan";
            var lastName = "ivanov";
            var id = await service.CreateAsync<CustomerModel>(new CustomerModel
            {
                FirstName = name,
                LastName = lastName,
            });

            var customer = await service.GetByIdAsync<CustomerModel>(id);

            Assert.Equal(name, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
        }

        [Fact]
        public async Task GetBySearchCriteriaAsyncShouldWorkCorrectlyWithFirstName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var customerRepo = new EfDeletableEntityRepository<Customer>(dbContext);

            var service = new CustomerService(customerRepo);

            var id = await service.CreateAsync<CustomerModel>(new CustomerModel
            {
                FirstName = "ivan",
                LastName = "ivanov",
                PersonalNumber = "1234567890",
            });

            var model = new CustomerSearchModel
            {
                FirstName = "ivan",
            };

            var customers = await service.GetBySearchCriteriaAsync<CustomerModel, CustomerSearchModel>(model);

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

            var customerRepo = new EfDeletableEntityRepository<Customer>(dbContext);

            var service = new CustomerService(customerRepo);

            var id = await service.CreateAsync<CustomerModel>(new CustomerModel
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                PersonalNumber = "1234567890",
            });

            var model = new CustomerSearchModel
            {
                MiddleName = "Ivanov",
            };

            var customers = await service.GetBySearchCriteriaAsync<CustomerModel, CustomerSearchModel>(model);

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

            var customerRepo = new EfDeletableEntityRepository<Customer>(dbContext);

            var service = new CustomerService(customerRepo);

            var id = await service.CreateAsync<CustomerModel>(new CustomerModel
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                PersonalNumber = "1234567890",
            });

            var model = new CustomerSearchModel
            {
                LastName = "Ivanov",
            };

            var customers = await service.GetBySearchCriteriaAsync<CustomerModel, CustomerSearchModel>(model);

            Assert.Single(customers);
            Assert.Collection(
                customers,
                x => Assert.Equal("ivanov", x.LastName));
        }

        [Fact]
        public async Task GetBySearchCriteriaAsyncShouldWorkCorrectlyWithPersonalNumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var customerRepo = new EfDeletableEntityRepository<Customer>(dbContext);

            var service = new CustomerService(customerRepo);

            var id = await service.CreateAsync<CustomerModel>(new CustomerModel
            {
                FirstName = "ivan",
                LastName = "ivanov",
                MiddleName = "ivanov",
                PersonalNumber = "1234567890",
            });

            var model = new CustomerSearchModel
            {
                PersonalNumber = "1234567890",
            };

            var customers = await service.GetBySearchCriteriaAsync<CustomerModel, CustomerSearchModel>(model);

            Assert.Single(customers);
            Assert.Collection(
                customers,
                x => Assert.Equal("1234567890", x.PersonalNumber));
        }

        [Fact]
        public async Task DeleteAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var customerRepo = new EfDeletableEntityRepository<Customer>(dbContext);

            var service = new CustomerService(customerRepo);
            var customerId = Guid.NewGuid().ToString();
            await customerRepo.AddAsync(new Customer
            {
                Id = customerId,
                FirstName = "Ivan",
            });
            await customerRepo.AddAsync(new Customer
            {
                FirstName = "Dragan",
            });
            await customerRepo.SaveChangesAsync();
            await service.DeleteAsync(customerId);
            var customers = await customerRepo.All().ToListAsync();

            Assert.Single(customers);
            Assert.DoesNotContain(customers, x => x.Id == customerId);
        }
    }
}
