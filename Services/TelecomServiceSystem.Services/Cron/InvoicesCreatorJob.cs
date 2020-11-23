namespace TelecomServiceSystem.Services.Cron
{
    using System.Threading.Tasks;

    using TelecomServiceSystem.Services.Billing;

    public class InvoicesCreatorJob
    {
        public const string CronSchedule = "1 0 1 * *";
        private readonly IBillingService billingService;

        public InvoicesCreatorJob(IBillingService billingService)
        {
            this.billingService = billingService;
        }

        public async Task GenerateBills()
        {
            await this.billingService.CreateAsync();
        }
    }
}
