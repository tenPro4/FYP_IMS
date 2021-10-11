using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entities
{
    public class ProjectColumn:IBaseEntity
    {
        public ProjectColumn()
        {
            MasterTask = new HashSet<MasterTask>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        [ForeignKey(nameof(MasterProject))]
        public int ProjectId { get; set; }
        public MasterProject MasterProject { get; set; }

        public ICollection<MasterTask> MasterTask { get; set; }
    }
}
