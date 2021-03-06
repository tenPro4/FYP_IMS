using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Event
{
    public class EventApiDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public string Color { get; set; }
    }
}
