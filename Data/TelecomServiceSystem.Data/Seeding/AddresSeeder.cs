namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
                    City = "Varna",
                    Street = "Sv. Sv. Kiril i Metodii",
                    Number = 41,
                },
                new Address
                {
                    City = "Varna",
                    Street = "Narodni buditeli",
                    Number = 80,
                    Entrance = "A",
                    Floor = 3,
                    Apartment = 20,
                },
                new Address
                {
                    City = "Varna",
                    Neighborhood = "Druzhba",
                    Number = 50,
                },
            };
            await dbContext.Addresses.AddRangeAsync(addresses);
        }
    }
}
