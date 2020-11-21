namespace TelecomServiceSystem.Services.Billing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TelecomServiceSystem.Data.Common.Repositories;
    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.CloudinaryService;
    using TelecomServiceSystem.Services.Data.Bills;
    using TelecomServiceSystem.Services.Data.Customers;
    using TelecomServiceSystem.Services.HtmlToPDF;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Messaging;
    using TelecomServiceSystem.Services.Models;
    using TelecomServiceSystem.Services.Models.Bills;
    using TelecomServiceSystem.Services.ViewRrender;

    public class BillingService : IBillingService
    {
        private readonly IViewRenderService viewRenderService;
        private readonly IHtmlToPdfConverter htmlToPdfConverter;
        private readonly IUploadService uploadService;
        private readonly ICustomerService customerService;
        private readonly IBillsService billsService;
        private readonly IEmailSender emailSender;

        public BillingService(IViewRenderService viewRenderService, IHtmlToPdfConverter htmlToPdfConverter, IUploadService uploadService, ICustomerService customerService, IBillsService billsService, IEmailSender emailSender)
        {
            this.viewRenderService = viewRenderService;
            this.htmlToPdfConverter = htmlToPdfConverter;
            this.uploadService = uploadService;
            this.customerService = customerService;
            this.billsService = billsService;
            this.emailSender = emailSender;
        }

        public async Task CreateAsync()
        {
            var customers = await this.customerService.GetAllForBilling<BillViewModel>();

            foreach (var customer in customers)
            {
                customer.ServicesInfo = customer.ServicesInfo.Where(x => x.IsActive).ToHashSet();
                var htmlData = await this.viewRenderService.RenderToStringAsync("~/Views/Bills/Bill.cshtml", customer);
                var fileContents = this.htmlToPdfConverter.ConvertToImage(Environment.CurrentDirectory, htmlData, "A4", "Portrait");

                var url = await this.uploadService.UploadBillAsync(fileContents);

                await this.billsService.Create(customer.Id, url);

                await this.emailSender.SendEmailAsync("CustomerService@TSS.com", "CustomerService", customer.Email, "New Invoice", $"<p>Your monthly invoice is available. You can see it at {url} </p>");
            }
        }
    }
}
