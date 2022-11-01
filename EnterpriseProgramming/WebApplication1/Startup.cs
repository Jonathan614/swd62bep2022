using BusinessLogic.Services;
using DataAccess.Context;
using DataAccess.Repositories;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShoppingCartContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ShoppingCartContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            /*
             * 
             * Singleton: IoC container will create and share a single instance of a service 
             * throughout the application's lifetime.
             * e.g 50 concurrent users making 5 ItemsRepository calls, that means that only 1 instance of ItemsRepository is created for everyone
             * 
             * 
               Transient: The IoC container will create a new instance of the specified service type every time you ask for it.
            e.g. 50 concurrent users making 3 ItemsRepository calls, that means 50 x 3= 150 instances in memory

               Scoped: IoC container will create an instance of the specified service type once per request and will be shared in a single request.
              e.g. 50 concurrent users making 3 ItemsRepository calls, that means 50 x 1= 50 instances in memory

             * 
             */

            services.AddScoped<ItemsRepository>();
            services.AddScoped<ItemsService>();
 
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            
        // services.AddScoped<ICategoriesRepository, CategoriesFileRepository>(provider => new CategoriesFileRepository((@"C:\Users\attar\source\repos\swd62bep2022\EnterpriseProgramming\WebApplication1\Data\categories.txt")));
         

            

           

            services.AddScoped<CategoriesService>();

 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
