using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelecomServiceSystem.Data.Models;

namespace TelecomServiceSystem.Data.Seeding
{
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
                AddressId = 1,
                CreatedOn = DateTime.UtcNow,
                UserName = "Ivan.Ivanov",
                PasswordHash = "123456",
                Email = "Ivan.Ivanov@TSS.com",
                EmailConfirmed = true,
            });
        }
    }
}
