using Demo.BusinessLogic.Profiles;
using Demo.BusinessLogic.Services.AttachmentService;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Models.RoleManagerModel;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using Demo.Presentation.Helper;
using Demo.Presentation.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container
            builder.Services.AddControllersWithViews(Options =>
            {
                Options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            //builder.Services.AddScoped<ApplicationDbContext>(); // 2.Register to Service in DI Container
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();

            } );

            //builder.Services.AddScoped<DepartmentRepository>();
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            builder.Services.AddScoped<IDepartmentService , DepartmentService>();

            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //builder.Services.AddAutoMapper(typeof(ProjectReference).Assembly);
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUnitOfWork ,UnitOfWork>();
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            builder.Services.AddTransient<IMailService, MailService>(); 
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline.

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            #endregion

            app.Run();
        }
    }
}
