namespace TelecomServiceSystem.Data.Models
{

    using TelecomServiceSystem.Data.Common.Models;

    public class Document : BaseDeletableModel<int>
    {
        public string Url { get; set; }

        public int ServiceInfoId { get; set; }

        public ServiceInfo ServiceInfo { get; set; }
    }
}
