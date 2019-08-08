using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.Hospital
{
    internal sealed class PatirntConfiguration : EntityTypeConfiguration<Patient>
    {
        public PatirntConfiguration()
        {
            ToTable("patients");
            HasKey(i => i.Id);
            HasRequired(i => i.Doctor).WithMany(i => i.Pacients).HasForeignKey(i => i.DoctorId);
            Property(i => i.Id).HasColumnName("patient_id");
            Property(i => i.Name).HasColumnName("patient_name");
            Property(i => i.Surname).HasColumnName("patient_surname");
            Property(i => i.Age).HasColumnName("patient_age");
            Property(i => i.CaseHistoryNumber).HasColumnName("patient_casehistorynumber");
        }
    }
}
