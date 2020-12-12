namespace TelecomServiceSystem.Services.Data.Employees
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmployeesService
    {
        Task<IEnumerable<TOutput>> GetBySearchCriteriaAsync<TOutput, TQuery>(TQuery employee);

        Task<T> GetByIdAsync<T>(string id);

        Task EditAsync<T>(T input);

        Task<bool> ExistAsync(string id);

        Task Delete(string employeeId);
    }
}
