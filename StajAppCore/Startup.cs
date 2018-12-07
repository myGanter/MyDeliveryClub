using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StajAppCore.Models;
using StajAppCore.Services;

namespace StajAppCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private string RootPath = "";

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            RootPath = env.ContentRootPath;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            string connection = Configuration.GetConnectionString("DefaultConnection");
            connection = connection.Replace("%ROOTPATH%", RootPath);

            Helpers.MyHTMLHelpers.RootPath = RootPath;

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });

            services.AddTransient<RoleMaster>();
            RoleMaster.AddRoleException("Администратор");
            RoleMaster.AddRoleForView("Администратор", "AdminPanel", new MenuItem[] 
            {
                new MenuItem() { Name = "Пользователи", Url = "tab1" },
                new MenuItem() { Name = "Курьеры", Url = "tab2" },
                new MenuItem() { Name = "Магазины", Url = "tab3" }
            });
            RoleMaster.AddRoleForView("Пользователь", "ClientPanel", new MenuItem[]
            {
                new MenuItem() { Name = "Магазины", Url = "tab1" },
                new MenuItem() { Name = "Активные заказы", Url = "tab2" }
            });
            RoleMaster.AddRoleForView("Курьер", "CourierPanel", new MenuItem[]
            {
                new MenuItem() { Name = "Активные заказы", Url = "tab1" },
                new MenuItem() { Name = "Принятые заказы", Url = "tab2" }
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}
