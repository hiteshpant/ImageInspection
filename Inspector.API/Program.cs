using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Inspector.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables()
               .AddCommandLine(args)
               .Build();
            CreateHostBuilder(args, config).Run();
        }

        public static IWebHost CreateHostBuilder(string[] args, IConfigurationRoot config) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseConfiguration(config)
                    .UseStartup<Startup>()
                    .Build();

    }

}
