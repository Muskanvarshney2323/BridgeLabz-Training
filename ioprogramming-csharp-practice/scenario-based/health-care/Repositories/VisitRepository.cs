using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HealthCareClinicSystem.Interfaces;
using HealthCareClinicSystem.Models;
using HealthCareClinicSystem.Utilities;
using Microsoft.Data.SqlClient;

namespace HealthCareClinicSystem.Repositories
{
    public class VisitRepository : IVisitRepository
    {
        public int RecordVisit(Visit visit, List<Prescription> prescriptions)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Update appointment status
                    string updateAppointment =
                        @"UPDATE Appointments SET status = 'COMPLETED' 
                                               WHERE appointment_id = @AppointmentId";
                    SqlCommand updateCmd = new SqlCommand(updateAppointment, conn, transaction);
                    updateCmd.Parameters.AddWithValue("@AppointmentId", visit.AppointmentId);
                    updateCmd.ExecuteNonQuery();

                    // Insert visit
                    string visitQuery =
                        @"INSERT INTO Visits (appointment_id, patient_id, doctor_id, diagnosis, notes) 
                                        VALUES (@AppointmentId, @PatientId, @DoctorId, @Diagnosis, @Notes);
                                        SELECT SCOPE_IDENTITY();";

                    SqlCommand visitCmd = new SqlCommand(visitQuery, conn, transaction);
                    visitCmd.Parameters.AddWithValue("@AppointmentId", visit.AppointmentId);
                    visitCmd.Parameters.AddWithValue("@PatientId", visit.PatientId);
                    visitCmd.Parameters.AddWithValue("@DoctorId", visit.DoctorId);
                    visitCmd.Parameters.AddWithValue(
                        "@Diagnosis",
                        visit.Diagnosis ?? (object)DBNull.Value
                    );
                    visitCmd.Parameters.AddWithValue("@Notes", visit.Notes ?? (object)DBNull.Value);

                    int visitId = Convert.ToInt32(visitCmd.ExecuteScalar());

                    // Insert prescriptions
                    if (prescriptions != null && prescriptions.Count > 0)
                    {
                        foreach (var prescription in prescriptions)
                        {
                            string prescriptionQuery =
                                @"INSERT INTO Prescriptions (visit_id, medicine_name, dosage, duration) 
                                                       VALUES (@VisitId, @MedicineName, @Dosage, @Duration)";

                            SqlCommand prescriptionCmd = new SqlCommand(
                                prescriptionQuery,
                                conn,
                                transaction
                            );
                            prescriptionCmd.Parameters.AddWithValue("@VisitId", visitId);
                            prescriptionCmd.Parameters.AddWithValue(
                                "@MedicineName",
                                prescription.MedicineName
                            );
                            prescriptionCmd.Parameters.AddWithValue(
                                "@Dosage",
                                prescription.Dosage ?? (object)DBNull.Value
                            );
                            prescriptionCmd.Parameters.AddWithValue(
                                "@Duration",
                                prescription.Duration ?? (object)DBNull.Value
                            );
                            prescriptionCmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return visitId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<Visit> GetPatientMedicalHistory(int patientId)
        {
            List<Visit> visits = new List<Visit>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT v.*, d.full_name as DoctorName, p.full_name as PatientName,
                                       a.appointment_date, a.appointment_time
                               FROM Visits v
                               INNER JOIN Doctors d ON v.doctor_id = d.doctor_id
                               INNER JOIN Patients p ON v.patient_id = p.patient_id
                               INNER JOIN Appointments a ON v.appointment_id = a.appointment_id
                               WHERE v.patient_id = @PatientId
                               ORDER BY v.visit_date DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientId", patientId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    visits.Add(MapVisit(reader));
                }
            }
            return visits;
        }

        public Visit GetVisitById(int visitId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT v.*, d.full_name as DoctorName, p.full_name as PatientName,
                                       a.appointment_date, a.appointment_time
                               FROM Visits v
                               INNER JOIN Doctors d ON v.doctor_id = d.doctor_id
                               INNER JOIN Patients p ON v.patient_id = p.patient_id
                               INNER JOIN Appointments a ON v.appointment_id = a.appointment_id
                               WHERE v.visit_id = @VisitId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VisitId", visitId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return MapVisit(reader);
                }
                return null;
            }
        }

        public List<Prescription> GetPrescriptionsByVisit(int visitId)
        {
            List<Prescription> prescriptions = new List<Prescription>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Prescriptions WHERE visit_id = @VisitId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VisitId", visitId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    prescriptions.Add(
                        new Prescription
                        {
                            PrescriptionId = Convert.ToInt32(reader["prescription_id"]),
                            VisitId = Convert.ToInt32(reader["visit_id"]),
                            MedicineName = reader["medicine_name"].ToString(),
                            Dosage = reader["dosage"]?.ToString(),
                            Duration = reader["duration"]?.ToString(),
                        }
                    );
                }
            }
            return prescriptions;
        }

        private Visit MapVisit(SqlDataReader reader)
        {
            return new Visit
            {
                VisitId = Convert.ToInt32(reader["visit_id"]),
                AppointmentId = Convert.ToInt32(reader["appointment_id"]),
                PatientId = Convert.ToInt32(reader["patient_id"]),
                PatientName = reader["PatientName"].ToString(),
                DoctorId = Convert.ToInt32(reader["doctor_id"]),
                DoctorName = reader["DoctorName"].ToString(),
                VisitDate = Convert.ToDateTime(reader["visit_date"]),
                Diagnosis = reader["diagnosis"]?.ToString(),
                Notes = reader["notes"]?.ToString(),
            };
        }
    }
}