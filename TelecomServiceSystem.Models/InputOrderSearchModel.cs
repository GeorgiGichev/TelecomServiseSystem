using System;
using System.Collections.Generic;
using System.Text;
using TelecomServiceSystem.Data.Models;
using TelecomServiceSystem.Services.Mapping;

namespace TelecomServiceSystem.Services.Models
{
    public class InputOrderSearchModel : IMapFrom<ServiceInfo>, IMapTo<ServiceInfo>
    {
        public string Status { get; set; }

        public DateTime? FinishedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string ServiceName { get; set; }

        public string ServiceType { get; set; }

        public string UserId { get; set; }
    }
}
