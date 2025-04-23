using Demo.DataAccess.Models.RoleManagerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Data.Configurations
{
    public class ApplicationRoleConfigurations : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(R => R.Name).HasColumnType("varchar(50)");
            builder.Property(E => E.RoleName).
             HasConversion((RoleName) => RoleName.ToString(),
            (_RoleName) => (RoleName)Enum.Parse(typeof(RoleName), _RoleName));
        }
    }        
}
