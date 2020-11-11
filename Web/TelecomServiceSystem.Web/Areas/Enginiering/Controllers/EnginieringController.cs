namespace TelecomServiceSystem.Web.Areas.Engeniering.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Services.Data.Orders;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Services.Data.Tasks;
    using TelecomServiceSystem.Web.Controllers;
    using TelecomServiceSystem.Web.Infrastructure.Extensions;
    using TelecomServiceSystem.Web.ViewModels.Orders;
    using TelecomServiceSystem.Web.ViewModels.Tasks;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.EnginierRoleName)]
    [Area("Enginiering")]
    public class EnginieringController : BaseController
    {
        private readonly ITasksService taskService;
        private readonly IOrderService orderService;
        private readonly IServiceInfoService serviceInfoService;

        public EnginieringController(ITasksService taskService, IOrderService orderService, IServiceInfoService serviceInfoService)
        {
            this.taskService = taskService;
            this.orderService = orderService;
            this.serviceInfoService = serviceInfoService;
        }

        public async Task<IActionResult> AllTasks()
        {
            IEnumerable<TaskViewModel> model = null;
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                model = await this.taskService.GetAllAsync<TaskViewModel>();
            }
            else
            {
                model = await this.taskService.GetByUserIdAsync<TaskViewModel>(this.User.GetId());
            }

            return this.View(model);
        }

        public async Task<IActionResult> CompleteInstalation(string orderId)
        {
            var model = await this.serviceInfoService.GetByOrderIdAsync<FixedServiceInfiViewModel>(orderId);
            await this.orderService.FinishOrderAsync<FixedServiceInfiViewModel>(model);
            return this.RedirectToAction(nameof(this.AllTasks));
        }

        public async Task<IActionResult> InstalationInfo(string orderId)
        {
            var model = await this.serviceInfoService.GetByOrderIdAsync<InstallationInfoViewModel>(orderId);
            return this.View(model);
        }
    }
}
