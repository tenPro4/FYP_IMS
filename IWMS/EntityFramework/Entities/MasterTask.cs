using BusinessLogicShared.Constants;
using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class MasterTask : IBaseEntity
    {

        public MasterTask()
        {
            TaskComment = new List<TaskComment>();
            Assignees = new List<TaskUser>();
        }

        [Key]
        public int TaskId { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }

        public TaskType TaskType { get; set; }

        public BusinessLogicShared.Constants.TaskPriority TaskPriority { get; set; }

        public int ListPosition { get; set; }

        [ForeignKey(nameof(CreatedByUserId))]
        public Employee CreatedByUser { get; set; }
        public int? CreatedByUserId { get; set; }

        [ForeignKey(nameof(ProjectColumn))]
        public int ColumnId { get; set; }
        public ProjectColumn Column { get; set; }

        public ICollection<TaskUser> Assignees { get; set; }
        public ICollection<TaskComment> TaskComment { get; set; }
    }
}
