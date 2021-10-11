using BusinessLogic.Services.Interfaces;
using EntityFramework.Context;
using EntityFramework.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static IWMS.Configurations.Authorization.Permissions;

namespace IWMS.Configurations.Authorization
{
    public class CRUDPermission : AuthorizeAttribute, IAuthorizationFilter
    {
        public PermissionType PermissionType { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var pt = PermissionType.ToString();
            if (string.IsNullOrEmpty(pt) ||
                !context.HttpContext.User.HasClaim(x => x.Type == "permission"))
            {
                //Validation cannot take place without any permissions so returning unauthorized
                context.Result = new UnauthorizedResult();
                return;
            }

            var dep = context.HttpContext.User.Claims.Where(x => x.Type == "department").Select(x => x.Value).Single();
            //var empId = context.HttpContext.User?.Claims.Where(x => x.Type == "empId").Select(x => x.Value).Single();
            var permissionList = context.HttpContext.User?.Claims.Where(x => x.Type == "permission").Select(x => x.Value).ToList();
            var permissionName = dep + "." + pt.ToString();
            //var userEmail = context.HttpContext.User?.FindFirstValue(ClaimTypes.Email);
            //var serviceProvider = context.HttpContext.RequestServices.GetService<ServiceProvider>();
            //var db = serviceProvider.GetRequiredService<AppDbContext>();
            //var account = db.MasterAccount.Where(x => x.Email == userEmail).SingleOrDefault();
            //var userPermissions = db.EmployeePermission.Where(x => x.EmployeeId ==Int16.Parse(empId))
            //    .Select(r => r.MasterPermission.PermissionName).ToList();

            if (permissionList.Contains(permissionName))
                return; //User Authorized. Wihtout setting any result value and just returning is sufficent for authorizing user
            
            context.Result = new UnauthorizedResult();
            return;

        }
    }
} 
