namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using TelecomServiceSystem.Data.Models;

    internal class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = "Ivanov",
                EGN = "1234567890",
                CreatedOn = DateTime.UtcNow,
                UserName = "Ivan.Ivanov",
                PasswordHash = "AQAAAAEAACcQAAAAELwLwT4BpdoajXL5B8b69UV6WcWbXaGZEW6AD1jpljAyrn1eDbFLARjr1xZ8UjrvUg==",
                Email = "Ivan.Ivanov@TSS.com",
                EmailConfirmed = true,
            });
        }
    }
}
