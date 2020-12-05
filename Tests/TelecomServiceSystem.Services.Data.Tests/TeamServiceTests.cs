namespace TelecomServiceSystem.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Repositories;
    using TelecomServiceSystem.Services.Data.Teams;
    using TelecomServiceSystem.Services.Data.Tests.TestsModels;
    using Xunit;

    public class TeamServiceTests : TestsBase
    {
        [Fact]
        public async Task AddEmployeeAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var teamRepo = new EfDeletableEntityRepository<Team>(dbContext);
            var employeeRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new TeamService(teamRepo, employeeRepo);

            var employeeId = Guid.NewGuid().ToString();
            var teamId = 1;

            await teamRepo.AddAsync(new Team());
            await teamRepo.SaveChangesAsync();

            await employeeRepo.AddAsync(new ApplicationUser
            {
                Id = employeeId,
            });
            await employeeRepo.SaveChangesAsync();

            await service.AddEmployeeAsync(teamId, employeeId);

            var team = await teamRepo.All()
                .FirstOrDefaultAsync();

            var employee = await employeeRepo.All()
                .FirstOrDefaultAsync();
            Assert.Contains(team.Employees, x => x.Id == employeeId);
            Assert.Equal(teamId, team.Id);
        }

        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var teamRepo = new EfDeletableEntityRepository<Team>(dbContext);
            var employeeRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new TeamService(teamRepo, employeeRepo);

            var employeeId = Guid.NewGuid().ToString();
            var teamId = 1;

            await employeeRepo.AddAsync(new ApplicationUser
            {
                Id = employeeId,
            });
            await employeeRepo.SaveChangesAsync();

            await service.CreateAsync(employeeId, 1);

            var team = await teamRepo.All()
                .FirstOrDefaultAsync();

            var employee = await employeeRepo.All()
                .FirstOrDefaultAsync();
            Assert.Contains(team.Employees, x => x.Id == employeeId);
            Assert.Equal(teamId, team.Id);
            Assert.True(team.InstalationSlots.Any());
        }

        [Fact]
        public async Task GetAllTeamsAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var teamRepo = new EfDeletableEntityRepository<Team>(dbContext);
            var employeeRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new TeamService(teamRepo, employeeRepo);

            await teamRepo.AddAsync(new Team());
            await teamRepo.AddAsync(new Team
            {
                CityId = 10,
            });
            await teamRepo.SaveChangesAsync();

            var teams = await service.GetAllTeamsAsync<TeamModel>();

            Assert.Contains(teams, x => x.CityId == 10);
            Assert.Equal(2, teams.Count());
        }

        [Fact]
        public async Task GetByCityIdAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var teamRepo = new EfDeletableEntityRepository<Team>(dbContext);
            var employeeRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new TeamService(teamRepo, employeeRepo);

            await teamRepo.AddAsync(new Team());
            await teamRepo.AddAsync(new Team
            {
                CityId = 10,
            });
            await teamRepo.SaveChangesAsync();

            var teams = await service.GetByCityIdAsync<TeamModel>(10);

            Assert.Contains(teams, x => x.CityId == 10);
            Assert.Single(teams);
        }

        [Fact]
        public async Task GetFreeTeamByCityIdAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var teamRepo = new EfDeletableEntityRepository<Team>(dbContext);
            var employeeRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new TeamService(teamRepo, employeeRepo);

            await teamRepo.AddAsync(new Team());
            await teamRepo.AddAsync(new Team
            {
                CityId = 10,
            });
            await teamRepo.SaveChangesAsync();

            var team = await service.GetFreeTeamByCityIdAsync<TeamModel>(10);

            Assert.Equal(10, team.CityId);
        }

        [Fact]
        public async Task AddSlotsToTeamsAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var teamRepo = new EfDeletableEntityRepository<Team>(dbContext);
            var employeeRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new TeamService(teamRepo, employeeRepo);

            await teamRepo.AddAsync(new Team());
            await teamRepo.AddAsync(new Team
            {
                CityId = 10,
            });
            await teamRepo.SaveChangesAsync();

            await service.AddSlotsToTeamsAsync();

            var teams = await teamRepo.All().ToListAsync();

            Assert.Equal(2, teams.Count());
            Assert.True(teams[0].InstalationSlots.Any());
            Assert.True(teams[1].InstalationSlots.Any());
            Assert.DoesNotContain(teams[0].InstalationSlots, x => x.StartingTime == DateTime.Parse("01/01/2021"));
        }
    }
}
