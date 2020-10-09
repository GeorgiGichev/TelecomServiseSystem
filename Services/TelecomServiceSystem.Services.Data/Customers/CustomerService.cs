namespace TelecomServiceSystem.Services.Data.Customers
{
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

        public async Task<IEnumerable<TOutput>> GetBySearchCriteriaAsync<TOutput, TQuery>(TQuery query)
        {
            IQueryable<Customer> customers = this.customerRepo.All();

            var customer = query.To<InputCustomerSearchModel>();

            if (customer.FirstName != null)
            {
                customers = customers.Where(c => c.FirstName == customer.FirstName);
            }

            if (customer.MiddleName != null)
            {
                customers = customers.Where(c => c.MiddleName == customer.MiddleName);
            }

            if (customer.LastName != null)
            {
                customers = customers.Where(c => c.LastName == customer.LastName);
            }

            if (customer.PersonalNumber != null)
            {
                customers = customers.Where(c => c.PersonalNumber == customer.PersonalNumber);
            }

            var result = await customers.To<TOutput>()
                .ToListAsync();

            return result;
        }
    }
}
