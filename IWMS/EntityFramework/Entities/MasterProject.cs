using BusinessLogicShared.Constants;
using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entities
{
    public class MasterProject : IBaseEntity
    {
        public MasterProject()
        {
            Column = new HashSet<ProjectColumn>();
            ProjectUser = new HashSet<ProjectUser>();
        }

        [Key]
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ProjectStatus Status { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [ForeignKey(nameof(Leader))]//Restrict
        public int EmployeeLeaderId { get; set; }
        public Employee Leader { get; set; }

        public ICollection<ProjectColumn> Column { get; set; }
        public ICollection<ProjectUser> ProjectUser { get; set; }
    }
}
