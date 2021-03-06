using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class MasterJobPosition : IBaseEntity
    {
        public MasterJobPosition()
        {
            EmployeeState = new HashSet<EmployeeState>();
        }

        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string PositionCode { get; set; }

        public ICollection<EmployeeState> EmployeeState { get; set; }
    }
}
