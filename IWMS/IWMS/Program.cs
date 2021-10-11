using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using EntityFramework.Context;

namespace IWMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        public static async Task MainAsync(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            //using (var serviceScope = host.Services.CreateScope())
            //{
            //    var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

            //    await dbContext.Database.MigrateAsync();
            //}
            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
