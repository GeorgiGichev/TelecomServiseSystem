namespace TelecomServiceSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using TelecomServiceSystem.Data;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;
    using TelecomServiceSystem.Data.Repositories;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Services.Data.Tasks;
    using TelecomServiceSystem.Services.Data.Tests.TestsModels;
    using Xunit;

    public class TasksServiceTests : TestsBase
    {
        [Fact]
        public async Task GetFreeSlotsByAddressIdAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var orderRepo = new EfDeletableEntityRepository<Order>(dbContext);
            var taskRepo = new EfDeletableEntityRepository<EnginieringTask>(dbContext);
            var slotRepo = new EfDeletableEntityRepository<InstalationSlot>(dbContext);

            var moqAddressService = new Mock<IAddressService>();

            moqAddressService.Setup(x => x.GetCityIdByAddressIdAsync(It.IsAny<int>())).Returns(Task.FromResult(1));

            var service = new TasksService(
                slotRepo,
                moqAddressService.Object,
                taskRepo,
                orderRepo);

            var startingTime = DateTime.UtcNow.AddDays(2);
            var endingTime = startingTime.AddHours(2);
            await slotRepo.AddAsync(new InstalationSlot
            {
                StartingTime = startingTime,
                EndingTime = endingTime,
                Team = new Team
                {
                    CityId = 1,
                },
            });

            await slotRepo.AddAsync(new InstalationSlot
            {
                StartingTime = startingTime,
                EndingTime = endingTime,
                Team = new Team
                {
                    CityId = 1,
                },
            });

            await slotRepo.SaveChangesAsync();

            var slots = await service.GetFreeSlotsByAddressIdAsync<SlotModel>(1);

            Assert.Equal(2, slots.Count());
            Assert.Contains(slots, x => x.TeamId == 1);
            Assert.Contains(slots, x => x.StartingTime == startingTime);
            Assert.Contains(slots, x => x.EndingTime == endingTime);
        }

        [Fact]
        public async Task GetByUserIdAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var orderRepo = new EfDeletableEntityRepository<Order>(dbContext);
            var taskRepo = new EfDeletableEntityRepository<EnginieringTask>(dbContext);
            var slotRepo = new EfDeletableEntityRepository<InstalationSlot>(dbContext);

            var moqAddressService = new Mock<IAddressService>();

            moqAddressService.Setup(x => x.GetCityIdByAddressIdAsync(It.IsAny<int>())).Returns(Task.FromResult(1));

            var service = new TasksService(
                slotRepo,
                moqAddressService.Object,
                taskRepo,
                orderRepo);
            var userId = Guid.NewGuid().ToString();
            var user2 = Guid.NewGuid().ToString();
            var startingTime = DateTime.UtcNow.AddDays(2);
            var endingTime = startingTime.AddHours(2);
            await taskRepo.AddAsync(new EnginieringTask
            {
                Order = new Order
                {
                    Status = Status.ForExecution,
                },
                Team = new Team
                {
                    Employees = new HashSet<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Id = userId,
                        },
                    },
                },
                InstalationDate = startingTime,
            });

            await taskRepo.AddAsync(new EnginieringTask
            {
                Order = new Order
                {
                    Status = Status.Finished,
                },
                Team = new Team
                {
                    Employees = new HashSet<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Id = user2,
                        },
                    },
                },
                InstalationDate = startingTime,
            });

            await slotRepo.SaveChangesAsync();

            var slots = await service.GetByUserIdAsync<TaskModel>(userId);

            Assert.Single(slots);
            Assert.Contains(slots, x => x.TeamId == 1);
        }

        [Fact]
        public async Task GetAllAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var orderRepo = new EfDeletableEntityRepository<Order>(dbContext);
            var taskRepo = new EfDeletableEntityRepository<EnginieringTask>(dbContext);
            var slotRepo = new EfDeletableEntityRepository<InstalationSlot>(dbContext);

            var moqAddressService = new Mock<IAddressService>();

            moqAddressService.Setup(x => x.GetCityIdByAddressIdAsync(It.IsAny<int>())).Returns(Task.FromResult(1));

            var service = new TasksService(
                slotRepo,
                moqAddressService.Object,
                taskRepo,
                orderRepo);
            var userId = Guid.NewGuid().ToString();
            var user2 = Guid.NewGuid().ToString();
            var startingTime = DateTime.UtcNow.AddDays(2);
            var endingTime = startingTime.AddHours(2);
            await taskRepo.AddAsync(new EnginieringTask
            {
                Order = new Order
                {
                    Status = Status.ForExecution,
                },
                Team = new Team
                {
                    Employees = new HashSet<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Id = userId,
                        },
                    },
                },
                InstalationDate = startingTime,
            });

            await taskRepo.AddAsync(new EnginieringTask
            {
                Order = new Order
                {
                    Status = Status.ForExecution,
                },
                Team = new Team
                {
                    Employees = new HashSet<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Id = user2,
                        },
                    },
                },
                InstalationDate = startingTime,
            });

            await slotRepo.SaveChangesAsync();

            var slots = await service.GetAllAsync<TaskModel>();

            Assert.Equal(2, slots.Count());
            Assert.Contains(slots, x => x.TeamId == 1);
        }

        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var orderRepo = new EfDeletableEntityRepository<Order>(dbContext);
            var taskRepo = new EfDeletableEntityRepository<EnginieringTask>(dbContext);
            var slotRepo = new EfDeletableEntityRepository<InstalationSlot>(dbContext);

            var moqAddressService = new Mock<IAddressService>();

            moqAddressService.Setup(x => x.GetCityIdByAddressIdAsync(It.IsAny<int>())).Returns(Task.FromResult(1));

            var service = new TasksService(
                slotRepo,
                moqAddressService.Object,
                taskRepo,
                orderRepo);

            var orderId = Guid.NewGuid().ToString();
            var slotId = 1;
            var orderId2 = Guid.NewGuid().ToString();
            var slotId2 = 2;

            await slotRepo.AddAsync(new InstalationSlot
            {
                StartingTime = DateTime.UtcNow,
            });
            await slotRepo.AddAsync(new InstalationSlot
            {
                StartingTime = DateTime.UtcNow,
            });
            await slotRepo.SaveChangesAsync();

            await orderRepo.AddAsync(new Order
            {
                Id = orderId,
            });
            await orderRepo.AddAsync(new Order
            {
                Id = orderId2,
            });
            await orderRepo.SaveChangesAsync();
            await service.CreateAsync(orderId, slotId);
            await service.CreateAsync(orderId2, slotId2);

            var tasks = await taskRepo.All().ToListAsync();

            Assert.Equal(2, tasks.Count());
            Assert.Contains(tasks, x => x.OrderId == orderId);
            Assert.Contains(tasks, x => x.OrderId == orderId2);
        }
    }
}
