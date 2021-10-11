using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Specifications
{
    public class LeaveSpecification : BaseSpecification<Leave>
    {
        public LeaveSpecification(Expression<Func<Leave, bool>> filter)
            : base(filter)
        {
            AddInclude(x => x.SupportFile);
            AddInclude(x => x.Employee);
        }

        public LeaveSpecification()
        {
            AddInclude(x => x.SupportFile);
            AddInclude(x => x.Employee);
        }
    }
}
