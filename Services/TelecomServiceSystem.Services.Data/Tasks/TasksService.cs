namespace TelecomServiceSystem.Services.Data.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Services.Mapping;

    public class TasksService : ITasksService
    {
        private readonly IDeletableEntityRepository<InstalationSlot> slotRepo;
        private readonly IAddressService addressService;
        private readonly IDeletableEntityRepository<EnginieringTask> taskRepo;
        private readonly IDeletableEntityRepository<Order> orderRepo;

        public TasksService(IDeletableEntityRepository<InstalationSlot> slotRepo, IAddressService addressService, IDeletableEntityRepository<EnginieringTask> taskRepo, IDeletableEntityRepository<Order> orderRepo)
        {
            this.slotRepo = slotRepo;
            this.addressService = addressService;
            this.taskRepo = taskRepo;
            this.orderRepo = orderRepo;
        }

        public async Task CreateAsync(string orderId, int slotId)
        {
            var slot = await this.slotRepo.All().FirstOrDefaultAsync(x => x.Id == slotId);
            var task = new EnginieringTask
            {
                OrderId = orderId,
                InstalationDate = slot.StartingTime,
                TeamId = slot.TeamId,
            };
            var order = await this.orderRepo.All().FirstOrDefaultAsync(x => x.Id == orderId);
            await this.taskRepo.AddAsync(task);
            await this.taskRepo.SaveChangesAsync();
            task = await this.taskRepo.All().FirstOrDefaultAsync(x => x.OrderId == orderId);
            order.EnginieringTaskId = task.Id;
            this.orderRepo.Update(order);
            await this.orderRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.taskRepo.All()
                .Where(x => x.Order.Status == Status.ForExecution)
                .OrderBy(x => x.InstalationDate)
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByUserIdAsync<T>(string userId)
        {
            return await this.taskRepo.All()
                .Where(x => x.Team.Employees.Any(u => u.Id == userId) && x.Order.Status == Status.ForExecution)
                .OrderBy(x => x.InstalationDate)
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetFreeSlotsByAddressId<T>(int addressId)
        {
            var cityId = await this.addressService.GetCityIdByAddressId(addressId);
            var result = await this.slotRepo.All()
                .OrderBy(x => x.StartingTime)
                .Where(x => x.StartingTime.Date > DateTime.UtcNow.Date && x.StartingTime.Date <= DateTime.UtcNow.AddDays(11).Date && x.Team.CityId == cityId && !x.Team.Tasks.Any(t => t.InstalationDate == x.StartingTime))
                .To<T>()
                .ToListAsync();

            return result;
        }
    }
}
