using HerbShop.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HerbShop
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
            services.Configure<CookiePolicyOptions>(conf =>
            {
                conf.CheckConsentNeeded = context => true;
                conf.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            services.AddSession((opt) =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(15);
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
            });
            services.AddHttpContextAccessor();
            services.AddTransient<ICookies, Cookies>();
            services.AddDbContext<HerbsDbContext>();

            services.AddMvc((opt) => opt.EnableEndpointRouting = false)
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();
        }
    }
}
