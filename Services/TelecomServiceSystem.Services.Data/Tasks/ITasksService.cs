namespace TelecomServiceSystem.Services.Data.Tasks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITasksService
    {
        Task<IEnumerable<T>> GetFreeSlotsByAddressId<T>(int addressId);

        Task CreateAsync(string orderId, int slotId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<IEnumerable<T>> GetByUserIdAsync<T>(string userId);
    }
}
