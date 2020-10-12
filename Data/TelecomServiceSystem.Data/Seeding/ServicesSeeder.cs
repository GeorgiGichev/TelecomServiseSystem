namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;

    internal class ServicesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Services.Any())
            {
                return;
            }

            var services = new List<Service>()
            {
                new Service
                {
                    Name = "Unlimited S",
                    ServiceType = (ServiceType)1,
                    Price = 20,
                },
                new Service
                {
                    Name = "Data S",
                    ServiceType = (ServiceType)1,
                    Price = 10,
                },
                new Service
                {
                    Name = "Net 50",
                    ServiceType = (ServiceType)2,
                    Price = 15,
                },
                new Service
                {
                    Name = "Tv S",
                    ServiceType = (ServiceType)2,
                    Price = 10,
                },
            };

            await dbContext.Services.AddRangeAsync(services);
        }
    }
}
