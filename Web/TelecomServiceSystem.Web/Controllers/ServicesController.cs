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
    }
}
