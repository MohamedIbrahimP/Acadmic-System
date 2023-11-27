using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mvc_day2.Filters;
using Mvc_day2.Migrations;
using Mvc_day2.Models;
using Mvc_day2.Repository.CourseRepository;
using Mvc_day2.Repository.CourseResultRepository;
using Mvc_day2.Repository.DepartmentRepository;
using Mvc_day2.Repository.IdentityRepository;
using Mvc_day2.Repository.InstructorRepository;
using Mvc_day2.Repository.StudentRepository;

namespace Mvc_day2
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //////REgister Custom Services
            ///            
            builder.Services.AddDbContext<dbLogic>(Option =>Option.UseSqlServer(builder.Configuration.GetConnectionString("db")) );
            builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromHours(14); });

            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ICourseResultRepository, CourseResultRepository>();
            builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();




            builder.Services.AddIdentity<ApplicationIdentityUser, IdentityRole>().AddEntityFrameworkStores<dbLogic>();

            ///////////////////////////////////
            ///
            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();





            builder.Services.Configure<SecurityStampValidatorOptions>(options =>
             {
                 options.ValidationInterval = TimeSpan.Zero;
             });


            //////////////////////////////////////////////





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Dept",
                pattern: "Dept",
                defaults :new {controller="Department" , Action ="Index"});

            app.MapControllerRoute("LogIn", "LogIn", new { controller = "Account", Action = "LogIn" });

            app.MapControllerRoute("Rsgister", "SignUp", new { controller = "Account", Action = "Register" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}