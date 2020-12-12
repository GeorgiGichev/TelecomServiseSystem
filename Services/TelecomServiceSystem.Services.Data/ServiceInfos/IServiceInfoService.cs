namespace TelecomServiceSystem.Services.Data.ServiceInfos
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Models;

    public interface IServiceInfoService
    {
        Task<ServiceInfo> CreateAsync<T>(string orderId, T model, string url);

        Task<string> GetICCAsync();

        Task<T> GetByOrderIdAsync<T>(string orderId);

        Task SetServiceAsActiveAsync(int serviceInfoId);

        Task<IEnumerable<TOutput>> GetBySearchCriteriaAsync<TOutput, TQuery>(TQuery query);

        Task<IEnumerable<T>> GetAllByCustomerIdAsync<T>(string customerId);

        Task<T> GetByIdAsync<T>(int id);

        Task RenewAsync<T>(T model, string url);

        Task ContractCancelAsync(int id, string url);

        Task<bool> ExistByOrderIdAsync(string orderId);

        Task<bool> ExistByIdAsync(int id);
    }
}
