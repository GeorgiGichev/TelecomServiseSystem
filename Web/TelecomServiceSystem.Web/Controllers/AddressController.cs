namespace TelecomServiceSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Web.ViewModels.Customers;

    public class AddressController : BaseController
    {
        private readonly IAddressService addressService;

        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        public async Task<IActionResult> Edit(int id, string customerId)
        {
            var model = await this.addressService.GetByIdAsync<CustomersAddressInputModel>(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomersAddressInputModel model)
        {
            await this.addressService.EditAsync<CustomersAddressInputModel>(model);

            return this.View(model);
        }
    }
}
