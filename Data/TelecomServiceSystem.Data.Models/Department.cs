namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using TelecomServiceSystem.Data.Common.Models;

    public class Department : BaseDeletableModel<int>
    {
        public Department()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        public string Name { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int ManagerId { get; set; }

        public virtual ApplicationUser Manager { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
