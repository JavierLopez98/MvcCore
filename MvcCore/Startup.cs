using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCore.Data;
using MvcCore.Helpers;
using MvcCore.Interfaces;
using MvcCore.Repository;

namespace MvcCore
{
    public class Startup
    {

        IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            String cadenaSql = this.configuration.GetConnectionString("cadenasqlcasa");
            String cadenaOracle = this.configuration.GetConnectionString("cadenaoraclecasa");
            String cadenaMySql = this.configuration.GetConnectionString("cadenamysql");
            services.AddTransient<PathProvider>();
            services.AddTransient<RepositoryJoyerias>();
            services.AddTransient<RepositoryAlumno>();
            //services.AddTransient<IRepositoryDepartamentos, RepositoryDepartamentosSql>();
            //services.AddTransient<IRepositoryDepartamentos> (x=>new RepositoryDepartamentosOracle(cadenaOracle));
            services.AddTransient<IRepositoryDepartamentos>(x => new RepositoryDepartamentosMysql(cadenaMySql));
            services.AddDbContext<DepartamentosContext>(options=>options.UseSqlServer(cadenaSql));

            

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
