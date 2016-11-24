using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreWebAppTest;

namespace ConsoleApplication
{
    public class Startup
    {
		public IConfigurationRoot Configuration { get; }
		
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

			var connectionString = Configuration["DbContextSettings:ConnectionString"];
			services.AddDbContext<DatabaseContext>(opts => opts.UseNpgsql(connectionString));
        }

        //public void Configure(IApplicationBuilder app)
        //{
		//    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //        name: "default",
        //        template: "{controller=Home}/{action=Time}");
        //    });
        //}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc(routes =>
		   {
			   routes.MapRoute(
			   name: "default",
			   template: "{controller=Home}/{action=Time}");
		   });
		}
    }
}