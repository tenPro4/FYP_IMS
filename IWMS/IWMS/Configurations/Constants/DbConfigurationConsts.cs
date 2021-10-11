using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Configurations.Constants
{
    public class DbConfigurationConsts
    {
        public const string SQLITE_DEFAULT_DATABASE_CONNECTIONSTRING = "Data Source=realworld.db";
        public const string SQLITE_DEFAULT_DATABASE_PROVIDER = "sqlite";

        public const string SQLSERVER_DEFAULT_DATABASE_CONNECTIONSTRING = "DefaultConnection";
        public const string SQLSERVER_DEFAULT_DATABASE_PROVIDER = "DefaultProvider";
    }
}
