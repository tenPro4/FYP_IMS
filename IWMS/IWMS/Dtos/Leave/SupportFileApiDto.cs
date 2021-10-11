using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Leave
{
    public class SupportFileApiDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public int Size { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
