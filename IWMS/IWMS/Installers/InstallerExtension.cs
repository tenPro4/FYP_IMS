using IWMS.Routes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace IWMS.Installers
{
    public static class InstallerExtension
    {
        public static void InstallServicesAssembly(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installer => installer.InstallAssembly(services, configuration));
        }
    }
}
