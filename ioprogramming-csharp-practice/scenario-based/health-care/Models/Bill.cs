using System;

namespace HealthCareClinicSystem.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public int VisitId { get; set; }
        public string PatientName { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentMode { get; set; }
    }
}