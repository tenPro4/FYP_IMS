using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Specifications
{
    public class AttendanceSpecification: BaseSpecification<Attendance>
    {
        public AttendanceSpecification(Expression<Func<Attendance,bool>> filter)
            :base(filter)
        {

        }

        public AttendanceSpecification()
        {

        }
    }
}
