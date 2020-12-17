namespace TelecomServiceSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Data;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Repositories;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Services.Data.Tests.TestsModels;
    using Xunit;

    public class AddressServiceTests : TestsBase
    {
        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 1,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
                IsMainAddress = true,
            });

            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 2,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
                IsMainAddress = true,
            });

            var addressesInDbCount = addressRepo.All().ToList().Count();
            var address1 = await addressRepo.All().FirstOrDefaultAsync(x => x.Id == 1);
            var address2 = await addressRepo.All().FirstOrDefaultAsync(x => x.Id == 2);
            Assert.Equal(2, addressesInDbCount);
            Assert.Equal(1, address1.Id);
            Assert.Equal(1, address1.CityId);
            Assert.Equal("Test", address1.Street);
            Assert.Equal("NTest", address1.Neighborhood);
            Assert.Equal(1, address1.Number);
            Assert.False(address1.IsMainAddress);
            Assert.True(address2.IsMainAddress);
        }

        [Fact]
        public async Task GetByIdAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 1,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
            });

            var address = await service.GetByIdAsync<AddressModel>(1);

            Assert.Equal(1, address.Id);
            Assert.Equal(1, address.CityId);
            Assert.Equal("Test", address.Street);
            Assert.Equal("NTest", address.Neighborhood);
            Assert.Equal(1, address.Number);
        }

        [Fact]
        public async Task GetByCustomerIdAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            var customerId = Guid.NewGuid().ToString();
            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 1,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
                CustomerId = customerId,
            });

            var addresses = await service.GetByCustomerIdAsync<AddressModel>(customerId);

            Assert.Single<AddressModel>(addresses);
            Assert.Collection<AddressModel>(
                addresses,
                x => Assert.Equal(customerId, x.CustomerId));
        }

        [Fact]
        public async Task EditAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            var customerId = Guid.NewGuid().ToString();
            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 1,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
                CustomerId = customerId,
                IsMainAddress = true,
            });
            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 2,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
                CustomerId = customerId,
                IsMainAddress = false,
            });

            var addressForUpdate = await service.GetByIdAsync<AddressModel>(2);

            addressForUpdate.Street = "ASD";
            addressForUpdate.IsMainAddress = true;
            await service.EditAsync<AddressModel>(addressForUpdate);
            var address = await service.GetByIdAsync<AddressModel>(2);
            var address2 = await service.GetByIdAsync<AddressModel>(1);
            Assert.Equal("ASD", address.Street);
            Assert.True(address.IsMainAddress);
            Assert.False(address2.IsMainAddress);
        }

        [Fact]
        public async Task EditAsyncShouldSetIsMainAsTrueWhenNoMain()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            var customerId = Guid.NewGuid().ToString();
            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 1,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
                CustomerId = customerId,
                IsMainAddress = false,
            });
            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 2,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
                CustomerId = customerId,
                IsMainAddress = false,
            });

            var addressForUpdate = await service.GetByIdAsync<AddressModel>(2);

            addressForUpdate.Street = "ASD";
            addressForUpdate.IsMainAddress = true;
            await service.EditAsync<AddressModel>(addressForUpdate);
            var address = await service.GetByIdAsync<AddressModel>(2);
            Assert.Equal("ASD", address.Street);
            Assert.True(address.IsMainAddress);
        }

        [Fact]
        public async Task AddNewCityAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            await countryRepo.AddAsync(new Country
            {
                Name = GlobalConstants.CountryOfUsing,
            });
            await countryRepo.SaveChangesAsync();

            var countryId = (await countryRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == GlobalConstants.CountryOfUsing)).Id;

            await service.AddNewCityAsync<CityModel>(new CityModel()
            {
                Name = "Varna",
            });

            Assert.Equal(1, cityRepo.All().Count());
        }

        [Fact]
        public async Task CityExistAsyncShouldReturnTrueWhenCityExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            await countryRepo.AddAsync(new Country
            {
                Name = GlobalConstants.CountryOfUsing,
            });
            await countryRepo.SaveChangesAsync();

            var countryId = (await countryRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == GlobalConstants.CountryOfUsing)).Id;

            await service.AddNewCityAsync<CityModel>(new CityModel()
            {
                Name = "Varna",
            });

            Assert.True(await service.CityExistAsync(1));
        }

        [Fact]
        public async Task CityExistAsyncShouldReturnFalseWhenCityDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            await countryRepo.AddAsync(new Country
            {
                Name = GlobalConstants.CountryOfUsing,
            });
            await countryRepo.SaveChangesAsync();

            var countryId = (await countryRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == GlobalConstants.CountryOfUsing)).Id;

            await service.AddNewCityAsync<CityModel>(new CityModel()
            {
                Name = "Varna",
            });

            Assert.False(await service.CityExistAsync(2));
        }

        [Fact]
        public async Task GetCityIdByAddressIdAsyncShouldReturnCorrectCityId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            await countryRepo.AddAsync(new Country
            {
                Name = GlobalConstants.CountryOfUsing,
            });
            await countryRepo.SaveChangesAsync();

            var countryId = (await countryRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == GlobalConstants.CountryOfUsing)).Id;

            await service.AddNewCityAsync<CityModel>(new CityModel()
            {
                Name = "Varna",
            });

            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 1,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
            });

            Assert.Equal(1, await service.GetCityIdByAddressIdAsync(1));
        }

        [Fact]
        public async Task GetCityIdByAddressIdAsyncShouldReturn0WhenAddressIdDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            await countryRepo.AddAsync(new Country
            {
                Name = GlobalConstants.CountryOfUsing,
            });
            await countryRepo.SaveChangesAsync();

            var countryId = (await countryRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == GlobalConstants.CountryOfUsing)).Id;

            await service.AddNewCityAsync<CityModel>(new CityModel()
            {
                Name = "Varna",
            });

            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 1,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
            });

            Assert.Equal(0, await service.GetCityIdByAddressIdAsync(2));
        }

        [Fact]
        public async Task GetCitiesByCountryAsyncShouldReturn0WhenAddressIdDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            await cityRepo.AddAsync(new City
            {
                Name = "Varna",
                Country = new Country
                {
                    Name = GlobalConstants.CountryOfUsing,
                },
            });
            await cityRepo.SaveChangesAsync();

            var cities = await service.GetCitiesByCountryAsync<CityModel>(GlobalConstants.CountryOfUsing);
            Assert.Collection(
                cities,
                x => Assert.Equal("Varna", x.Name));
            Assert.Single(cities);
        }

        [Fact]
        public async Task GetMainAddressAsyncShouldReturnMainAddress()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            var customerId = Guid.NewGuid().ToString();
            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 1,
                CityId = 1,
                Street = "ASD",
                Neighborhood = "NTest",
                Number = 1,
                CustomerId = customerId,
                IsMainAddress = true,
            });
            await service.CreateAsync<AddressModel>(new AddressModel()
            {
                Id = 2,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
                CustomerId = customerId,
                IsMainAddress = false,
            });

            var address = await service.GetMainAddressAsync<AddressModel>(customerId);

            Assert.Equal("ASD", address.Street);
            Assert.True(address.IsMainAddress);
        }

        [Fact]
        public async Task GetByServiceInfoIdAsyncShouldReturnMainAddress()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            var customerId = Guid.NewGuid().ToString();
            await addressRepo.AddAsync(new Address()
            {
                Id = 1,
                CityId = 1,
                Street = "ASD",
                Neighborhood = "NTest",
                Number = 1,
                CustomerId = customerId,
                IsMainAddress = true,
                ServicesInfos = new HashSet<ServiceInfo> { new ServiceInfo() },
            });
            await addressRepo.AddAsync(new Address()
            {
                Id = 2,
                CityId = 1,
                Street = "Test",
                Neighborhood = "NTest",
                Number = 1,
                CustomerId = customerId,
                IsMainAddress = false,
            });
            await addressRepo.SaveChangesAsync();
            var address = await service.GetByServiceInfoIdAsync<AddressModel>(1);

            Assert.Equal("ASD", address.Street);
            Assert.True(address.IsMainAddress);
        }

        [Fact]
        public async Task GetAllCitiesAsyncGetAllCitiesAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            await cityRepo.AddAsync(new City
            {
                Name = "Varna",
                Country = new Country
                {
                    Name = GlobalConstants.CountryOfUsing,
                },
            });
            await cityRepo.SaveChangesAsync();

            var cities = await service.GetAllCitiesAsync<CityModel>();
            Assert.Collection(
                cities,
                x => Assert.Equal("Varna", x.Name));
            Assert.Single(cities);
        }

        [Fact]
        public async Task GetAllCitiesAsyncGetAllCitiesAsyncShouldReturnEmptyCollectionWhenRepoIsEmpty()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            var cities = await service.GetAllCitiesAsync<CityModel>();

            Assert.Empty(cities);
        }

        [Fact]
        public async Task DeleteCityAsyncGetAllCitiesAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var addressRepo = new EfDeletableEntityRepository<Address>(dbContext);
            var countryRepo = new EfDeletableEntityRepository<Country>(dbContext);
            var cityRepo = new EfDeletableEntityRepository<City>(dbContext);

            var service = new AddressService(addressRepo, countryRepo, cityRepo);

            await cityRepo.AddAsync(new City
            {
                Name = "Varna",
                Country = new Country
                {
                    Name = GlobalConstants.CountryOfUsing,
                },
            });
            await cityRepo.AddAsync(new City
            {
                Name = "Sofia",
                Country = new Country
                {
                    Name = GlobalConstants.CountryOfUsing,
                },
            });
            await cityRepo.SaveChangesAsync();
            await service.DeleteCityAsync(2);
            var cities = await service.GetAllCitiesAsync<CityModel>();
            Assert.Collection(
                cities,
                x => Assert.Equal("Varna", x.Name));
            Assert.Single(cities);
        }
    }
}
