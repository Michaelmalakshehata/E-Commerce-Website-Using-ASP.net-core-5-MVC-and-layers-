using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.Repositories;
using E_Commerce_Website.BL.Repositories.EmailService;
using E_Commerce_Website.DAL;
using E_Commerce_Website.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Website
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
            // Add services to the container.
            services.AddDbContext<EntityContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("E-Commerce"));
            });
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Default Password settings.
                //options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<EntityContext>()
                       .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;

            });
            // Add services
            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericService<>));
            services.AddScoped(typeof(IServiceCategory), typeof(ServiceCategory));
            services.AddScoped(typeof(IServiceProduct), typeof(ServiceProduct));
            services.AddScoped(typeof(IServiceShowMenu), typeof(ServiceShowMenu));
            services.AddScoped(typeof(IServiceCart), typeof(ServiceCart));
            services.AddScoped(typeof(IServiceWishList), typeof(ServiceWishList));
            services.AddScoped(typeof(IServiceOrder), typeof(ServiceOrder));
            services.AddScoped(typeof(IServiceFinishOrder), typeof(ServiceFinishedOrders));
            services.AddScoped(typeof(IServiceComments), typeof(ServiceComments));

            //end services
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
