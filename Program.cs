using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using log4net;
using log4net.Config;
using System.Reflection;
using System.IO;
using System.Xml;
using Microsoft.Extensions.Hosting;

namespace sandalphon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoadLog4netConfig();
            CreateHostBuilder(args).Build().Run();
        }

        private static void LoadLog4netConfig()
        {
            var repository = LogManager.CreateRepository(
                    Assembly.GetEntryAssembly(),
                    typeof(log4net.Repository.Hierarchy.Hierarchy)
                );
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
     Host.CreateDefaultBuilder(args)
         .ConfigureWebHostDefaults(webBuilder =>
         {
             webBuilder.ConfigureKestrel(serverOptions =>
             {
                 // Set properties and call methods on options
             })
             .UseStartup<Startup>();
         });
    }
}
