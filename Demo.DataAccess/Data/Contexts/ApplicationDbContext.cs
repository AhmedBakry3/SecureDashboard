
using Demo.DataAccess.Data.Configurations;
using Demo.DataAccess.Models.DepartmentModel;
using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Models.RoleManagerModel;
using Demo.DataAccess.Models.UserManagerModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Demo.DataAccess.Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser , ApplicationRole,string>(options)
    {
        public DbSet<Department> Departments { get; set; }  
        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
                modelBuilder.Entity<ApplicationUser>()
               .HasDiscriminator<string>("Discriminator")
               .HasValue<ApplicationUser>("ApplicationUser")
               .HasValue<UserManager>("UserManager");

            base.OnModelCreating(modelBuilder);

        }
    }
}
