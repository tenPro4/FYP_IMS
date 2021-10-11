using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Specifications
{
    public class ProjectColumnSpecification : BaseSpecification<ProjectColumn>
    {
        public ProjectColumnSpecification(Expression<Func<ProjectColumn, bool>> filter)
            :base(filter)
        {
            AddInclude(x => x.MasterTask);
        }

        public ProjectColumnSpecification()
        {
            AddInclude(x => x.MasterTask);
        }
    }
}
