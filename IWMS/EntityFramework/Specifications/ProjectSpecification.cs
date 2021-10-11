using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Specifications
{
    public class ProjectSpecification : BaseSpecification<MasterProject>
    {
        public ProjectSpecification(Expression<Func<MasterProject,bool>> filter)
            :base(filter)
        {
            AddInclude(x => x.ProjectUser);
            AddInclude(x => x.ProjectUser);
            AddInclude(x => x.Column);
        }

        public ProjectSpecification()
        {
            AddInclude(x => x.ProjectUser);
            AddInclude(x => x.Column);
        }
    }
}
