using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using IWMS.Configurations.Constants;
using EntityFramework.Context;

namespace IWMS.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallAssembly(IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString(DbConfigurationConsts.SQLSERVER_DEFAULT_DATABASE_CONNECTIONSTRING) ??
            //           DbConfigurationConsts.SQLITE_DEFAULT_DATABASE_CONNECTIONSTRING;

            //var databaseProvider = configuration.GetConnectionString(DbConfigurationConsts.SQLSERVER_DEFAULT_DATABASE_PROVIDER);

            //if (string.IsNullOrWhiteSpace(databaseProvider))
            //    databaseProvider = DbConfigurationConsts.SQLITE_DEFAULT_DATABASE_PROVIDER;

            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(DbConfigurationConsts.SQLSERVER_DEFAULT_DATABASE_CONNECTIONSTRING), sql => sql.MigrationsAssembly(migrationAssembly))
                //if (databaseProvider.ToLower().Trim().Equals("sqlite"))
                //    options.UseSqlite(connectionString);
                //else if (databaseProvider.ToLower().Trim().Equals("sqlserver"))
                //{
                //    // only works in windows container
                //    options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
                //}
                //else
                //    throw new Exception("Database provider unknown. Please check configuration");
                );
        }
    }
}
