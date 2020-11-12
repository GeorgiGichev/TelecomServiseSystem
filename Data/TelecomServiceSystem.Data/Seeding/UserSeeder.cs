namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Data.Models;

    internal class UserSeeder : ISeeder
    {
        private const string UsersPassword = "123456";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (dbContext.Users.Any())
            {
                return;
            }

            var seller = new ApplicationUser
            {
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = "Ivanov",
                EGN = "1234567890",
                CreatedOn = DateTime.UtcNow,
                UserName = "Ivan.Ivanov",
                Email = "Ivan.Ivanov@TSS.com",
                EmailConfirmed = true,
                CityId = (await dbContext.Cities.FirstOrDefaultAsync()).Id,
            };

            var admin = new ApplicationUser
            {
                FirstName = "Georgi",
                MiddleName = "Dinkov",
                LastName = "Gichev",
                EGN = "1234567899",
                CreatedOn = DateTime.UtcNow,
                UserName = "Georgi.Gichev",
                Email = "Georgi.Gichev@TSS.com",
                EmailConfirmed = true,
                CityId = (await dbContext.Cities.FirstOrDefaultAsync()).Id,
            };
            var engineer = new ApplicationUser
            {
                FirstName = "Petyr",
                MiddleName = "P",
                LastName = "Petrov",
                EGN = "1234567898",
                CreatedOn = DateTime.UtcNow,
                UserName = "Petyr.Petrov",
                NormalizedUserName = "Petyr.Petrov",
                Email = "Petyr.Petrov@TSS.com",
                NormalizedEmail = "Petyr.Petrov@TSS.com",
                TeamId = (await dbContext.Teams.FirstOrDefaultAsync(x => x.Employees.Count < 2)).Id,
                EmailConfirmed = true,
                CityId = (await dbContext.Cities.FirstOrDefaultAsync()).Id,
            };

            await SeedUser(userManager, admin, UsersPassword, GlobalConstants.AdministratorRoleName);
            await SeedUser(userManager, seller, UsersPassword, GlobalConstants.SellerRoleName);
            await SeedUser(userManager, engineer, UsersPassword, GlobalConstants.EngineerRoleName);
        }

        private static async Task SeedUser(UserManager<ApplicationUser> userManager, ApplicationUser user, string password, string roleName)
        {
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}
