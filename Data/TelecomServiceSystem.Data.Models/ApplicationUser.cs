// ReSharper disable VirtualMemberCallInConstructor
namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;
    using TelecomServiceSystem.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Orders = new HashSet<Order>();
        }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        public string EGN { get; set; }

        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public string PictureURL { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int? ManagerId { get; set; }

        public virtual ApplicationUser Manager { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
