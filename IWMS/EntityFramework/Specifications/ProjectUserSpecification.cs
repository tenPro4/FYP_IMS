using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Specifications
{
    public class ProjectUserSpecification:BaseSpecification<ProjectUser>
    {
        public ProjectUserSpecification(Expression<Func<ProjectUser, bool>> filter)
            :base(filter)
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Employee.EmployeeImage);
        }

        public ProjectUserSpecification()
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Employee.EmployeeImage);
        }
    }
}
