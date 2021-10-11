using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IWMS.Configurations.Authorization.Permissions;

namespace IWMS.Configurations.Authorization
{
    public class DepartmentPermission : AuthorizeAttribute, IAuthorizationFilter
    {
        public DepartmentType Department { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (string.IsNullOrEmpty(Department.ToString()) || 
                !context.HttpContext.User.HasClaim(x => x.Type == "department"))
            {
                //Validation cannot take place without any permissions so returning unauthorized
                context.Result = new UnauthorizedResult();
                return;
            }

            var dep = context.HttpContext.User.Claims.Where(x => x.Type == "department").Select(x => x.Value).Single();

            if (dep != null &&
                 dep.Equals(Department.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
