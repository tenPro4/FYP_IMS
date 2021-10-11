using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Entities;
using IWMS.Configurations.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace IWMS.Installers
{
    public class AuthorizationInstaller : IInstaller
    {
        #region old code
        //public static async Task InitializePoliciesAsync(this IServiceCollection sc)
        //{
        //    var services = sc.BuildServiceProvider();
        //    var context = services.GetRequiredService<AppDbContext>();
        //    var permission = await context.MasterPermission.AsNoTracking().ToListAsync();

        //    if (permission.Any())
        //    {
        //        var nonInsert = Metrics.Where(x => !permission.Select(y => y.PermissionName).Contains(x)).ToList();

        //        nonInsert.ForEach(x => context.MasterPermission.
        //        AddRange(new MasterPermission
        //        { PermissionCode = x, PermissionName = x }));
        //    }
        //    else
        //    {
        //        Metrics.ForEach(x => context.MasterPermission.
        //        AddRange(new MasterPermission
        //        { PermissionCode = x, PermissionName = x }));
        //    }

        //    await context.SaveChangesAsync();

        //    if (permission.Any())
        //    {
        //        sc.AddAuthorization(options =>
        //        {
        //            Metrics.ForEach(x => options.AddPolicy(x, builder =>
        //            {
        //                builder.RequireClaim(x, "true");
        //            }));
        //        });
        //    }
        //}

        //private static readonly List<string> _metrics =
        //new List<string>
        // {
        //    "GETACTIVE",
        //    "GETABSENT",
        // };

        //public static List<string> Metrics
        //{
        //    get { return _metrics; }
        //}

        #endregion old code

        public void InstallAssembly(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                //o.ForEach(x => options.AddPolicy(x.Test, policy =>
                //{
                //    policy.RequireClaim(x.Test, "true");
                //}));

                //options.AddPolicy("Over21Only", policy => policy.Requirements.Add(new MinimumAgeRequirement(21)));
                //[Authorize(Policy = "Over21Only")]

                options.AddPolicy("OnlyCompanyEmail", policy => policy.Requirements.Add(new WorksForCompanyRequirement("gmail.com")));

                //example with multiple handle(require)
                //options.AddPolicy("WithMultipleRequire", policy => policy.Requirements.Add(new OfficeEntryRequirement()));


            });

        }
    }
}
