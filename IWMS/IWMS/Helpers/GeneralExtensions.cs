using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IWMS.Helpers
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;

            return httpContext.User.Claims.Single(x => x.Type == "Id").Value;
            
        }

        public static string GetEmpId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;

            return httpContext.User.Claims.Single(x => x.Type == "empId").Value;
        }

        //public static List<Claim> GetUserClaims(this HttpContext httpContext)
        //{
        //    if (httpContext.User == null)
        //        return null;

        //    return httpContext.User.Claims.Where(x => x.Type == "group").ToList();
        //}
    }
}
