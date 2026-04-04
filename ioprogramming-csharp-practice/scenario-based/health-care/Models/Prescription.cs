namespace HealthCareClinicSystem.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int VisitId { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
    }
}