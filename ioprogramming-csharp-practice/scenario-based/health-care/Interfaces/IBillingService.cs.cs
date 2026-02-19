using System;
using System.Collections.Generic;
using HealthCareClinicSystem.Models;

namespace HealthCareClinicSystem.Interfaces
{
    public interface IBillingService
    {
        int GenerateBill(int visitId, decimal additionalCharges);
        bool RecordPayment(int billId, decimal amount, string paymentMode);
        List<Bill> GetOutstandingBills();
        void DisplayRevenueReport(DateTime startDate, DateTime endDate, string groupBy);
        Bill GetBillByVisitId(int visitId);
    }
}