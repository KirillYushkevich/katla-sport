using FluentValidation.Attributes;

namespace KatlaSport.Services.HospitalManagment
{
    [Validator(typeof(PatientRequestValidator))]
    public class PatientRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public int DoctorId { get; set; }

        public int CaseHistoryNumber { get; set; }
    }
}
