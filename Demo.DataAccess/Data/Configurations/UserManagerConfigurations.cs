using Demo.DataAccess.Models.UserManagerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Data.Configurations
{
    public class UserManagerConfigurations : IEntityTypeConfiguration<UserManager>
    {
        public void Configure(EntityTypeBuilder<UserManager> builder)
        {
            builder.HasOne(E => E.Roles)
                .WithMany(R => R.userManagers)
                .HasForeignKey(e => e.RolesId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }   
}
