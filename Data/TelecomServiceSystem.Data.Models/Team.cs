namespace TelecomServiceSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TelecomServiceSystem.Data.Common.Models;

    public class Team : BaseDeletableModel<int>
    {
        private const int TeamCap = 2;

        public Team()
        {
            this.Employees = new HashSet<ApplicationUser>();
            this.Tasks = new HashSet<EnginieringTask>();
            this.TeamCapacity = TeamCap;
        }

        public int TeamCapacity { get; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<ApplicationUser> Employees { get; set; }

        public virtual ICollection<EnginieringTask> Tasks { get; set; }
    }
}
