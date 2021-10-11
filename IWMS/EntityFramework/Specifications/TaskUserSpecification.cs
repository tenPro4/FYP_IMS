using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Specifications
{
    public class TaskUserSpecification : BaseSpecification<TaskUser>
    {
        public TaskUserSpecification(Expression<Func<TaskUser, bool>> filter)
            :base(filter)
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Employee.EmployeeImage);
        }

        public TaskUserSpecification()
        {
            AddInclude(x => x.Employee);
        }
    }
}
