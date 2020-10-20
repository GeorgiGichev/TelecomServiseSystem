namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Models;

    internal class ServiceNumbersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ServiceNumbers.Any())
            {
                return;
            }

            var numbers = new List<ServiceNumber>();

            for (int i = 866000001; i <= 866000999; i++)
            {
                numbers.Add(new ServiceNumber() { Number = i.ToString(), IsFree = true });
            }

            for (int i = 100001; i < 100999; i++)
            {
                numbers.Add(new ServiceNumber() { Number = "N." + i, IsFree = true });
                numbers.Add(new ServiceNumber() { Number = "TV." + i, IsFree = true });
            }

            await dbContext.ServiceNumbers.AddRangeAsync(numbers);
        }
    }
}
