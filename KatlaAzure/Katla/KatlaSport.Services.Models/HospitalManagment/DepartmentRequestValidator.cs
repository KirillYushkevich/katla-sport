using FluentValidation;

namespace KatlaSport.Services.HospitalManagment
{
    public class DepartmentRequestValidator : AbstractValidator<DepartmentRequest>
    {
        public DepartmentRequestValidator()
        {
            RuleFor(r => r.Name).Length(5, 60);
            RuleFor(r => r.Description).Length(0, 300);
        }
    }
}
