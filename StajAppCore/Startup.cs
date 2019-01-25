using StajAppCore.Models;
using StajAppCore.Services;
using StajAppCore.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StajAppCore.Services.MessageSending;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using StajAppCore.Services.MessageSending.MailMass;
using StajAppCore.Services.Repositories.RepositoryBuilder;

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

            Role admin = new Role { Id = 1, Name = "Администратор" };
            Role user = new Role { Id = 2, Name = "Пользователь" };
            Role courier = new Role { Id = 3, Name = "Курьер" };
            RoleMaster.AddRoles(new Role[] { admin, user, courier });

            RoleMaster.AddRoleException(admin.Name);
            RoleMaster.AddRoleForView(admin.Name, "Admin", "AdminPanel", new MenuItem[] 
            {
                new MenuItem() { Name = "Пользователи", Url = "Users" },
                new MenuItem() { Name = "Курьеры", Url = "Couriers" },
                new MenuItem() { Name = "Магазины", Url = "Shops" }
            });
            RoleMaster.AddRoleForView(user.Name, "User", "ClientPanel", new MenuItem[]
            {
                new MenuItem() { Name = "Магазины", Url = "Shops" },
                new MenuItem() { Name = "Активные заказы", Url = "ActiveOrders" }
            });
            RoleMaster.AddRoleForView(courier.Name, "Courier", "CourierPanel", new MenuItem[]
            {
                new MenuItem() { Name = "Заказы пользователей", Url = "AllOrders" },
                new MenuItem() { Name = "Принятые заказы", Url = "ActiveCourierOrders" }
            });

            services.AddTransient<IRepositoryBuilder, RepositoryBuilder>();
            services.AddTransient<PasswdHesher<IHesh>>();            

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionDBLog));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            MailOptions mailSendOpt = Configuration.
                                      GetSection("EmailConfiguration").
                                      Get<MailOptions>();
            MailMassService.MailPagePath = $"{RootPath}//wwwroot//html//MailPage.html";
            services.AddScoped<IMass>(provider => 
                new MailMassService(mailSendOpt, 
                    new ExceptionDBLog(
                        new ApplicationContext(
                            new DbContextOptionsBuilder<ApplicationContext>()
                                .UseSqlServer(connection)
                                    .Options))));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Main/CriticalError");
            //    app.UseHsts();
            //}

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
