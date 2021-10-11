using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class EmployeeImage : IBaseEntity
    {
        [Key]
        public int ImageId { get; set; }

        public string ImageName { get; set; }

        public string ImageHeader { get; set; }

        public byte[] ImageBinary { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
