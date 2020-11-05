namespace TelecomServiceSystem.Services.Data.Teams
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Data.Employees;
    using TelecomServiceSystem.Services.Mapping;

    public class TeamService : ITeamsService
    {
        private readonly IDeletableEntityRepository<Team> teamRepo;
        private readonly IDeletableEntityRepository<ApplicationUser> empRepo;

        public TeamService(IDeletableEntityRepository<Team> teamRepo, IDeletableEntityRepository<ApplicationUser> empRepo)
        {
            this.teamRepo = teamRepo;
            this.empRepo = empRepo;
        }

        public async Task AddEmployee(int teamId, string employeeId)
        {
            var team = await this.teamRepo.All().FirstOrDefaultAsync(t => t.Id == teamId);
            var employee = await this.empRepo.All().FirstOrDefaultAsync(e => e.Id == employeeId);
            team.Employees.Add(employee);
            this.teamRepo.Update(team);
            await this.teamRepo.SaveChangesAsync();
        }

        public async Task CreateAsync(string employeeId, int cityId)
        {
            var user = await this.empRepo.All().FirstOrDefaultAsync(e => e.Id == employeeId);
            var team = new Team
            {
                CityId = cityId,
            };
            team.Employees.Add(user);
            await this.teamRepo.AddAsync(team);
            await this.teamRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetByCityId<T>(int cityId)
        {
            return await this.teamRepo.All()
                .Where(t => t.CityId == cityId)
                .To<T>()
                .ToListAsync();
        }


    }
}
