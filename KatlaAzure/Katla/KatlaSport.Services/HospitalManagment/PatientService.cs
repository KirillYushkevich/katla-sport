namespace KatlaSport.Services.HospitalManagment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using KatlaSport.DataAccess;
    using KatlaSport.DataAccess.Hospital;

    internal sealed class PatientService : IPatientService
    {
        private readonly IHospitalContext _context;

        public PatientService(IHospitalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Patient> CreatePatientAsync(PatientRequest request)
        {
            var dbPatients = await _context.Patients.Where(d => (d.Name == request.Name)).ToArrayAsync();
            if (dbPatients.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            var patietnts = Mapper.Map<PatientRequest, Patient>(request);
            _context.Patients.Add(patietnts);

            await _context.SaveChangesAsync();

            return patietnts;
        }

        public async Task DeletePatientAsync(int patientId)
        {
            var dbPatients = await _context.Patients.Where(p => p.Id == patientId).ToArrayAsync();
            if (dbPatients.Length == 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            _context.Patients.Remove(dbPatients[0]);
            await _context.SaveChangesAsync();
        }

        public async Task<PatientRequest> GetPatientAsync(int patientId)
        {
            var dbPatients = await _context.Patients.Where(d => d.Id == patientId).ToArrayAsync();
            if (dbPatients.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var patients = Mapper.Map<Patient, PatientRequest>(dbPatients[0]);
            return patients;
        }

        public async Task<List<PatientRequest>> GetPatientssAsync()
        {
            var dbPatients = await _context.Patients.OrderBy(d => d.Id).ToListAsync();
            var patients = dbPatients.Select(d => Mapper.Map<Patient, PatientRequest>(d)).ToList();

            return patients;
        }

        public async Task<Patient> UpdatePatientAsync(PatientRequest request, int patientId)
        {
            var dbPatietns = await _context.Patients.Where(d => d.Name == request.Name && d.Id != patientId).ToArrayAsync();
            if (dbPatietns.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            dbPatietns = _context.Patients.Where(p => p.Id == request.Id).ToArray();
            if (dbPatietns.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbPatient = dbPatietns[0];

            dbPatient.Name = request.Name;
            dbPatient.Age = request.Age;
            dbPatient.CaseHistoryNumber = request.CaseHistoryNumber;
            dbPatient.DoctorId = dbPatient.DoctorId;
            dbPatient.Surname = dbPatient.Surname;
            await _context.SaveChangesAsync();

            return dbPatient;
        }
    }
}
