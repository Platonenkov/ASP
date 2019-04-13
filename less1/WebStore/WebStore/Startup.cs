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
using WebStore.Controllers.Implementations;
using WebStore.Controllers.Interfaces;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Infrastructure.Implementations;
using WebStore.Infrastructure.Interfaces;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

         public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<WebStoreContextInitializer>();

            services.AddDbContext<WebStoreContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConection")));
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, WebStoreContextInitializer db)
        {
            db.InitializeAsync().Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
