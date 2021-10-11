using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Configurations.Authorization
{
    public static class Permissions
    {
        public enum PermissionType
        {
            READ,
            CREATE,
            UPDATE,
            DELETE
        }

        public enum DepartmentType
        {
            UNCATEGORY,
            DEPARTMENT,
            PERMISSION,
        }

        public enum Roles
        {
            Admin,
            ProjectLeader
        }
    }
}
