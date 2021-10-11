using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using IWMS.ExceptionHandling;
using IWMS.Configurations.Constants;
using BusinessLogicShared.Security;
using Microsoft.AspNetCore.Mvc.Razor;
using EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using EntityFramework.Context;
using Newtonsoft.Json;

namespace IWMS.Installers
{
    public class OtherInstaller : IInstaller
    {
        public void InstallAssembly(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ControllerExceptionFilterAttribute>();

            services.AddCors(options =>
            {
                options.AddPolicy(OtherConfigurationConsts.CrossOrigin,
                    builder =>
                    {
                        builder.WithOrigins(OtherConfigurationConsts.CrossOriginHost1,
                                            OtherConfigurationConsts.CrossOriginHost2)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowAnyOrigin();
                    });
            });

            var smtpSettings = new SmtpSettings();
            configuration.Bind(nameof(smtpSettings), smtpSettings);
            services.AddSingleton(smtpSettings);

            services.AddDefaultIdentity<MasterAccount>()
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
