namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using TelecomServiceSystem.Data.Models;

    internal class AddresSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Addresses.Any())
            {
                return;
            }

            var addresses = new List<Address>()
            {
                new Address
                {
                    Country = "Bulgaria",
                    City = "Varna",
                    Street = "Sv. Sv. Kiril i Metodii",
                    Neighborhood = "Asparuhovo",
                    Number = 41,
                    CustomerId = (await dbContext.Customers.FirstOrDefaultAsync(c => c.FirstName == "Георги")).Id,
                },
                new Address
                {
                    Country = "Bulgaria",
                    City = "Varna",
                    Street = "Narodni buditeli",
                    Neighborhood = "Asparuhovo",
                    Number = 80,
                    Entrance = "A",
                    Floor = 3,
                    Apartment = 20,
                    CustomerId = (await dbContext.Customers.FirstOrDefaultAsync(c => c.FirstName == "Георги")).Id,
                },
                new Address
                {
                    Country = "Bulgaria",
                    City = "Varna",
                    Street = "3-ta",
                    Neighborhood = "Druzhba",
                    Number = 50,
                    CustomerId = (await dbContext.Customers.FirstOrDefaultAsync(c => c.FirstName == "Иван")).Id,
                },
            };
            await dbContext.Addresses.AddRangeAsync(addresses);
            await dbContext.SaveChangesAsync();
        }
    }
}
