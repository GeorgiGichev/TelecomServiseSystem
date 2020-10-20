namespace TelecomServiceSystem.Services.Data.Orders
{
    using System.Threading.Tasks;

    public interface IOrderService
    {
        Task<Toutput> CreateAsync<Toutput, Tinput>(Tinput order, Toutput serviceInfo);

        Task FinishOrderAsync<T>(T model);
    }
}
