using FluentValidation;

namespace KatlaSport.Services.HospitalManagment
{
    public class DoctorRequestValidator : AbstractValidator<DoctorRequest>
    {
        public DoctorRequestValidator()
        {
            RuleFor(r => r.Name).Length(3, 20);
            RuleFor(r => r.Surname).Length(3, 20);
            RuleFor(r => r.DepartmentId).GreaterThan(0);
        }
    }
}
