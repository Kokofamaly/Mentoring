using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog.Extensions.Logging;
using Serilog.AspNetCore;
using Serilog.Debugging;
using Serilog;
using BrainstormSessions.Infrastructure;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration().CreateLogger();
                
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog((hostingContext, loggerConfiguration) => {
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);

                loggerConfiguration.WriteTo.Sink(new EmailSink(
                    hostingContext.Configuration["EmailSettings:From"],
                    hostingContext.Configuration["EmailSettings:To"],
                    hostingContext.Configuration["EmailSettings:Smtp"],
                    int.Parse(hostingContext.Configuration["EmailSettings:Port"]),
                    hostingContext.Configuration["EmailSettings:User"],
                    hostingContext.Configuration["EmailSettings:Pass"]

                    ));
                
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
