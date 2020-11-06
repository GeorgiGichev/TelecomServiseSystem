namespace TelecomServiceSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class TasksController : Controller
    {
        public async Task<IActionResult> GetFreeSlotsAsJson(int addressId)
        {
            var slots = "";
            return this.Json(slots);
        }
    }
}
