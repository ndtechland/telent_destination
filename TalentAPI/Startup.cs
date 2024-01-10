using Microsoft.EntityFrameworkCore;
using System;
using TalnetAPI.Business;
using TalnetAPI.DataAccess;
using TalnetAPI.Interface;

namespace TalnetAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15); // Set session timeout
            });

            services.AddDbContext<talent_Context>(options => options.UseSqlServer(Configuration.GetConnectionString("db1")));


            services.AddControllers();
            services.AddScoped<ITalnetOperations, TalnetOperations>();
            services.AddDistributedMemoryCache();




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
                // Configure production error handling here
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Enable attribute routing
            });
        }
    }
}
