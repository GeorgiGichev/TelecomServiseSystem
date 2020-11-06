namespace TelecomServiceSystem.Services.Data.Tasks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITasksService
    {
        Task<IEnumerable<T>> GetFreeSlotsByCityId<T>(int cityId);
    }
}
