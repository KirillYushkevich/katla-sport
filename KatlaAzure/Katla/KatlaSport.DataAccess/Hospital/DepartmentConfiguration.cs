using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.Hospital
{
    internal sealed class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
        {

            ToTable("departments");
            HasKey(i => i.Id);
            Property(i => i.Id).HasColumnName("department_id");
            Property(i => i.Name).HasColumnName("department_name");
            Property(i => i.Description).HasColumnName("department_description");
            Property(i => i.ParentId).HasColumnName("department_parentid");
        }
    }
}
