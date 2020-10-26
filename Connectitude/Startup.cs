using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connectitude.Database;
using Connectitude.Models;
using Connectitude.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Connectitude
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
            services.AddControllersWithViews();
            services.AddScoped<HomeRepository>();
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase(databaseName: "automation"));

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ApiContext context, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            defaultPopulate(context);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private static void defaultPopulate(ApiContext context)
        {
            HomeModel connectitudeHome = new HomeModel
            {
                Name = "Connectitude"
            };

            HomeModel jamesHome = new HomeModel
            {
                Name = "James"
            };

            RoomModel kitchen = new RoomModel
            {
                Name = "Kök",
                Temperature = 28,
                Humidity = 0.95f,
                HomeId = 1
            };
            RoomModel livingRoom = new RoomModel
            {
                Name = "Vardagsrum",
                Temperature = 21,
                Humidity = 0.2f,
                HomeId = 1
            };
            RoomModel bedRoom = new RoomModel
            {
                Name = "Sovrum",
                Temperature = 20,
                Humidity = 0.31f,
                HomeId = 2
            };
            context.Homes.Add(connectitudeHome);
            context.Homes.Add(jamesHome);
            context.Rooms.Add(kitchen);
            context.Rooms.Add(livingRoom);
            context.Rooms.Add(bedRoom);

            context.SaveChanges();
        }

    }
}
