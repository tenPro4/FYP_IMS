using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Configurations.Constants
{
    public class OtherConfigurationConsts
    {
        public const string Serilog = "Serilog";
        public const string LogPath = "Log/imslog.txt";
        public const string LogSchemaName = "dbo";

        public const string CrossOrigin = "AllowAllOrigins";
        public const string CrossOriginHost1 = "http://localhost:5000";
        public const string CrossOriginHost2 = "http://localhost:3000";

        public const string ResourcesPath = "Resources";
    }
}
