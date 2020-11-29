namespace TelecomServiceSystem.Services.Data.Addresses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class AddressService : IAddressService
    {
        private readonly IDeletableEntityRepository<Address> addressRepo;
        private readonly IDeletableEntityRepository<Country> countryRepo;
        private readonly IDeletableEntityRepository<City> cityRepo;

        public AddressService(IDeletableEntityRepository<Address> addressRepo, IDeletableEntityRepository<Country> countryRepo, IDeletableEntityRepository<City> cityRepo)
        {
            this.addressRepo = addressRepo;
            this.countryRepo = countryRepo;
            this.cityRepo = cityRepo;
        }

        public async Task CreateAsync<T>(T model)
        {
            var address = model.To<Address>();
            await this.addressRepo.AddAsync(address);
            await this.addressRepo.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
            => await this.addressRepo.All()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetByCustomerIdAsync<T>(string customerId)
        {
            var addreses = await this.addressRepo.All()
                .Where(a => a.CustomerId == customerId)
                .To<T>().ToListAsync();

            return addreses;
        }

        public async Task EditAsync<T>(T input)
        {
            var addressInput = input.To<Address>();
            var mainAddress = await this.addressRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.IsMainAddress);
            if (mainAddress != null && addressInput.IsMainAddress)
            {
                addressInput.IsMainAddress = false;
            }

            var address = await this.addressRepo.All()
                .FirstOrDefaultAsync(a => a.Id == addressInput.Id);
            await this.addressRepo.UpdateModel(address, addressInput);
        }

        public async Task<IEnumerable<T>> GetByCustomerId<T>(string customerId)
        {
            return await this.addressRepo.All()
                .Where(x => x.CustomerId == customerId)
                .To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetCitiesByCountryAsync<T>(string country)
        {
            return await this.cityRepo.All()
                .Where(x => x.Country.Name == country)
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllCountries<T>()
        {
            return await this.countryRepo.All()
                .To<T>()
                .ToListAsync();
        }

        public async Task<int> GetCityIdByAddressId(int addressId)
        {
            var address = await this.addressRepo.All()
                .FirstOrDefaultAsync(x => x.Id == addressId);
            return address.CityId;
        }

        public async Task AddNewCity<T>(T model)
        {
            var city = model.To<City>();
            city.CountryId = (await this.countryRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == GlobalConstants.CountryOfUsing)).Id;
            if (await this.cityRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Name.ToLower() == city.Name.ToLower()) == null)
            {
                await this.cityRepo.AddAsync(city);
                await this.cityRepo.SaveChangesAsync();
            }
        }

        public async Task<bool> CityExist(int id)
        {
            return await this.cityRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) == null ? false : true;
        }
    }
}
