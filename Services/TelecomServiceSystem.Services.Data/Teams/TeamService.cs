﻿namespace TelecomServiceSystem.Services.Data.Teams
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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

        public async Task AddEmployeeAsync(int teamId, string employeeId)
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
                InstalationSlots = this.GetInstalationSlots() as ICollection<InstalationSlot>,
            };
            team.Employees.Add(user);
            await this.teamRepo.AddAsync(team);
            await this.teamRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllTeamsAsync<T>()
        {
            return await this.teamRepo.All()
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByCityIdAsync<T>(int cityId)
        {
            return await this.teamRepo.All()
                .Where(t => t.CityId == cityId)
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetFreeTeamByCityIdAsync<T>(int cityId)
        {
            return await this.teamRepo.All().Where(x => x.CityId == cityId).To<T>().FirstOrDefaultAsync();
        }

        public async Task AddSlotsToTeamsAsync()
        {
            var teamsToAdd = this.teamRepo.All();

            foreach (var team in teamsToAdd)
            {
                var slots = this.GetInstalationSlots();
                team.InstalationSlots = slots as ICollection<InstalationSlot>;
                this.teamRepo.Update(team);
            }

            await this.teamRepo.SaveChangesAsync();
        }

        private IEnumerable<InstalationSlot> GetInstalationSlots()
        {
            var year = DateTime.UtcNow.Year;
            var startDate = DateTime.UtcNow == DateTime.ParseExact($"31/12/{year}", @"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
                ? DateTime.ParseExact($"01/01/{year + 1}", @"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
                : DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow == DateTime.ParseExact($"31/12/{year}", @"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
                ? DateTime.ParseExact($"31/12/{year + 1}", @"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
                : DateTime.ParseExact($"31/12/{year}", @"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            var slots = new HashSet<InstalationSlot>();

            while (startDate <= endDate)
            {
                if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    slots.Add(new InstalationSlot
                    {
                        StartingTime = DateTime.Parse($"{startDate.ToShortDateString()} 08:00:00"),
                        EndingTime = DateTime.Parse($"{startDate.ToShortDateString()} 12:00:00"),
                    });

                    slots.Add(new InstalationSlot
                    {
                        StartingTime = DateTime.Parse($"{startDate.ToShortDateString()} 12:00:00"),
                        EndingTime = DateTime.Parse($"{startDate.ToShortDateString()} 16:00:00"),
                    });

                    slots.Add(new InstalationSlot
                    {
                        StartingTime = DateTime.Parse($"{startDate.ToShortDateString()} 16:00:00"),
                        EndingTime = DateTime.Parse($"{startDate.ToShortDateString()} 18:00:00"),
                    });
                }

                startDate = startDate.AddDays(1);
            }

            return slots;
        }
    }
}
