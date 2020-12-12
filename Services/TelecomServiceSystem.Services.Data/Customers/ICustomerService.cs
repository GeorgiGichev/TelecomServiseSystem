namespace TelecomServiceSystem.Services.Data.Customers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICustomerService
    {
        Task<IEnumerable<TOutput>> GetBySearchCriteriaAsync<TOutput, TQuery>(TQuery query);

        Task<string> CreateAsync<T>(T input);

        Task<T> GetByIdAsync<T>(string id);

        Task EditAsync<T>(T input);

        Task<IEnumerable<T>> GetAllForBillingAsync<T>();

        Task<bool> ExistAsync(string id);

        Task Delete(string id);
    }
}
