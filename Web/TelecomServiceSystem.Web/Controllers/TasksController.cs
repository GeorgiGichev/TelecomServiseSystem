namespace TelecomServiceSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Data.Tasks;
    using TelecomServiceSystem.Web.ViewModels.Slots;

    public class TasksController : Controller
    {
        private readonly ITasksService tasksService;

        public TasksController(ITasksService tasksService)
        {
            this.tasksService = tasksService;
        }

        public async Task<IActionResult> GetFreeSlotsAsJson(int addressId)
        {
            var slots = await this.tasksService.GetFreeSlotsByAddressId<SlotViewModel>(addressId);
            return this.Json(slots);
        }
    }
}
