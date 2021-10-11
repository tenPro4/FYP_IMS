using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Specifications
{
    public class DepartmentSpecification : BaseSpecification<MasterDepartment>
    {
        public DepartmentSpecification(Expression<Func<MasterDepartment,bool>> filter)
            :base(filter)
        {
            AddInclude(x => x.Employees);
        }

        public DepartmentSpecification()
        {
            AddInclude(x => x.Employees);
        }
    }
}
