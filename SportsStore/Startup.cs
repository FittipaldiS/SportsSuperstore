using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        //IConfiguration access to the ASP.NET Core Configuration System
        private IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<StoreDbContext>(opts => {
                opts.UseSqlServer(
                Configuration["ConnectionStrings:SportsStoreConnection"]);
            });

            /*Il metodo AddScoped crea un servizio in cui ogni richiesta HTTP ottiene il proprio oggetto repository, che � il modo in cui Entity
                Framework Core � tipicamente usato.*/
            services.AddScoped<IStoreRepository, EFStoreRepository>();
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseStaticFiles();
                app.UseSession();
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute("catpage",
                         "{category}/Page{productPage:int}",
                         new { Controller = "Home", action = "Index" });

                    endpoints.MapControllerRoute("page", "Page{productPage:int}",
                         new { Controller = "Home", action = "Index", productPage = 1 });

                    endpoints.MapControllerRoute("category", "{category}",
                         new { Controller = "Home", action = "Index", productPage = 1 });

                    endpoints.MapControllerRoute("pagination",
                              "Products/Page{productPage}",
                         new { Controller = "Home", action = "Index", productPage = 1 });

                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
            SeedData.EnsurePopulated(app);
        }
    }
}
