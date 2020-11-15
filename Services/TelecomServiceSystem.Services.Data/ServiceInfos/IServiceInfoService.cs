namespace TelecomServiceSystem.Services.Data.ServiceInfos
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Models;

    public interface IServiceInfoService
    {
        Task<ServiceInfo> CreateAsync<T>(string orderId, T model);

        Task<string> GetICC();

        Task<T> GetByOrderIdAsync<T>(string orderId);

        Task SetServiceAsActiveAsync(int serviceInfoId);

        Task<IEnumerable<TOutput>> GetBySearchCriteriaAsync<TOutput, TQuery>(TQuery query);

        Task<IEnumerable<T>> GetAllByCustomerIdAsync<T>(string customerId);

        Task<T> GetById<T>(int id);

        Task Renew<T>(T model);
    }
}
