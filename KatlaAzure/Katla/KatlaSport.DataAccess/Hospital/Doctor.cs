using System.Collections.Generic;

namespace KatlaSport.DataAccess.Hospital
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Photo { get; set; }

        public Department Department { get; set; }

        public int DepartmentId { get; set; }

        public ICollection<Patient> Pacients { get; set; }
    }
}
