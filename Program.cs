using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using BangazonAPI;


namespace BangOrientAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder() 
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder() //follows builder pattern to create webhost
                .UseConfiguration(config)
                .UseKestrel() //this is particular web server- could use others
                .UseContentRoot(Directory.GetCurrentDirectory()) //method on webhostbuilder that specifies root directory
                .UseIISIntegration()  //method on webhostbuilder to host IIS and IIS express
                .UseStartup<Startup>()  //specifies startup class for app. in startup.cs
                .Build(); //method builds Iwebhost to host the app

            host.Run(); //method that runs app and starts listening for HTTP calls
        }
    }
}
