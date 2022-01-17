using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoesStore.Data;
using ShoesStore.Data.Infrastructure.Extensions;
using ShoesStore.Data.Models;
using ShoesStore.Services.Sellers;
using ShoesStore.Services.Shoes;
using ShoesStore.Services.Statistics;
using Microsoft.AspNetCore.Mvc;


namespace ShoesStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShoesStoreDbContext>(options => options
            .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ShoesStoreDbContext>();


            services.AddAutoMapper(typeof(Startup));

            services.AddMemoryCache();

            services.AddControllersWithViews(options => 
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<ISellerService, SellerService>();
            services.AddTransient<IShoeService, ShoeService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
               .UseStaticFiles()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllerRoute(
                       name: "Areas",
                       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                   endpoints.MapControllerRoute(
                       name: "Shoe Details",
                       pattern: "/Shoes/Details/{id}/{information}",
                       defaults:  new { controller = "Shoes", action = "Details"}); 

                   endpoints.MapDefaultControllerRoute();
                   endpoints.MapRazorPages();
               });
        }
    }
}
