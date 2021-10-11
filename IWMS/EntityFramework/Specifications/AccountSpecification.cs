using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Specifications
{
    public class AccountSpecification : BaseSpecification<MasterAccount>
    {
        public AccountSpecification(Expression<Func<MasterAccount, bool>> filter)
            : base(filter)
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Employee.Department);
            AddInclude(x => x.Employee.Department.MasterDepartment);
            AddInclude(x => x.Employee.Project);
        }

        public AccountSpecification()
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Employee.Department);
            AddInclude(x => x.Employee.Department.MasterDepartment);
            AddInclude(x => x.Employee.Project);
        }
    }
}
