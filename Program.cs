using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PustokBook.Areas.Admin.Helpers;
using PustokBook.Contexts;
using PustokBook.Models;
using PustokBook.Services.Interfaces;
using PustokBook.Services.Iplement;

namespace PustokBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<PustokDbContexts>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
            })
            .AddIdentity<User, IdentityRole>(opt =>
                {
                    opt.SignIn.RequireConfirmedEmail = true;
                    opt.User.RequireUniqueEmail = true;
                    opt.Lockout.MaxFailedAccessAttempts = 5;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    opt.Password.RequiredUniqueChars = 2;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequiredLength = 4;
                    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
                })
             .AddDefaultTokenProviders()
             .AddEntityFrameworkStores<PustokDbContexts>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Auth/Login");
                options.LogoutPath = new PathString("/Auth/Logout");

                options.Cookie = new()
                {
                    Name = "IdentityCookie",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.Always
                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });

            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
               opt.TokenLifespan = TimeSpan.FromHours(2));

            //builder.Services.AddSession();
            //builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IEmailService, EmailService>();
            //builder.Services.AddScoped<IConfiguration, ConfigurationManager>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            PathConstants.RootPath = app.Environment.WebRootPath;
            app.Run();
        }
    }
}