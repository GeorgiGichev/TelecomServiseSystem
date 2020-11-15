namespace TelecomServiceSystem.Data.Models
{
    using System.Collections.Generic;

    using TelecomServiceSystem.Data.Common.Models;

    public class ServiceNumber : BaseDeletableModel<int>
    {
        public ServiceNumber()
        {
            this.IsFree = true;
        }

        public string Number { get; set; }

        public bool IsFree { get; set; }

        public virtual ICollection<ServiceInfo> ServiceInfos { get; set; } = new HashSet<ServiceInfo>();
    }
}
