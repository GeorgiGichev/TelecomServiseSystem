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

        public async Task DeleteAsync(string employeeId)
        {
            var employee = await this.usersRepo.All()
                .FirstOrDefaultAsync(x => x.Id == employeeId);
            this.usersRepo.Delete(employee);
            await this.usersRepo.SaveChangesAsync();
        }

        public async Task EditAsync<T>(T input)
        {
            var employeeToEdit = input.To<ApplicationUser>();
            var employee = await this.GetByIdAsync<ApplicationUser>(employeeToEdit.Id);
            await this.usersRepo.UpdateModel(employee, input);
        }

        public async Task<bool> ExistAsync(string id)
        {
            return await this.usersRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == id) == null ? false : true;
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
                employees = employees.Where(e => e.FirstName.ToLower().Contains(employee.FirstName.ToLower()));
            }

            if (employee.MiddleName != null)
            {
                employees = employees.Where(e => e.MiddleName.ToLower().Contains(employee.MiddleName.ToLower()));
            }

            if (employee.LastName != null)
            {
                employees = employees.Where(e => e.LastName.ToLower().Contains(employee.LastName.ToLower()));
            }

            if (employee.EGN != null)
            {
                employees = employees.Where(e => e.EGN.Contains(employee.EGN));
            }

            var result = await employees.To<TOutput>()
                .ToListAsync();

            return result;
        }
    }
}
