using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ASCOMCore
{
    public class Program
    {
        public static FWSimulator Simulator;
        public static TraceLogger TraceLogger;

        public const int ASCOM_ERROR_NUMBER_OFFSET = unchecked((int)0x80040000); // int value of the ASCOM COM error code range

        public static void Main(string[] args)
        {
            Simulator = new FWSimulator();
            TraceLogger = new TraceLogger("FWSimulator");
            TraceLogger.Enabled = true;
            TraceLogger.LogMessage("Main", "Logger created");
            
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseIISIntegration()
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .Build();

            TraceLogger.LogMessage("Main", "Running host");
            host.Run();
            TraceLogger.LogMessage("Main", "Program end");

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();



    }
}
