namespace TelecomServiceSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Data.Customers;
    using TelecomServiceSystem.Web.ViewModels.CustomersSearch;

    public class CustomersController : BaseController
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public async Task<IActionResult> Search()
        {
            var model = new SearchCustomersViewModel
            {
                CustomersList = await this.GetCustomersAsync(new SearchCustomerInputModel()),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchCustomersViewModel model)
        {
            model.CustomersList = await this.GetCustomersAsync(model.Input);
            return this.View(model);
        }

        private async Task<HashSet<SearchCustomersOutpuModel>> GetCustomersAsync(SearchCustomerInputModel model)
            => (await this.customerService.GetBySearchCriteriaAsync<SearchCustomersOutpuModel, SearchCustomerInputModel>(model)).ToHashSet();
    }
}
