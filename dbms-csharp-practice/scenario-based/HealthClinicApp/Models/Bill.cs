namespace HealthClinicApp.Models
{
    public class Bill
    {
        public int BillID { get; set; }
        public int VisitID { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
