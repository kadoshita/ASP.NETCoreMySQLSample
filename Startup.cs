using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ASP.NETCoreMySQLSample.Data;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreMySQLSample
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            if (_env.IsDevelopment())
            {
                services.AddDbContext<UserContext>(options => options.UseMySql(Configuration.GetConnectionString("UserContext")));
            }
            else
            {
                String connectionString = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb");
                Regex reg = new Regex(@"Database=(?<database>.*?);Data Source=(?<address>.*?):(?<port>.*?);User Id=(?<user>.*?);Password=(?<password>.*?)$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match match = reg.Match(connectionString);
                connectionString = $"server={match.Groups["address"].Value};port={match.Groups["port"].Value};user={match.Groups["user"].Value};password={match.Groups["password"].Value};Database={match.Groups["database"].Value}";
                services.AddDbContext<UserContext>(options => options.UseMySql(connectionString));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<UserContext>())
                {
                    context.Database.Migrate();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
