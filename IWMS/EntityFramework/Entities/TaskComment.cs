using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class TaskComment :IBaseEntity
    {
        [Key]
        public int CommentId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Text { get; set; }

        public int TaskId { get; set; }
        public MasterTask MasterTask { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
