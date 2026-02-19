using System;
using System.Collections.Generic;
using HealthCareClinicSystem.Interfaces;
using HealthCareClinicSystem.Models;
using HealthCareClinicSystem.Utilities;
using Microsoft.Data.SqlClient;

namespace HealthCareClinicSystem.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        public int AddDoctor(Doctor doctor)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"INSERT INTO Doctors (full_name, specialty_id, phone, email, consultation_fee) 
                               VALUES (@FullName, @SpecialtyId, @Phone, @Email, @ConsultationFee);
                               SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", doctor.FullName);
                cmd.Parameters.AddWithValue("@SpecialtyId", doctor.SpecialtyId);
                cmd.Parameters.AddWithValue("@Phone", doctor.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", doctor.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ConsultationFee", doctor.ConsultationFee);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool UpdateDoctor(Doctor doctor)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"UPDATE Doctors 
                               SET full_name = @FullName, phone = @Phone, email = @Email, 
                                   consultation_fee = @ConsultationFee
                               WHERE doctor_id = @DoctorId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorId", doctor.DoctorId);
                cmd.Parameters.AddWithValue("@FullName", doctor.FullName);
                cmd.Parameters.AddWithValue("@Phone", doctor.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", doctor.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ConsultationFee", doctor.ConsultationFee);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateDoctorSpecialty(int doctorId, int specialtyId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string query =
                        "UPDATE Doctors SET specialty_id = @SpecialtyId WHERE doctor_id = @DoctorId";
                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@DoctorId", doctorId);
                    cmd.Parameters.AddWithValue("@SpecialtyId", specialtyId);

                    int result = cmd.ExecuteNonQuery();
                    transaction.Commit();
                    return result > 0;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool DeactivateDoctor(int doctorId)
        {
            if (HasFutureAppointments(doctorId))
            {
                return false;
            }

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "UPDATE Doctors SET is_active = 0 WHERE doctor_id = @DoctorId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorId", doctorId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public Doctor GetDoctorById(int doctorId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT d.*, s.specialty_name 
                               FROM Doctors d
                               INNER JOIN Specialties s ON d.specialty_id = s.specialty_id
                               WHERE d.doctor_id = @DoctorId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorId", doctorId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return MapDoctor(reader);
                }
                return null;
            }
        }

        public List<Doctor> GetDoctorsBySpecialty(string specialtyName)
        {
            List<Doctor> doctors = new List<Doctor>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT d.*, s.specialty_name 
                               FROM Doctors d
                               INNER JOIN Specialties s ON d.specialty_id = s.specialty_id
                               WHERE s.specialty_name LIKE @SpecialtyName AND d.is_active = 1
                               ORDER BY d.full_name";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SpecialtyName", $"%{specialtyName}%");

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    doctors.Add(MapDoctor(reader));
                }
            }
            return doctors;
        }

        public List<Doctor> GetAllActiveDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT d.*, s.specialty_name 
                               FROM Doctors d
                               INNER JOIN Specialties s ON d.specialty_id = s.specialty_id
                               WHERE d.is_active = 1
                               ORDER BY d.full_name";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    doctors.Add(MapDoctor(reader));
                }
            }
            return doctors;
        }

        public List<Specialty> GetAllSpecialties()
        {
            List<Specialty> specialties = new List<Specialty>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Specialties ORDER BY specialty_name";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    specialties.Add(
                        new Specialty
                        {
                            SpecialtyId = Convert.ToInt32(reader["specialty_id"]),
                            SpecialtyName = reader["specialty_name"].ToString(),
                            Description = reader["description"]?.ToString(),
                        }
                    );
                }
            }
            return specialties;
        }

        public bool HasFutureAppointments(int doctorId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT COUNT(*) FROM Appointments 
                               WHERE doctor_id = @DoctorId 
                               AND appointment_date >= CAST(GETDATE() AS DATE)
                               AND status IN ('SCHEDULED', 'RESCHEDULED')";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorId", doctorId);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private Doctor MapDoctor(SqlDataReader reader)
        {
            return new Doctor
            {
                DoctorId = Convert.ToInt32(reader["doctor_id"]),
                FullName = reader["full_name"].ToString(),
                SpecialtyId = Convert.ToInt32(reader["specialty_id"]),
                SpecialtyName = reader["specialty_name"].ToString(),
                Phone = reader["phone"]?.ToString(),
                Email = reader["email"]?.ToString(),
                ConsultationFee = Convert.ToDecimal(reader["consultation_fee"]),
                IsActive = Convert.ToBoolean(reader["is_active"]),
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
            };
        }
    }
}