using AutoMapper;
using DataAccessDepartment = KatlaSport.DataAccess.Hospital.Department;
using DataAccessDoctor = KatlaSport.DataAccess.Hospital.Doctor;
using DataAccessPatient = KatlaSport.DataAccess.Hospital.Patient;

namespace KatlaSport.Services.HospitalManagment
{
    public sealed class HospitalManagmentMappingProfile : Profile
    {
        public HospitalManagmentMappingProfile()
        {
            CreateMap<DepartmentRequest, DataAccessDepartment>();
            CreateMap<DataAccessDepartment, DepartmentRequest>();
            CreateMap<DataAccessDoctor, DoctorRequest>();
            CreateMap<DoctorRequest, DataAccessDoctor>();
            CreateMap<DataAccessPatient, PatientRequest>();
            CreateMap<PatientRequest, DataAccessPatient>();
        }
    }
}
