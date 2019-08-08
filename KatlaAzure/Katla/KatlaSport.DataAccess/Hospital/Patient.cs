namespace KatlaSport.DataAccess.Hospital
{
    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public Doctor Doctor { get; set; }

        public int DoctorId { get; set; }

        public int CaseHistoryNumber { get; set; }
    }
}
