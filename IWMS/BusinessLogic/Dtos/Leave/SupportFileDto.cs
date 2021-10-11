using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Leave
{
    public class SupportFileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Size { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
