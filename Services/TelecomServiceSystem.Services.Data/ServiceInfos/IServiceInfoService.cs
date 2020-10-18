namespace TelecomServiceSystem.Services.Data.ServiceInfos
{
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Models;

    public interface IServiceInfoService
    {
        Task<ServiceInfo> CreateAsync<T>(string orderId, T model);

        Task<string> GetICC();
    }
}
