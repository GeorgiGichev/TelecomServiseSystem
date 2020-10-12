namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Net.Sockets;
    using System.Text;

    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore.Query.Internal;
    using TelecomServiceSystem.Data.Common.Models;
    using TelecomServiceSystem.Data.Models.Enums;

    public class Customer : BaseDeletableModel<string>
    {
        public Customer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ServicesInfo = new HashSet<ServiceInfo>();
            this.Addresses = new HashSet<Address>();
        }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        public DocumentType DocumentType { get; set; }

        [Required]
        public string PersonalNumber { get; set; }

        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<ServiceInfo> ServicesInfo { get; set; }
    }
}
