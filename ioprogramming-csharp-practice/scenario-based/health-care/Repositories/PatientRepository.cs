using System;
using System.Collections.Generic;
using HealthCareClinicSystem.Interfaces;
using HealthCareClinicSystem.Models;
using HealthCareClinicSystem.Utilities;
using Microsoft.Data.SqlClient;

namespace HealthCareClinicSystem.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public int RegisterPatient(Patient patient)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"INSERT INTO Patients (full_name, dob, gender, phone, email, address, blood_group) 
                               VALUES (@FullName, @DOB, @Gender, @Phone, @Email, @Address, @BloodGroup);
                               SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", patient.FullName);
                cmd.Parameters.AddWithValue("@DOB", patient.DOB);
                cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                cmd.Parameters.AddWithValue("@Phone", patient.Phone);
                cmd.Parameters.AddWithValue("@Email", patient.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", patient.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue(
                    "@BloodGroup",
                    patient.BloodGroup ?? (object)DBNull.Value
                );

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool UpdatePatient(Patient patient)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"UPDATE Patients 
                               SET full_name = @FullName, dob = @DOB, gender = @Gender, 
                                   phone = @Phone, email = @Email, address = @Address, blood_group = @BloodGroup
                               WHERE patient_id = @PatientId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientId", patient.PatientId);
                cmd.Parameters.AddWithValue("@FullName", patient.FullName);
                cmd.Parameters.AddWithValue("@DOB", patient.DOB);
                cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                cmd.Parameters.AddWithValue("@Phone", patient.Phone);
                cmd.Parameters.AddWithValue("@Email", patient.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", patient.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue(
                    "@BloodGroup",
                    patient.BloodGroup ?? (object)DBNull.Value
                );

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public Patient GetPatientById(int patientId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Patients WHERE patient_id = @PatientId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientId", patientId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return MapPatient(reader);
                }
                return null;
            }
        }

        public Patient GetPatientByPhone(string phone)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Patients WHERE phone = @Phone";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Phone", phone);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return MapPatient(reader);
                }
                return null;
            }
        }

        public Patient GetPatientByEmail(string email)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Patients WHERE email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return MapPatient(reader);
                }
                return null;
            }
        }

        public List<Patient> SearchPatients(string searchTerm)
        {
            List<Patient> patients = new List<Patient>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT * FROM Patients 
                               WHERE full_name LIKE @SearchTerm 
                               OR phone LIKE @SearchTerm 
                               OR CAST(patient_id AS VARCHAR) LIKE @SearchTerm";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    patients.Add(MapPatient(reader));
                }
            }
            return patients;
        }

        public List<Visit> GetPatientVisitHistory(int patientId)
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
                    visits.Add(
                        new Visit
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
                        }
                    );
                }
            }
            return visits;
        }

        public bool PatientExists(string phone, string email)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    "SELECT COUNT(*) FROM Patients WHERE phone = @Phone OR email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private Patient MapPatient(SqlDataReader reader)
        {
            return new Patient
            {
                PatientId = Convert.ToInt32(reader["patient_id"]),
                FullName = reader["full_name"].ToString(),
                DOB = Convert.ToDateTime(reader["dob"]),
                Gender = reader["gender"]?.ToString(),
                Phone = reader["phone"].ToString(),
                Email = reader["email"]?.ToString(),
                Address = reader["address"]?.ToString(),
                BloodGroup = reader["blood_group"]?.ToString(),
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
            };
        }
    }
}