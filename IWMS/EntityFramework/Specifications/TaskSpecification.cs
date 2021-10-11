using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Specifications
{
    public class TaskSpecification : BaseSpecification<MasterTask>
    {
        public TaskSpecification(Expression<Func<MasterTask,bool>> filter)
            :base(filter)
        {
            AddInclude(x => x.TaskComment);
            AddInclude(x => x.Assignees);
            //AddInclude(x => x.TaskStatus);
            //AddInclude(x => x.TaskPriority);
        }

        public TaskSpecification()
        {
            AddInclude(x => x.TaskComment);
            AddInclude(x => x.Assignees);
            //AddInclude(x => x.TaskStatus);
            //AddInclude(x => x.TaskPriority);
        }

    }
}
