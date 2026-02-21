namespace HealthClinicApp.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
    }
}
