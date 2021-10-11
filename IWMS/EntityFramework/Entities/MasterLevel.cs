using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class MasterLevel : IBaseEntity
    {
        public MasterLevel()
        {
            EmployeeState = new HashSet<EmployeeState>();
        }

        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public string LevelCode { get; set; }

        public ICollection<EmployeeState> EmployeeState { get; set; }
    }
}
