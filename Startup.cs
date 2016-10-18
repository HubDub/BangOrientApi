using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Bangazon.Data;
using Microsoft.EntityFrameworkCore;
// all of the System and Microsoft usings are predetermined class libraries that have built in properties and methods
//the namespace is the name of your app
namespace BangazonAPI  
{
    //the startup class is where you define the request handling pipeline and where any services needed by app are configured. it must be public and must contain Configure Services and Configure methods
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Console.WriteLine("Startup");
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("ConfigureServices");
            // Add framework services.

            services.AddMvc(); //something about middleware 

            // Add CORS framework //this is where you specify who is allowed to request your data. in this he's open to anyone. this is most open policy possible.
            services.AddCors(options =>
            {
                options.AddPolicy("AllowDevelopmentEnvironment",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            string path = System.Environment.GetEnvironmentVariable("Bangazon_Db_Path");
            var connection = $"Filename={path}";
            Console.WriteLine($"connection = {connection}");
            services.AddDbContext<BangazonContext>(options => options.UseSqlite(connection)); //fat arrow is lambda? 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. Configure in particular defines the middleware in the pipeline. middleware performs logic and then either invokes the next middleware in the sequence or terminates the requests directly. you generally "use" middleware by taking a dependency on a NuGet package and invoking a corresponding Usexyz extension  method on the IApplicationBuilder in the configure method. I think app.UseMvc() is this. MVC adds routing the middleware pipeline as a part of configuration. 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Console.WriteLine("Configure");
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}