using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Specifications
{
    public class EmpPermissionSpecification:BaseSpecification<EmployeePermission>
    {
        public EmpPermissionSpecification(Expression<Func<EmployeePermission, bool>> filter):base(filter)
        {
            AddInclude(x => x.MasterPermission);
        }

        public EmpPermissionSpecification()
        {
            AddInclude(x => x.MasterPermission);
        }
    }
}
