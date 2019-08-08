namespace KatlaSport.Services.HospitalManagment
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;
    using KatlaSport.DataAccess.Hospital;
    using Microsoft.WindowsAzure.Storage.Blob;

    public interface IDoctorService
    {
        Task<List<DoctorRequest>> GetDoctorsAsync();

        Task<DoctorRequest> GetDoctorAsync(int doctorId);

        Task<Doctor> CreateDoctorAsync(DoctorRequest request, CloudBlobContainer blobContainer, HttpPostedFile file);

        Task<Doctor> UpdateDoctorAsync(DoctorRequest request, CloudBlobContainer blobContainer, HttpPostedFile file, int doctorId);

        Task DeleteDoctorAsync(int doctorId, CloudBlobContainer blobContainer);
    }
}
