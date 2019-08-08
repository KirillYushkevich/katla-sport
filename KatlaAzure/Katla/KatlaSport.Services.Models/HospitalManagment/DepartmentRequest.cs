using FluentValidation.Attributes;

namespace KatlaSport.Services.HospitalManagment
{
    [Validator(typeof(DepartmentRequestValidator))]
    public class DepartmentRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }
    }
}
