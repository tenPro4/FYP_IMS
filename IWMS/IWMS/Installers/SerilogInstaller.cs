using BusinessLogicShared.Security;
using IWMS.Configurations.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;

namespace IWMS.Installers
{
    public static class SerilogInstaller
    {
        public static void AddLogging(this IApplicationBuilder app, ILoggerFactory loggerFactory, IConfigurationRoot configuration)
        {

            var columnOptions = new ColumnOptions();

            columnOptions.Store.Add(StandardColumn.LogEvent);
            //columnOptions.Store.Remove(StandardColumn.Properties);

            var template = "{Timestamp:HH:mm:ss} [{Level}] {SourceContext} {Message} {Properties:j} {NewLine}{Exception}";

            var serilogSettings = new SeriglogSettings();
            configuration.Bind(OtherConfigurationConsts.Serilog, serilogSettings);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("System",LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("API Version", ApiConfigurationConsts.ApiVersionV1)
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .WriteTo.Console(outputTemplate: template, theme: AnsiConsoleTheme.Code,restrictedToMinimumLevel:LogEventLevel.Information)
                .WriteTo.File(path: OtherConfigurationConsts.LogPath, rollingInterval: RollingInterval.Day, outputTemplate: template, restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.MSSqlServer(
                connectionString: configuration.GetConnectionString(DbConfigurationConsts.SQLSERVER_DEFAULT_DATABASE_CONNECTIONSTRING),
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = serilogSettings.TableName,
                    AutoCreateSqlTable = true,
                    BatchPostingLimit = serilogSettings.BatchPostingLimit,
                    BatchPeriod = TimeSpan.Parse("0.00:00:30"),
                    SchemaName = OtherConfigurationConsts.LogSchemaName,
                },
                columnOptions: columnOptions,
                restrictedToMinimumLevel: LogEventLevel.Information
                )
                .CreateLogger();
        }

    }
}
