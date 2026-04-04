using Microsoft.Data.SqlClient;
using HealthClinicApp.Utilities;

namespace HealthClinicApp.Services
{
    public class BillingService
    {
        // UC-5.1 Generate Bill
        public void GenerateBill(int visitId)
        {
            using var con = DbConnectionFactory.GetConnection();
            con.Open();

            // Get consultation fee
            string feeQuery = @"SELECT d.ConsultationFee
                                FROM Visits v
                                JOIN Doctors d ON v.DoctorID = d.DoctorID
                                WHERE v.VisitID=@VisitID";

            SqlCommand feeCmd = new SqlCommand(feeQuery, con);
            feeCmd.Parameters.AddWithValue("@VisitID", visitId);
            decimal consultationFee = (decimal)feeCmd.ExecuteScalar();

            // SUM of bill items
            string sumQuery = "SELECT ISNULL(SUM(Amount),0) FROM BillItems WHERE BillID=@BillID";
            SqlCommand sumCmd = new SqlCommand(sumQuery, con);
            sumCmd.Parameters.AddWithValue("@BillID", visitId);
            decimal itemsTotal = (decimal)sumCmd.ExecuteScalar();

            decimal total = consultationFee + itemsTotal;

            string insertBill = "INSERT INTO Bills(VisitID, TotalAmount) VALUES(@VisitID,@Total)";
            SqlCommand billCmd = new SqlCommand(insertBill, con);
            billCmd.Parameters.AddWithValue("@VisitID", visitId);
            billCmd.Parameters.AddWithValue("@Total", total);
            billCmd.ExecuteNonQuery();

            Console.WriteLine("Bill Generated Successfully");
        }

        // UC-5.2 Record Payment (Transaction)
        public void RecordPayment(int billId, decimal amount, string mode)
        {
            using var con = DbConnectionFactory.GetConnection();
            con.Open();
            using var tx = con.BeginTransaction();

            try
            {
                SqlCommand updateBill = new SqlCommand(
                    "UPDATE Bills SET PaymentStatus='PAID', PaymentDate=GETDATE() WHERE BillID=@BillID",
                    con, tx);
                updateBill.Parameters.AddWithValue("@BillID", billId);
                updateBill.ExecuteNonQuery();

                SqlCommand insertTxn = new SqlCommand(
                    "INSERT INTO PaymentTransactions(BillID,AmountPaid,PaymentMode) VALUES(@BillID,@Amt,@Mode)",
                    con, tx);
                insertTxn.Parameters.AddWithValue("@BillID", billId);
                insertTxn.Parameters.AddWithValue("@Amt", amount);
                insertTxn.Parameters.AddWithValue("@Mode", mode);
                insertTxn.ExecuteNonQuery();

                tx.Commit();
                Console.WriteLine("Payment Recorded");
            }
            catch
            {
                tx.Rollback();
                Console.WriteLine("Payment Failed");
            }
        }

        // UC-5.3 Outstanding Bills
        public void ViewOutstandingBills()
        {
            using var con = DbConnectionFactory.GetConnection();
            con.Open();

            string query = @"SELECT p.FullName, COUNT(b.BillID) AS PendingBills, SUM(b.TotalAmount) AS TotalDue
                             FROM Bills b
                             JOIN Visits v ON b.VisitID = v.VisitID
                             JOIN Patients p ON v.PatientID = p.PatientID
                             WHERE b.PaymentStatus='UNPAID'
                             GROUP BY p.FullName";

            SqlCommand cmd = new SqlCommand(query, con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FullName"]} | Bills: {reader["PendingBills"]} | Due: {reader["TotalDue"]}");
            }
        }

        // UC-5.4 Revenue Report
        public void RevenueReport(DateTime from, DateTime to)
        {
            using var con = DbConnectionFactory.GetConnection();
            con.Open();

            string query = @"SELECT d.FullName, s.SpecialtyName, SUM(b.TotalAmount) AS Revenue
                             FROM Bills b
                             JOIN Visits v ON b.VisitID = v.VisitID
                             JOIN Doctors d ON v.DoctorID = d.DoctorID
                             JOIN Specialties s ON d.SpecialtyID = s.SpecialtyID
                             WHERE b.PaymentStatus='PAID'
                             AND b.PaymentDate BETWEEN @From AND @To
                             GROUP BY d.FullName, s.SpecialtyName
                             HAVING SUM(b.TotalAmount) > 0";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@From", from);
            cmd.Parameters.AddWithValue("@To", to);

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FullName"]} | {reader["SpecialtyName"]} | {reader["Revenue"]}");
            }
        }
    }
}
