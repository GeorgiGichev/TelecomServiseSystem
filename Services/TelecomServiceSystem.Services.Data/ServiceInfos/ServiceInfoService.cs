﻿namespace TelecomServiceSystem.Services.Data.ServiceInfos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;
    using TelecomServiceSystem.Services.Data.ServiceNumber;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models;

    public class ServiceInfoService : IServiceInfoService
    {
        private readonly IDeletableEntityRepository<ServiceInfo> serviseInfoRepo;
        private readonly IDeletableEntityRepository<SimCard> simRepo;
        private readonly IServiceNumberService serviceNumberService;

        public ServiceInfoService(IDeletableEntityRepository<ServiceInfo> serviseInfoRepo, IDeletableEntityRepository<SimCard> simRepo, IServiceNumberService serviceNumberService)
        {
            this.serviseInfoRepo = serviseInfoRepo;
            this.simRepo = simRepo;
            this.serviceNumberService = serviceNumberService;
        }

        public async Task<ServiceInfo> CreateAsync<T>(string orderId, T model, string url)
        {
            var serviceInfoToAdd = model.To<ServiceInfo>();
            serviceInfoToAdd.OrderId = orderId;
            serviceInfoToAdd.Expirеs = DateTime.UtcNow.AddMonths(serviceInfoToAdd.ContractDuration);
            serviceInfoToAdd.Documents.Add(new Document { Url = url });
            await this.serviseInfoRepo.AddAsync(serviceInfoToAdd);
            await this.serviseInfoRepo.SaveChangesAsync();

            return serviceInfoToAdd;
        }

        public async Task<T> GetByOrderIdAsync<T>(string orderId)
        {
            var result = await this.serviseInfoRepo.All()
                .Where(si => si.OrderId == orderId)
                .To<T>()
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<string> GetICCAsync()
        {
            if (!this.simRepo.All().Any())
            {
                return GlobalConstants.NoAvailableSimMessage;
            }

            var sim = await this.simRepo.All()
              .FirstOrDefaultAsync();

            this.simRepo.Delete(sim);
            await this.simRepo.SaveChangesAsync();
            return sim.ICC;
        }

        public async Task SetServiceAsActiveAsync(int serviceInfoId)
        {
            var serviceInfo = await this.serviseInfoRepo.All().FirstOrDefaultAsync(si => si.Id == serviceInfoId);
            serviceInfo.IsActive = true;
            this.serviseInfoRepo.Update(serviceInfo);
            if (serviceInfo.ICC != null)
            {
                var sim = await this.simRepo.AllWithDeleted().FirstOrDefaultAsync(s => s.ICC == serviceInfo.ICC);
                this.simRepo.HardDelete(sim);
            }

            await this.serviseInfoRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TOutput>> GetBySearchCriteriaAsync<TOutput, TQuery>(TQuery query)
        {
            IQueryable<ServiceInfo> orders = this.serviseInfoRepo.All();

            var order = query.To<InputOrderSearchModel>();

            if (order.UserId != null)
            {
                orders = orders.Where(o => o.Order.UserId == order.UserId);
            }

            if (order.Status != null)
            {
                orders = orders.Where(o => o.Order.Status == Enum.Parse<Status>(order.Status));
            }

            if (order.ServiceType != null)
            {
                orders = orders.Where(o => o.Service.ServiceType == Enum.Parse<ServiceType>(order.ServiceType));
            }

            if (order.FinishedOn.HasValue)
            {
                var date = order.FinishedOn.Value.Date;
                orders = orders.Where(o => o.Order.FinishedOn.Value.Date == date);
            }

            if (order.CreatedOn.HasValue)
            {
                var date = order.CreatedOn.Value.Date;
                orders = orders.Where(o => o.Order.CreatedOn.Date == date);
            }

            if (order.ServiceName != null)
            {
                orders = orders.Where(o => o.Service.Name.ToLower().Contains(order.ServiceName.ToLower()));
            }

            var result = await orders
                .To<TOutput>()
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<T>> GetAllByCustomerIdAsync<T>(string customerId)
        {
            return await this.serviseInfoRepo.All()
                .Where(s => s.CustomerId == customerId && s.IsActive)
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.serviseInfoRepo.All().Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task RenewAsync<T>(T model, string url)
        {
            var doc = new Document
            {
                Url = url,
            };

            var serviceInput = model.To<ServiceInfo>();
            var service = await this.serviseInfoRepo.All().FirstOrDefaultAsync(a => a.Id == serviceInput.Id);
            service.Expirеs = DateTime.UtcNow.AddMonths(serviceInput.ContractDuration);
            service.Documents.Add(doc);
            await this.serviseInfoRepo.UpdateModel(service, model);
        }

        public async Task ContractCancelAsync(int id, string url)
        {
            var service = await this.serviseInfoRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            service.IsActive = false;
            service.CancellationDate = DateTime.UtcNow;
            service.Documents.Add(new Document { Url = url });
            await this.serviceNumberService.SetNumberAsFreeAsync(service.ServiceNumberId);
            this.serviseInfoRepo.Update(service);
            await this.serviseInfoRepo.SaveChangesAsync();
        }

        public async Task<bool> ExistByOrderIdAsync(string orderId)
        {
            return await this.serviseInfoRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderId == orderId) == null ? false : true;
        }

        public async Task<bool> ExistByIdAsync(int id)
        {
            return await this.serviseInfoRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) == null ? false : true;
        }
    }
}
