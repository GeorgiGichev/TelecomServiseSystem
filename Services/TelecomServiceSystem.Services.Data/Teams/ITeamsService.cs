namespace TelecomServiceSystem.Services.Data.Teams
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Models;

    public interface ITeamsService
    {
        Task<IEnumerable<T>> GetByCityIdAsync<T>(int cityId);

        Task CreateAsync(string employeeId, int cityId);

        Task AddEmployeeAsync(int teamId, string employeeId);

        Task<T> GetFreeTeamByCityIdAsync<T>(int cityId);

        Task<IEnumerable<T>> GetAllTeamsAsync<T>();

        Task AddSlotsToTeamsAsync();
    }
}
