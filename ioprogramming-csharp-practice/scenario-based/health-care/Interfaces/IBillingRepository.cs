using System;
using System.Collections.Generic;
using HealthCareClinicSystem.Models;

namespace HealthCareClinicSystem.Interfaces
{
    public interface IBillingRepository
    {
        int GenerateBill(int visitId, decimal consultationFee, decimal additionalCharges);
        bool RecordPayment(int billId, decimal amount, string paymentMode);
        List<Bill> GetOutstandingBills();
        Dictionary<string, decimal> GetRevenueReport(
            DateTime startDate,
            DateTime endDate,
            string groupBy
        );
        Bill GetBillByVisitId(int visitId);
    }
}