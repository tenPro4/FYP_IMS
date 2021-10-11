using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Configurations.Authorization
{
    public class HasSectionHandler : AuthorizationHandler<OfficeEntryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OfficeEntryRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "section" &&
                                            c.Issuer == "https://contoso.com"))
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
