namespace TelecomServiceSystem.Services.Data.Teams
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITeamsService
    {
        Task<IEnumerable<T>> GetByCityId<T>(int cityId);

        Task CreateAsync(string employeeId, int cityId);

        Task AddEmployee(int teamId, string employeeId);
    }
}
