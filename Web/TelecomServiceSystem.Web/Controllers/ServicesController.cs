namespace TelecomServiceSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Services.Data.Services;
    using TelecomServiceSystem.Web.ViewModels.Services;

    public class ServicesController : BaseController
    {
        private readonly IServiceInfoService serviceInfoService;
        private readonly IServiceService serviceService;

        public ServicesController(IServiceInfoService serviceInfoService, IServiceService serviceService)
        {
            this.serviceInfoService = serviceInfoService;
            this.serviceService = serviceService;
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
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            await this.serviceInfoService.ContractCancel(model.Id);
            return this.Redirect($"/Services/AllByCustomer/{model.CustomerId}");
        }

        public async Task<IActionResult> Create()
        {
            var model = new ServiceCreateInputModel
            {
                Types = await this.serviceService.GetAllTypes() as ICollection<string>,
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.serviceService.Create<ServiceCreateInputModel>(model);
            return this.Redirect("/Home/Index");
        }
    }
}
