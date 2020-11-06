namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using TelecomServiceSystem.Data.Models;

    internal class SimSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.SimCards.Any())
            {
                return;
            }

            var simCards = new List<SimCard>();
            var simICC = "893590322612345678";
            for (int i = 1000; i < 9999; i++)
            {
                simCards.Add(new SimCard
                {
                    ICC = simICC + i.ToString(),
                });
            }

            await dbContext.SimCards.AddRangeAsync(simCards);
            await dbContext.SaveChangesAsync();
        }
    }
}
