namespace KatlaSport.Services.HospitalManagment
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using AutoMapper;
    using KatlaSport.DataAccess;
    using KatlaSport.DataAccess.Hospital;
    using Microsoft.WindowsAzure.Storage.Blob;

    internal sealed class DoctorService : IDoctorService
    {
        private readonly IHospitalContext _context;

        public DoctorService(IHospitalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Doctor> CreateDoctorAsync(DoctorRequest request, CloudBlobContainer blobContainer, HttpPostedFile file)
        {
            var dbDoctors = await _context.Doctors.Where(d => (d.Surname == request.Surname && d.Name == request.Name)).ToArrayAsync();
            if (dbDoctors.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            var fileName = GetRandomBlobName(file.FileName);
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileName);
            using (var fileStream = file.InputStream)
            {
                await blob.UploadFromStreamAsync(fileStream);
            }

            request.Photo = blob.Uri.ToString();

            var doctor = Mapper.Map<DoctorRequest, Doctor>(request);
            _context.Doctors.Add(doctor);

            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task DeleteDoctorAsync(int doctorId, CloudBlobContainer blobContainer)
        {
            var dbDoctors = await _context.Doctors.Where(p => p.Id == doctorId).ToArrayAsync();
            if (dbDoctors.Length == 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            string filename = Path.GetFileName(dbDoctors[0].Photo);

            var blobDelete = blobContainer.GetBlockBlobReference(filename);
            await blobDelete.DeleteIfExistsAsync();

            _context.Doctors.Remove(dbDoctors[0]);
            await _context.SaveChangesAsync();
        }

        public async Task<DoctorRequest> GetDoctorAsync(int doctorId)
        {
            var dbDoctors = await _context.Doctors.Where(h => h.Id == doctorId).ToArrayAsync();
            if (dbDoctors.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var accountants = Mapper.Map<Doctor, DoctorRequest>(dbDoctors[0]);
            return accountants;
        }

        public async Task<List<DoctorRequest>> GetDoctorsAsync()
        {
            var dbDoctors = await _context.Doctors.OrderBy(h => h.Id).ToListAsync();
            var accountants = dbDoctors.Select(h => Mapper.Map<Doctor, DoctorRequest>(h)).ToList();

            return accountants;
        }

        public async Task<Doctor> UpdateDoctorAsync(DoctorRequest request, CloudBlobContainer blobContainer, HttpPostedFile file, int doctorId)
        {
            var dbDoctors = await _context.Doctors.Where(p => p.Name == request.Name && p.Id != doctorId).ToArrayAsync();
            if (dbDoctors.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            dbDoctors = _context.Doctors.Where(p => p.Id == doctorId).ToArray();
            if (dbDoctors.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbDoctor = dbDoctors[0];

            var fileName = GetRandomBlobName(file.FileName);
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileName);
            using (var fileStream = file.InputStream)
            {
                await blob.UploadFromStreamAsync(fileStream);
            }

            string filename = Path.GetFileName(dbDoctor.Photo);

            var blobDelete = blobContainer.GetBlockBlobReference(filename);
            await blobDelete.DeleteIfExistsAsync();

            dbDoctor.Photo = blob.Uri.ToString();
            dbDoctor.Name = request.Name;
            dbDoctor.Surname = request.Surname;
            dbDoctor.DepartmentId = request.DepartmentId;
            await _context.SaveChangesAsync();

            return dbDoctor;
        }

        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}
