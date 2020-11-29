namespace TelecomServiceSystem.Services.Data.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class CustomerService : ICustomerService
    {
        private readonly IDeletableEntityRepository<Customer> customerRepo;

        public CustomerService(IDeletableEntityRepository<Customer> customerRepo)
        {
            this.customerRepo = customerRepo;
        }

        public async Task<string> CreateAsync<T>(T input)
        {
            var customerToAdd = input.To<Customer>();
            await this.customerRepo.AddAsync(customerToAdd);
            await this.customerRepo.SaveChangesAsync();
            return customerToAdd.Id;
        }

        public async Task Edit<T>(T input)
        {
            var customerToEdit = input.To<Customer>();
            var customer = await this.GetByIdAsync<Customer>(customerToEdit.Id);
            await this.customerRepo.UpdateModel(customer, input);
        }

        public async Task<bool> Exist(string id)
        {
            return await this.customerRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) == null ?
                false : true;
        }

        public async Task<IEnumerable<T>> GetAllForBilling<T>()
        {
            return await this.customerRepo.All().Where(x => x.ServicesInfo.Any(y => y.IsActive)).To<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(string id)
            => (await this.customerRepo.All().FirstOrDefaultAsync(c => c.Id == id)).To<T>();

        public async Task<IEnumerable<TOutput>> GetBySearchCriteriaAsync<TOutput, TQuery>(TQuery query)
        {
            IQueryable<Customer> customers = this.customerRepo.All();

            var customer = query.To<InputCustomerSearchModel>();

            if (customer.FirstName != null)
            {
                customers = customers.Where(c => c.FirstName.ToLower().Contains(customer.FirstName.ToLower()));
            }

            if (customer.MiddleName != null)
            {
                customers = customers.Where(c => c.MiddleName.ToLower().Contains(customer.MiddleName.ToLower()));
            }

            if (customer.LastName != null)
            {
                customers = customers.Where(c => c.LastName.ToLower().Contains(customer.LastName.ToLower()));
            }

            if (customer.PersonalNumber != null)
            {
                customers = customers.Where(c => c.PersonalNumber.ToLower().Contains(customer.PersonalNumber.ToLower()));
            }

            var result = await customers.To<TOutput>()
                .ToListAsync();

            return result;
        }
    }
}
