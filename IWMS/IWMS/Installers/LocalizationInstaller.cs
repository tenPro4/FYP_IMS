using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IWMS.Configurations.Constants;
using IWMS.Helpers.Localization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using IWMS.Configurations.ApplicationParts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;

namespace IWMS.Installers
{
    public class LocalizationInstaller : IInstaller
    {
        public void InstallAssembly(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLocalization(options => { options.ResourcesPath = OtherConfigurationConsts.ResourcesPath; });

            services.TryAddTransient(typeof(IGenericControllerLocalizer<>), typeof(GenericControllerLocalizer<>));

            services.AddMvc(o =>
            {
                //var policy = new AuthorizationPolicyBuilder()
                //                .RequireAuthenticatedUser()
                //                .Build();
                //o.Filters.Add(new AuthorizeFilter(policy));
                o.Conventions.Add(new GenericControllerRouteConvention());
            })
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
               .AddViewLocalization(
                   LanguageViewLocationExpanderFormat.Suffix,
                   opts => { opts.ResourcesPath = OtherConfigurationConsts.ResourcesPath; })
               .AddDataAnnotationsLocalization()
               .AddJsonOptions(options => {
                   options.SerializerSettings.Formatting = Formatting.Indented;
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               });

            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new[]
                    {
                        new CultureInfo("en"),
                        new CultureInfo("fa"),
                        new CultureInfo("ru"),
                        new CultureInfo("sv"),
                        new CultureInfo("zh")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("sv");
                    opts.SupportedCultures = supportedCultures;
                    opts.SupportedUICultures = supportedCultures;
                    //opts.RequestCultureProviders = new List<IRequestCultureProvider>
                    //{
                    //    new QueryStringRequestCultureProvider(),
                    //    new CookieRequestCultureProvider()
                    //};
                });
        }
    }
}
