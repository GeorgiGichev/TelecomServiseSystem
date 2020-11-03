namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data.Models;

    public class CitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cities.Any())
            {
                return;
            }

            var cities = new List<City>
            {
                new City
                {
                    Name = "Varna",
                    CountryId = (await dbContext.Countries.FirstOrDefaultAsync(x => x.Name == "Bulgaria")).Id,
                },
                new City
                {
                    Name = "Sofia",
                    CountryId = (await dbContext.Countries.FirstOrDefaultAsync(x => x.Name == "Bulgaria")).Id,
                },
                new City
                {
                    Name = "Burgas",
                    CountryId = (await dbContext.Countries.FirstOrDefaultAsync(x => x.Name == "Bulgaria")).Id,
                },
                new City
                {
                    Name = "Plovdiv",
                    CountryId = (await dbContext.Countries.FirstOrDefaultAsync(x => x.Name == "Bulgaria")).Id,
                },
                new City
                {
                    Name = "Stara Zagora",
                    CountryId = (await dbContext.Countries.FirstOrDefaultAsync(x => x.Name == "Bulgaria")).Id,
                },
            };

            await dbContext.Cities.AddRangeAsync(cities);
            await dbContext.SaveChangesAsync();
        }
    }
}
