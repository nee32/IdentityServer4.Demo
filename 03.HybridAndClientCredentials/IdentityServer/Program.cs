using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.IO;
using System.Threading;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, logger) =>
                {
                    //logger.ReadFrom.loggeruration(ctx.loggeruration)
                    //.Enrich.FromLogContext()
                    //.WriteTo.Console();

                    logger.MinimumLevel.Information()// 最小的日志输出级别
                        .MinimumLevel.Debug()// 最小的日志输出级别
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//日志调用类命名空间如果以Microsoft开头，覆盖日志输出最小级别为Information
                        .MinimumLevel.Override("System", LogEventLevel.Warning)
                        .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                        .Enrich.With(new ThreadIdEnricher());//.FromLogContext();

                    //if (ctx.HostingEnvironment.IsDevelopment())
                    //{
                    //    logger.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}");
                    //}
                    //else if (ctx.HostingEnvironment.IsProduction())
                    //{
                    //    logger.WriteTo.File(Path.Combine("logs", @"log.txt"),
                    //        fileSizeLimitBytes: 1_000_000,
                    //        rollOnFileSizeLimit: true,
                    //        shared: true,
                    //        rollingInterval: RollingInterval.Day);
                    //}
                    logger.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
                          .WriteTo.File(@"log.txt",
                            fileSizeLimitBytes: 1_000_000,
                            rollOnFileSizeLimit: true,
                            shared: true,
                            restrictedToMinimumLevel: LogEventLevel.Information, //Information或更高的写入文件
                            rollingInterval: RollingInterval.Day);

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }

    class ThreadIdEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "ThreadId", Thread.CurrentThread.ManagedThreadId));
        }
    }
}
