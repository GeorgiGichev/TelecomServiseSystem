﻿namespace TelecomServiceSystem.Services.Data.ServiceInfos
{
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Models;

    public interface IServiceInfoService
    {
        Task<ServiceInfo> CreateAsync<T>(string orderId, T model);

        Task<string> GetICC();

        Task<T> GetByOrderId<T>(string orderId);

        Task SetServiceAsActiveAsync(int serviceInfoId);
    }
}