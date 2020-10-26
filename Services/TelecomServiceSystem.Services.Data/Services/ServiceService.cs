﻿namespace TelecomServiceSystem.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Data.Models.Enums;
    using TelecomServiceSystem.Services.Mapping;

    public class ServiceService : IServiceService
    {
        private readonly IDeletableEntityRepository<Service> serviceRepo;

        public ServiceService(IDeletableEntityRepository<Service> serviceRepo)
        {
            this.serviceRepo = serviceRepo;
        }

        public async Task<IEnumerable<T>> GetServiceNamesByType<T>(string type)
        {
            return await this.serviceRepo.All()
               .Where(s => s.ServiceType == Enum.Parse<ServiceType>(type, true))
               .To<T>()
               .ToListAsync();
        }
    }
}