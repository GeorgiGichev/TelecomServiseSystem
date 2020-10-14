namespace TelecomServiceSystem.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class ServiceViewModel
    {
        public IEnumerable<string> ServiceNames { get; set; } = new HashSet<string>();

        public int ServiseType { get; set; }
    }
}
