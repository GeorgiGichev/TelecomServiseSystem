namespace TelecomServiceSystem.Services.Data.Orders
{
    using System.Threading.Tasks;

    using TelecomServiceSystem.Services.Models;

    public interface IOrderService
    {
        Task<Toutput> CreateAsync<Toutput, Tinput>(Tinput order, Toutput serviceInfo);

        Task FinishOrderAsync<T>(T model);
    }
}
