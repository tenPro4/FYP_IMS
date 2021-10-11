using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Specifications
{
    public class EmpSpecification : BaseSpecification<Employee>
    {
        public EmpSpecification(Expression<Func<Employee, bool>> filter)
            : base(filter)
        {
            AddInclude(x => x.EmployeeAddress);
            AddInclude(x => x.EmployeeImage);
            AddInclude(x => x.Department);
            AddInclude(x => x.Project);
            AddInclude(x => x.Permission);
        }

        public EmpSpecification()
        {
            AddInclude(x => x.EmployeeAddress);
            AddInclude(x => x.EmployeeImage);
            AddInclude(x => x.Department);
            AddInclude(x => x.Project);
            AddInclude(x => x.Permission);
        }
    }
}
