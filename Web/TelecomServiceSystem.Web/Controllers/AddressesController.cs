namespace TelecomServiceSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Web.ViewModels.Addresses;
    using TelecomServiceSystem.Web.ViewModels.Customers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.SellerRoleName)]
    public class AddressesController : BaseController
    {
        private readonly IAddressService addressService;

        public AddressesController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        public async Task<IActionResult> Create()
        {
            var address = new CustomersAddressInputModel
            {
                Cities = await this.addressService.GetCitiesByCountryAsync<CityViewModel>(GlobalConstants.CountryOfUsing)
                as ICollection<CityViewModel>,
            };
            return this.PartialView("_CreatePartial", address);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomersAddressInputModel address)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView("_CreatePartial", address);
            }

            await this.addressService.CreateAsync(address);
            return this.PartialView("_CreatePartial", address);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.addressService.GetByIdAsync<CustomerAddressViewModel>(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerAddressViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.addressService.EditAsync<CustomerAddressViewModel>(model);

            return this.Redirect($"/Customers/Edit/{model.CustomerId}");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult CreateCity()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> CreateCity(CityInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.addressService.AddNewCityAsync<CityInputModel>(model);
            return this.Redirect($"/Home/Index");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> AllCities()
        {
            var model = await this.addressService.GetAllCitiesAsync<CityViewModel>();
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.addressService.DeleteCityAsync(id);
            return this.RedirectToAction("AllCities");
        }
    }
}
