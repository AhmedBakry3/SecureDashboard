

using Demo.DataAccess.Models.DepartmentModel;

namespace Demo.DataAccess.Data.Configurations
{
    internal class DepartmentConfigurations : BaseEntityConfigurations<Department> ,  IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("Varchar(20)");
            builder.Property(D => D.Code).HasColumnType("Varchar(20)");
            base.Configure(builder);
        }
    }
}
