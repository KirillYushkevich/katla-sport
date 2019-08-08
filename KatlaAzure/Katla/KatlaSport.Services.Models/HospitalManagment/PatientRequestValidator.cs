using FluentValidation;

namespace KatlaSport.Services.HospitalManagment
{
    public class PatientRequestValidator : AbstractValidator<PatientRequest>
    {
        public PatientRequestValidator()
        {
            RuleFor(r => r.Age).GreaterThan(0);
            RuleFor(r => r.CaseHistoryNumber).GreaterThan(0);
            RuleFor(r => r.Name).Length(4, 20);
            RuleFor(r => r.Surname).Length(4, 20);
            RuleFor(r => r.DoctorId).GreaterThan(0);
        }
    }
}
