using System;

namespace HealthCareClinicSystem.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal ConsultationFee { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}