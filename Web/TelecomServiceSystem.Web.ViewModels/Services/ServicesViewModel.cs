namespace TelecomServiceSystem.Web.ViewModels.Services
{
    using System;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class ServicesViewModel : IMapFrom<ServiceInfo>
    {
        public int Id { get; set; }

        public string ServiceNumberNumber { get; set; }

        public string ServiceName { get; set; }

        public decimal ServicePrice { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime Expirеs { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int ContractDuration { get; set; }

        public string CustomerId { get; set; }

        public string Color => this.GetColor();

        public string Disabled => this.IsDisabled();

        private string GetColor()
        {
            string color = "black";
            if (DateTime.UtcNow.Year == this.Expirеs.Year
                && DateTime.UtcNow.Month - this.Expirеs.Month <= 3)
            {
                if (DateTime.UtcNow.Month - this.Expirеs.Month == 3)
                {
                    if (DateTime.UtcNow.Date >= this.Expirеs.Date)
                    {
                        color = "red";
                    }
                }
                else
                {
                    color = "red";
                }
            }

            return color;
        }

        private string IsDisabled()
        {
            string disabled = "disabled";
            if (DateTime.UtcNow.Year == this.Expirеs.Year
                && DateTime.UtcNow.Month - this.Expirеs.Month <= 3)
            {
                if (DateTime.UtcNow.Month - this.Expirеs.Month == 3)
                {
                    if (DateTime.UtcNow.Date >= this.Expirеs.Date)
                    {
                        disabled = string.Empty;
                    }
                }
                else
                {
                    disabled = string.Empty;
                }
            }

            return disabled;
        }
    }
}
