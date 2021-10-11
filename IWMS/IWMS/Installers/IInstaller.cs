using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Installers
{
    public interface IInstaller
    {
        void InstallAssembly(IServiceCollection services, IConfiguration configuration);
    }
}
