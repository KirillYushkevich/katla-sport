namespace KatlaSport.DataAccess.Hospital
{
    internal sealed class HospitalContext : DomainContextBase<ApplicationDbContext>, IHospitalContext
    {
        public HospitalContext(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEntitySet<Department> Departments => GetDbSet<Department>();

        public IEntitySet<Doctor> Doctors => GetDbSet<Doctor>();

        public IEntitySet<Patient> Patients => GetDbSet<Patient>();
    }
}
