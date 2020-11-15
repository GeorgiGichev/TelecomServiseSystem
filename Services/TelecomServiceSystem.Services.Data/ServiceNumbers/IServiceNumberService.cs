namespace TelecomServiceSystem.Services.Data.ServiceNumber
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IServiceNumberService
    {
        Task<IEnumerable<T>> GetFreeNumbersAsync<T>(string serviceType, string serviceName);

        Task SetNumberAsHiredAsync(int numberId);

        Task SetNumberAsFreeAsync(int numberId);
    }
}
