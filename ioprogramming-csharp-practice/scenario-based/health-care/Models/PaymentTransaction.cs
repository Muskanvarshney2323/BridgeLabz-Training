using System;

namespace HealthCareClinicSystem.Models
{
    public class PaymentTransaction
    {
        public int TransactionId { get; set; }
        public int BillId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}