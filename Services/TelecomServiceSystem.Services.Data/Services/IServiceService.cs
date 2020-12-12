namespace TelecomServiceSystem.Services.Data.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IServiceService
    {
        Task<IEnumerable<T>> GetServiceNamesByTypeAsync<T>(string type);

        Task CreateAsync<T>(T model);

        Task<IEnumerable<string>> GetAllTypesAsync();

        Task<T> GetByNameAsync<T>(string name);

        Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task Delete(int id);
    }
}
