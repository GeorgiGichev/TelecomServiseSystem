namespace TelecomServiceSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;

    public class CustomerSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Customers.Any())
            {
                return;
            }

            var customers = new List<Customer>
            {
                new Customer
                {
                    FirstName = "Георги",
                    MiddleName = "Динков",
                    LastName = "Гичев",
                    DocumentType = Enum.Parse<DocumentType>("IdCard"),
                    PersonalNumber = "8709218694",
                    Email = "georgi.gichev@gmail.com",
                    Phone = "0876954785",
                },
                new Customer
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    DocumentType = Enum.Parse<DocumentType>("IdCard"),
                    PersonalNumber = "8804218694",
                    Email = "ivan.ivanov@gmail.com",
                    Phone = "0888663322",
                },
            };

            await dbContext.Customers.AddRangeAsync(customers);
            await dbContext.SaveChangesAsync();
        }
    }
}
