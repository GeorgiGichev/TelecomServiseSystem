namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
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
                InstalationSlots = this.GetInstalationSlots() as ICollection<InstalationSlot>,
            };

            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
        }

        private IEnumerable<InstalationSlot> GetInstalationSlots()
        {
            var year = DateTime.UtcNow.Year;
            var startDate = DateTime.UtcNow == DateTime.Parse($"31/12/{year}")
                ? DateTime.Parse($"01/01/{year + 1}")
                : DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow == DateTime.Parse($"31/12/{year}")
                ? DateTime.Parse($"31/12/{year + 1}")
                : DateTime.Parse($"31/12/{year}");
            var slots = new HashSet<InstalationSlot>();

            while (startDate <= endDate)
            {
                if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    slots.Add(new InstalationSlot
                    {
                        StartingTime = DateTime.Parse($"{startDate.ToShortDateString()} 08:00:00"),
                        EndingTime = DateTime.Parse($"{startDate.ToShortDateString()} 12:00:00"),
                    });

                    slots.Add(new InstalationSlot
                    {
                        StartingTime = DateTime.Parse($"{startDate.ToShortDateString()} 12:00:00"),
                        EndingTime = DateTime.Parse($"{startDate.ToShortDateString()} 16:00:00"),
                    });

                    slots.Add(new InstalationSlot
                    {
                        StartingTime = DateTime.Parse($"{startDate.ToShortDateString()} 16:00:00"),
                        EndingTime = DateTime.Parse($"{startDate.ToShortDateString()} 18:00:00"),
                    });
                }

                startDate = startDate.AddDays(1);
            }

            return slots;
        }
    }
}
