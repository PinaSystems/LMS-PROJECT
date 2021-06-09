using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace OAWA.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Warning()
              .MinimumLevel.Override("SerilogDemo", LogEventLevel.Warning)
              .WriteTo.File("Logs/Logs.txt",
              rollingInterval: RollingInterval.Day,
              retainedFileCountLimit: null,
              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
              shared: true)
              .CreateLogger();
            CreateWebHostBuilder(args).Build().Run();
          
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(o => 
                { 
                    o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(20);
                    o.Limits.MinRequestBodyDataRate = null;
                   o.Limits.MaxRequestBodySize=int.MaxValue;
                })
            .UseSerilog();
    }
}
