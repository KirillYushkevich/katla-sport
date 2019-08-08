namespace KatlaSport.Services.HospitalManagment
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using KatlaSport.DataAccess.Hospital;

    public interface IPatientService
    {
        Task<List<PatientRequest>> GetPatientssAsync();

        Task<PatientRequest> GetPatientAsync(int patientId);

        Task<Patient> CreatePatientAsync(PatientRequest request);

        Task<Patient> UpdatePatientAsync(PatientRequest request, int patientId);

        Task DeletePatientAsync(int patientId);
    }
}
