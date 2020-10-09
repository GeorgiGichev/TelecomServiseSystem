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

            var numbers = new List<ServiseNumber>();

            for (int i = 866000001; i <= 866000999; i++)
            {
                numbers.Add(new ServiseNumber() { Number = i.ToString() });
            }

            for (int i = 100001; i < 100999; i++)
            {
                numbers.Add(new ServiseNumber() { Number = "N." + i });
                numbers.Add(new ServiseNumber() { Number = "TV." + i });
            }

            await dbContext.ServiceNumbers.AddRangeAsync(numbers);
        }
    }
}
