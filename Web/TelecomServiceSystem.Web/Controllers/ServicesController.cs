namespace TelecomServiceSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Services.CloudinaryService;
    using TelecomServiceSystem.Services.Data.Addresses;
    using TelecomServiceSystem.Services.Data.Customers;
    using TelecomServiceSystem.Services.Data.ServiceInfos;
    using TelecomServiceSystem.Services.Data.ServiceNumber;
    using TelecomServiceSystem.Services.Data.Services;
    using TelecomServiceSystem.Services.HtmlToPDF;
    using TelecomServiceSystem.Services.ViewRrender;
    using TelecomServiceSystem.Web.Infrastructure.Extensions;
    using TelecomServiceSystem.Web.ViewModels.Addresses;
    using TelecomServiceSystem.Web.ViewModels.ContractTemplates;
    using TelecomServiceSystem.Web.ViewModels.Services;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.SellerRoleName)]
    public class ServicesController : BaseController
    {
        private readonly IServiceInfoService serviceInfoService;
        private readonly IServiceService serviceService;
        private readonly ICustomerService customerService;
        private readonly IAddressService addressService;
        private readonly IViewRenderService viewRenderService;
        private readonly IHtmlToPdfConverter htmlToPdfConverter;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IServiceNumberService serviceNumberService;
        private readonly IUploadService uploadService;

        public ServicesController(IServiceInfoService serviceInfoService, IServiceService serviceService, ICustomerService customerService, IAddressService addressService, IViewRenderService viewRenderService, IHtmlToPdfConverter htmlToPdfConverter, IWebHostEnvironment webHostEnvironment, IServiceNumberService serviceNumberService, IUploadService uploadService)
        {
            this.serviceInfoService = serviceInfoService;
            this.serviceService = serviceService;
            this.customerService = customerService;
            this.addressService = addressService;
            this.viewRenderService = viewRenderService;
            this.htmlToPdfConverter = htmlToPdfConverter;
            this.webHostEnvironment = webHostEnvironment;
            this.serviceNumberService = serviceNumberService;
            this.uploadService = uploadService;
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> All()
        {
            var model = await this.serviceService.GetAllAsync<ServiceAllViewModel>();
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.serviceService.Delete(id);
            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Cancellation(int serviceInfoId)
        {
            if (!await this.serviceInfoService.ExistByIdAsync(serviceInfoId))
            {
                return this.NotFound();
            }

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

            var url = await this.uploadService.UploadImageAsync(model.Image);
            await this.serviceInfoService.ContractCancelAsync(model.Id, url);
            return this.Redirect($"/Services/AllByCustomer/{model.CustomerId}");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create()
        {
            var model = new ServiceCreateInputModel
            {
                Types = await this.serviceService.GetAllTypesAsync() as ICollection<string>,
            };
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.serviceService.CreateAsync<ServiceCreateInputModel>(model);
            return this.Redirect("/Home/Index");
        }

        public async Task<IActionResult> Renew(string customerId, int duration, int serviceId, string serviceType, int serviceInfoId)
        {
            var model = await this.customerService.GetByIdAsync<ContarctViewModel>(customerId);
            model.Plan = await this.serviceService.GetByIdAsync<ServiceContractViewModel>(serviceId);
            model.ContractDuration = duration;
            model.Number = await this.serviceNumberService.GetByServiceInfoId(serviceInfoId);
            var serviceAddress = await this.addressService.GetByServiceInfoIdAsync<InstalationAddressViewModel>(serviceInfoId);
            model.ServiceAddress = serviceAddress == null ? "N/A" : serviceAddress.ToString();
            model.ServiceType = serviceType;
            model.UserId = this.User.GetId();
            model.Address = await this.addressService.GetMainAddressAsync<AddressViewModel>(customerId);
            var htmlData = await this.viewRenderService.RenderToStringAsync("~/Views/Services/Renew.cshtml", model);
            var fileContents = this.htmlToPdfConverter.Convert(this.webHostEnvironment.ContentRootPath, htmlData, "A4", "Portrait");
            return this.File(fileContents, "application/pdf");
        }

        public async Task<IActionResult> CancellationPdf(string customerId, string number, string service, int serviceId)
        {
            var model = await this.customerService.GetByIdAsync<CancellationViewModel>(customerId);
            model.Plan = service;
            model.Number = number;
            var serviceAddress = await this.addressService.GetByServiceInfoIdAsync<InstalationAddressViewModel>(serviceId);
            model.ServiceAddress = serviceAddress == null ? "N/A" : serviceAddress.ToString();
            model.UserId = this.User.GetId();
            model.Address = await this.addressService.GetMainAddressAsync<AddressViewModel>(customerId);
            var htmlData = await this.viewRenderService.RenderToStringAsync("~/Views/Services/CancellationPdf.cshtml", model);
            var fileContents = this.htmlToPdfConverter.Convert(this.webHostEnvironment.ContentRootPath, htmlData, "A4", "Portrait");
            return this.File(fileContents, "application/pdf");
        }
    }
}
