using System.Collections.Generic;

namespace KatlaSport.DataAccess.Hospital
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public virtual Department Parent { get; set; }

        public ICollection<Department> Children { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
