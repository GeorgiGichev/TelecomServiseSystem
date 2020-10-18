namespace TelecomServiceSystem.Services.Data.ServiceNumber
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IServiceNumberService
    {
        Task<IEnumerable<T>> GetFreeNumbers<T>(string serviceType, string serviceName);
    }
}
