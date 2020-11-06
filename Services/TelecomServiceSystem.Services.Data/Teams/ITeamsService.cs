namespace TelecomServiceSystem.Services.Data.Teams
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Models;

    public interface ITeamsService
    {
        Task<IEnumerable<T>> GetByCityId<T>(int cityId);

        Task CreateAsync(string employeeId, int cityId);

        Task AddEmployee(int teamId, string employeeId);

        Task<T> GetFreeTeamByCityId<T>(int cityId);

        Task<IEnumerable<T>> GetAllTeams<T>();

        Task AddSlotsToTeams();
    }
}
