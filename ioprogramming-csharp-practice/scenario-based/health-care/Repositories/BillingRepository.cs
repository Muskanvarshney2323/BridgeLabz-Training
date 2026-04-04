using System;
using System.Collections.Generic;
using HealthCareClinicSystem.Interfaces;
using HealthCareClinicSystem.Models;
using HealthCareClinicSystem.Utilities;
using Microsoft.Data.SqlClient;

namespace HealthCareClinicSystem.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        public int GenerateBill(int visitId, decimal consultationFee, decimal additionalCharges)
        {
            decimal totalAmount = consultationFee + additionalCharges;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"INSERT INTO Bills (visit_id, total_amount, payment_status) 
                               VALUES (@VisitId, @TotalAmount, 'UNPAID');
                               SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VisitId", visitId);
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool RecordPayment(int billId, decimal amount, string paymentMode)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string updateQuery =
                        @"UPDATE Bills 
                                         SET payment_status = 'PAID', 
                                             payment_date = GETDATE(), 
                                             payment_mode = @PaymentMode
                                         WHERE bill_id = @BillId";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction);
                    updateCmd.Parameters.AddWithValue("@BillId", billId);
                    updateCmd.Parameters.AddWithValue("@PaymentMode", paymentMode);
                    updateCmd.ExecuteNonQuery();

                    string transactionQuery =
                        @"INSERT INTO Payment_Transactions (bill_id, amount, payment_mode) 
                                              VALUES (@BillId, @Amount, @PaymentMode)";

                    SqlCommand transactionCmd = new SqlCommand(transactionQuery, conn, transaction);
                    transactionCmd.Parameters.AddWithValue("@BillId", billId);
                    transactionCmd.Parameters.AddWithValue("@Amount", amount);
                    transactionCmd.Parameters.AddWithValue("@PaymentMode", paymentMode);
                    transactionCmd.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<Bill> GetOutstandingBills()
        {
            List<Bill> bills = new List<Bill>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT b.*, p.full_name as PatientName
                               FROM Bills b
                               INNER JOIN Visits v ON b.visit_id = v.visit_id
                               INNER JOIN Patients p ON v.patient_id = p.patient_id
                               WHERE b.payment_status = 'UNPAID'
                               ORDER BY b.created_at";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    bills.Add(
                        new Bill
                        {
                            BillId = Convert.ToInt32(reader["bill_id"]),
                            VisitId = Convert.ToInt32(reader["visit_id"]),
                            PatientName = reader["PatientName"].ToString(),
                            TotalAmount = Convert.ToDecimal(reader["total_amount"]),
                            PaymentStatus = reader["payment_status"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"]),
                            PaymentDate =
                                reader["payment_date"] != DBNull.Value
                                    ? (DateTime?)Convert.ToDateTime(reader["payment_date"])
                                    : null,
                            PaymentMode = reader["payment_mode"]?.ToString(),
                        }
                    );
                }
            }
            return bills;
        }

        public Dictionary<string, decimal> GetRevenueReport(
            DateTime startDate,
            DateTime endDate,
            string groupBy
        )
        {
            Dictionary<string, decimal> revenue = new Dictionary<string, decimal>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "";

                if (groupBy.ToLower() == "date")
                {
                    query =
                        @"SELECT CAST(payment_date AS DATE) as GroupDate, SUM(total_amount) as Total
                            FROM Bills 
                            WHERE payment_status = 'PAID'
                            AND payment_date BETWEEN @StartDate AND @EndDate
                            GROUP BY CAST(payment_date AS DATE)
                            ORDER BY GroupDate";
                }
                else if (groupBy.ToLower() == "doctor")
                {
                    query =
                        @"SELECT d.full_name as DoctorName, SUM(b.total_amount) as Total
                            FROM Bills b
                            INNER JOIN Visits v ON b.visit_id = v.visit_id
                            INNER JOIN Doctors d ON v.doctor_id = d.doctor_id
                            WHERE b.payment_status = 'PAID'
                            AND b.payment_date BETWEEN @StartDate AND @EndDate
                            GROUP BY d.full_name
                            ORDER BY Total DESC";
                }
                else if (groupBy.ToLower() == "specialty")
                {
                    query =
                        @"SELECT s.specialty_name as SpecialtyName, SUM(b.total_amount) as Total
                            FROM Bills b
                            INNER JOIN Visits v ON b.visit_id = v.visit_id
                            INNER JOIN Doctors d ON v.doctor_id = d.doctor_id
                            INNER JOIN Specialties s ON d.specialty_id = s.specialty_id
                            WHERE b.payment_status = 'PAID'
                            AND b.payment_date BETWEEN @StartDate AND @EndDate
                            GROUP BY s.specialty_name
                            ORDER BY Total DESC";
                }

                if (string.IsNullOrEmpty(query))
                    return revenue;

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    revenue[reader[0].ToString()] = Convert.ToDecimal(reader["Total"]);
                }
            }

            return revenue;
        }

        public Bill GetBillByVisitId(int visitId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT b.*, p.full_name as PatientName
                               FROM Bills b
                               INNER JOIN Visits v ON b.visit_id = v.visit_id
                               INNER JOIN Patients p ON v.patient_id = p.patient_id
                               WHERE b.visit_id = @VisitId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VisitId", visitId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Bill
                    {
                        BillId = Convert.ToInt32(reader["bill_id"]),
                        VisitId = Convert.ToInt32(reader["visit_id"]),
                        PatientName = reader["PatientName"].ToString(),
                        TotalAmount = Convert.ToDecimal(reader["total_amount"]),
                        PaymentStatus = reader["payment_status"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["created_at"]),
                        PaymentDate =
                            reader["payment_date"] != DBNull.Value
                                ? (DateTime?)Convert.ToDateTime(reader["payment_date"])
                                : null,
                        PaymentMode = reader["payment_mode"]?.ToString(),
                    };
                }
                return null;
            }
        }
    }
}