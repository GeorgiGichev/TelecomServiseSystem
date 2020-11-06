namespace TelecomServiceSystem.Services.Data.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class TasksService : ITasksService
    {
        public Task<IEnumerable<T>> GetFreeSlotsByCityId<T>(int cityId)
        {
            throw new NotImplementedException();
        }
    }
}
