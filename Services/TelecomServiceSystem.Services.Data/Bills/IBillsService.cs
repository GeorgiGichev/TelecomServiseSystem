namespace TelecomServiceSystem.Services.Data.Bills
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TelecomServiceSystem.Data.Models;

    public interface IBillsService
    {
        Task CreateAsync(string customerId, string url);

        Task<IEnumerable<T>> GetAllByCustomerIdAsync<T>(string customerId);
    }
}
