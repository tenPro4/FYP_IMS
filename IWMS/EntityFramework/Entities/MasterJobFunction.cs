using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class MasterJobFunction : IBaseEntity
    {
        public MasterJobFunction()
        {
            EmployeeState = new HashSet<EmployeeState>();
        }

        public int JobFunctionId { get; set; }
        public int SectionId { get; set; }
        public string FunctionName { get; set; }
        public string FunctionCode { get; set; }

        public MasterSection Section { get; set; }
        public ICollection<EmployeeState> EmployeeState { get; set; }
    }
}
