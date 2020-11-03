namespace TelecomServiceSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Web.ViewModels.Addresses;
    using TelecomServiceSystem.Web.ViewModels.Customers;

    public class AddressesController : BaseController
    {
        private readonly IAddressService addressService;

        public AddressesController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        public IActionResult Create()
        {
            var address = new CustomersAddressInputModel();
            return this.PartialView("_CreatePartial", address);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomersAddressInputModel address)
        {
            await this.addressService.CreateAsync(address);
            return this.PartialView("_CreatePartial", address);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.addressService.GetByIdAsync<CustomersAddressInputModel>(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomersAddressInputModel model)
        {
            await this.addressService.EditAsync<CustomersAddressInputModel>(model);

            return this.Redirect($"/Customers/Edit/{model.CustomerId}");
        }
    }
}
