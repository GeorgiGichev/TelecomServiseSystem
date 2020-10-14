namespace TelecomServiceSystem.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class ServiceNumberViewModel
    {
        public IEnumerable<string> Numbers { get; set; } = new HashSet<string>();

        public string Number { get; set; }
    }
}
