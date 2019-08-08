namespace KatlaSport.DataAccess.Hospital
{
    public interface IHospitalContext : IAsyncEntityStorage
    {
        IEntitySet<Department> Departments { get; }

        IEntitySet<Doctor> Doctors { get; }

        IEntitySet<Patient> Patients { get; }
    }
}
