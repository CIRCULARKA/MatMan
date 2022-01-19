using System;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MatMan.Data;

namespace MatMan.UI
{
    public class Startup
    {
        private readonly IConfiguration _config;

        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            var conString = _config["ConnectionString"];

            services.AddDbContext<ApplicationDbContext>(
                options =>
                    options.UseSqlServer(_config["ConnectionString"])
            );

            services.AddApplicationServices();

            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            var urlsRewritingOptions = new RewriteOptions();

            using (var context =
                app.ApplicationServices.CreateScope().
                    ServiceProvider.GetService<ApplicationDbContext>()
            ) { context.Database.Migrate(); }

            app.PopulateDatabase();

            app.UseDeveloperExceptionPage();

            // if (env.IsDevelopment())
            // else
            // {
            //     app.UseExceptionHandler("/Error");
            //     app.UseHsts();
            // }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        "default",
                        "{controller=Materials}/{action=GetMaterialsListView}"
                    );
                }
            );
        }
    }
}
