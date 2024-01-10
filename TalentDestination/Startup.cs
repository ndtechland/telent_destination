using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.Security.Policy;
using TalentDestination.DataAccess;
using TalentDestination.Interface;
using TalentDestination.Business;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;

namespace TalentDestination
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
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15); // Set session timeout
            });

            services.AddDbContext<talent_Context>(options => options.UseSqlServer(Configuration.GetConnectionString("db1")));
           

            services.AddControllersWithViews();
            
            services.AddScoped<ITalnetOperations, TalnetOperations>();

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 200 * 1024 * 1024; // 200 MB limit
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Admin}/{action=Login}/{id?}");
            });
        }
    }
}
