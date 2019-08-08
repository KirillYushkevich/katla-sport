using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.Hospital
{
    internal sealed class DoctorConfiguration : EntityTypeConfiguration<Doctor>
    {
        public DoctorConfiguration()
        {
            ToTable("doctors");
            HasKey(i => i.Id);
            HasRequired(i => i.Department).WithMany(i => i.Doctors).HasForeignKey(i => i.DepartmentId);
            Property(i => i.Id).HasColumnName("doctor_id");
            Property(i => i.Name).HasColumnName("doctor_name");
            Property(i => i.DepartmentId).HasColumnName("doctor_departmentid");
            Property(i => i.Surname).HasColumnName("doctor_surname");
            Property(i => i.Photo).HasColumnName("doctor_photo");
        }
    }
}
