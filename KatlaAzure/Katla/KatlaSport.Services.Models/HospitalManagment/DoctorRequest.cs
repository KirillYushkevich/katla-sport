using FluentValidation.Attributes;

namespace KatlaSport.Services.HospitalManagment
{
    [Validator(typeof(DoctorRequestValidator))]
    public class DoctorRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Photo { get; set; }

        public int DepartmentId { get; set; }
    }
}
