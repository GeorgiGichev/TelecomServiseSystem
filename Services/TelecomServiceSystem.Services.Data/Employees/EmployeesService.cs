    namespace TelecomServiceSystem.Services.Data.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class EmployeesService : IEmployeesService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepo;

        public EmployeesService(IDeletableEntityRepository<ApplicationUser> usersRepo)
        {
            this.usersRepo = usersRepo;
        }

        public async Task Edit<T>(T input)
        {
            var employeeToEdit = input.To<ApplicationUser>();
            var employee = await this.GetByIdAsync<ApplicationUser>(employeeToEdit.Id);
            await this.usersRepo.UpdateModel(employee, input);
        }

        public async Task<T> GetByIdAsync<T>(string id)
            => (await this.usersRepo.All().FirstOrDefaultAsync(e => e.Id == id)).To<T>();

        public async Task<IEnumerable<TOutput>> GetBySearchCriteriaAsync<TOutput, TQuery>(TQuery query)
        {
            IQueryable<ApplicationUser> employees = null;

            var employee = query.To<InputEmployeeSerchModel>();

            if (!employee.IsDeleted.HasValue || employee.IsDeleted == false)
            {
                employees = this.usersRepo.All();
            }
            else
            {
                employees = this.usersRepo.AllWithDeleted()
                    .Where(u => u.IsDeleted == true);
            }

            if (employee.FirstName != null)
            {
                employees = employees.Where(e => e.FirstName == employee.FirstName);
            }

            if (employee.MiddleName != null)
            {
                employees = employees.Where(e => e.MiddleName == employee.MiddleName);
            }

            if (employee.LastName != null)
            {
                employees = employees.Where(e => e.LastName == employee.LastName);
            }

            if (employee.EGN != null)
            {
                employees = employees.Where(e => e.EGN == employee.EGN);
            }

            var result = await employees.To<TOutput>()
                .ToListAsync();

            return result;
        }
    }
}
