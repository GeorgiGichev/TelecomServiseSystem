namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data.Models;

    public class TeamSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Teams.Any())
            {
                return;
            }

            var team = new Team
            {
                CityId = (await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Varna")).Id,
            };

            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
        }
    }
}
