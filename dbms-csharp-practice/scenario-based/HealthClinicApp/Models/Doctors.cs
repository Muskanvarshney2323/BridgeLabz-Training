namespace HealthClinicApp.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Specialty { get; set; } = null!;
        public decimal ConsultationFee { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
