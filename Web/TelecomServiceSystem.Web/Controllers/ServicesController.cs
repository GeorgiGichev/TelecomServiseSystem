namespace TelecomServiceSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Web.ViewModels.Services;

    public class ServicesController : BaseController
    {
        private readonly IServiceInfoService serviceInfoService;

        public ServicesController(IServiceInfoService serviceInfoService)
        {
            this.serviceInfoService = serviceInfoService;
        }

        public async Task<IActionResult> AllByCustomer(string id)
        {
            var model = new ServicesCustomerViewModel
            {
                CustomerId = id,
                Services = await this.serviceInfoService.GetAllByCustomerIdAsync<ServicesViewModel>(id) as ICollection<ServicesViewModel>,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Cancellation(int serviceInfoId)
        {
            var model = await this.serviceInfoService.GetByIdAsync<ServiceCancelationViewModel>(serviceInfoId);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cancellation(ServiceCancelationViewModel model)
        {
            await this.serviceInfoService.ContractCancel(model.Id);
            return this.Redirect($"/Services/AllByCustomer/{model.CustomerId}");
        }
    }
}
