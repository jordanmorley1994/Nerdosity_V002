using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Nerdosity_V002.Models;

namespace Nerdosity_V002
{
    public class Startup
    {
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            // Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddRouting(options => options.LowercaseUrls = true);

                services.AddMemoryCache();
                services.AddSession();

                services.AddControllersWithViews().AddNewtonsoftJson();

                services.AddDbContext<NerdosityContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("NerdosityContext")));

                // add this
                services.AddIdentity<User, IdentityRole>(options => {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                }).AddEntityFrameworkStores<NerdosityContext>()
                  .AddDefaultTokenProviders();
            }

            // Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app)
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization(); 

                app.UseSession();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapAreaControllerRoute(
                        name: "admin",
                        areaName: "Admin",
                        pattern: "Admin/{controller=Product}/{action=Index}/{id?}");

                    endpoints.MapControllerRoute(
                        name: "",
                        pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}/filter/{manufacturer}/{category}/{price}");

                    // route for paging and sorting only
                    endpoints.MapControllerRoute(
                        name: "",
                        pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}");

                    // default route
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");
                });

                NerdosityContext.CreateAdminUser(app.ApplicationServices).Wait();
            }
        }
    }
