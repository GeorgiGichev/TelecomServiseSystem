namespace TelecomServiceSystem.Services.Data.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IServiceService
    {
        Task<IEnumerable<T>> GetServiceNamesByType<T>(string type);
    }
}
