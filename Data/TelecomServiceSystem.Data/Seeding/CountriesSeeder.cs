namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Models;

    public class CountriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Countries.Any())
            {
                return;
            }

            var country = new Country
            {
                Name = "Bulgaria",
            };

            await dbContext.Countries.AddAsync(country);
            await dbContext.SaveChangesAsync();
        }
    }
}
