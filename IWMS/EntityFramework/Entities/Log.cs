using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;

namespace EntityFramework.Entities
{
    public class Log : Entity
    {
        public string Message { get; set; }

        public string MessageTemplate { get; set; }

        public string Level { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public string Exception { get; set; }

        public string LogEvent { get; set; }

        public string Properties { get; set; }

        [NotMapped]
        public XElement PropertiesXml => XElement.Parse(Properties);
    }
}
