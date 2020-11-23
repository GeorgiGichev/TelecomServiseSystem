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
                var prevMonth = DateTime.UtcNow.Month != 1 ? DateTime.UtcNow.Month - 1 : 12;
                customer.ServicesInfo = customer.ServicesInfo.Where(x => x.IsActive || (x.CancellationDate.HasValue && x.CancellationDate.Value.Month == prevMonth)).ToHashSet();

                foreach (var service in customer.ServicesInfo)
                {
                    var fullPrice = service.ServicePrice;
                    var daysInMonth = DateTime.DaysInMonth(DateTime.UtcNow.Year, prevMonth);
                    if (service.IsActive && service.CreatedOn.Month == prevMonth)
                    {
                        service.ServicePrice = (fullPrice / daysInMonth) * (daysInMonth - service.CreatedOn.Day);
                    }
                    else if (service.CancellationDate.HasValue)
                    {
                        service.ServicePrice = (fullPrice / daysInMonth) * service.CancellationDate.Value.Day;
                    }
                }

                var htmlData = await this.viewRenderService.RenderToStringAsync("~/Views/Bills/Bill.cshtml", customer);
                var fileContents = this.htmlToPdfConverter.ConvertToImage(Environment.CurrentDirectory, htmlData, "A4", "Portrait");

                var url = await this.uploadService.UploadBillAsync(fileContents);

                await this.billsService.Create(customer.Id, url);
                //TODO: not working
                await this.emailSender.SendEmailAsync("georgi.gichev87@gmail.com", "CustomerService", customer.Email, "New Invoice", $"<p>Your monthly invoice is available. You can see it at {url} </p>");
            }
        }
    }
}
