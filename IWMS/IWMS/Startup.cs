using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IWMS.Installers;
using IWMS.Configurations.Constants;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AspNetCoreRateLimit;
using BusinessLogic.Extensions;
using EntityFramework.Context;
using IWMS.Configurations.Middlewares;
using IWMS.Data;
using System;
using IWMS.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using EntityFramework.Entities;

namespace IWMS
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();

            HostingEnvironment = env;
        }

        public IHostingEnvironment HostingEnvironment { get; }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesAssembly(Configuration);
            services.AddServices<AppDbContext>();
            //services.InitializePoliciesAsync().Wait();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env, IServiceProvider services)
        {
            app.AddLogging(loggerFactory, Configuration);
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", ApiConfigurationConsts.ApiName);
            });
            
            app.UseCors(OtherConfigurationConsts.CrossOrigin);
            app.AddProductionExceptionHandling(loggerFactory);

            app.UseMiddleware<CustomIpRateLimitMiddleware>();

            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizeOptions.Value);

            app.UseMvc();
            //SeedData.InitializeData(services);

            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                ///ui/playground
                return Task.CompletedTask;
            });
        }
    }
}
