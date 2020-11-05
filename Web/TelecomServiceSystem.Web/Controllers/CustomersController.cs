namespace TelecomServiceSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Services.Data.Customers;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Web.ViewModels.Addresses;
    using TelecomServiceSystem.Web.ViewModels.Customers;
    using TelecomServiceSystem.Web.ViewModels.CustomersSearch;

    public class CustomersController : BaseController
    {
        private readonly ICustomerService customerService;
        private readonly IAddressService addressService;

        public CustomersController(ICustomerService customerService, IAddressService addressService)
        {
            this.customerService = customerService;
            this.addressService = addressService;
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.customerService.GetByIdAsync<CustomerEditViewModel>(id);
            model.Addresses = await this.addressService.GetByCustomerIdAsync<CustomerAddressViewModel>(id) as ICollection<CustomerAddressViewModel>;
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.customerService.Edit<CustomerEditViewModel>(model);
            model.Addresses = await this.addressService.GetByCustomerIdAsync<CustomerAddressViewModel>(model.Id) as ICollection<CustomerAddressViewModel>;

            return this.View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = new CreateCustomerInputModel
            {
                Address = new CustomersAddressInputModel
                {
                    Cities = await this.addressService.GetCitiesByCountryAsync<CityViewModel>(GlobalConstants.CountryOfUsing) as ICollection<CityViewModel>,
                },
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var customer = model.To<CustomerInputModel>();
            string customerId = await this.customerService.CreateAsync<CustomerInputModel>(customer);
            var address = model.Address;
            address.CustomerId = customerId;
            await this.addressService.CreateAsync(address);
            return this.RedirectToAction("Search");
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
