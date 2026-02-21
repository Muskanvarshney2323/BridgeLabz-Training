using System.Data;
using Microsoft.Data.SqlClient;
using HealthClinicApp.Models;
using HealthClinicApp.Utilities;
using HealthClinicApp.Interfaces;

namespace HealthClinicApp.Services
{
    public class DoctorService : IDoctorService

    {
        public void AddDoctor(Doctor doctor)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = @"INSERT INTO doctors (FullName, DoctorPhone, DoctorEmail, DoctorSpecialty, ConsultationFee, IsActive)
                 VALUES (@FullName, @Phone, @Email, @Specialty, @Fee, 1);
                 SELECT SCOPE_IDENTITY();";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", doctor.FullName);
            command.Parameters.AddWithValue("@Phone", doctor.Phone);
            command.Parameters.AddWithValue("@Email", doctor.Email);
            command.Parameters.AddWithValue("@Specialty", doctor.Specialty);
            command.Parameters.AddWithValue("@Fee", doctor.ConsultationFee);

            int newId = Convert.ToInt32(command.ExecuteScalar());
            Console.WriteLine($"Doctor added successfully. ID = {newId}");
        }

        public void UpdateDoctorSpecialty(int doctorId, string newSpecialty)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "UPDATE doctors SET Specialty=@Specialty WHERE Id=@Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Specialty", newSpecialty);
            command.Parameters.AddWithValue("@Id", doctorId);
            command.ExecuteNonQuery();
            Console.WriteLine("Specialty updated successfully.");
        }

        public List<Doctor> GetDoctorsBySpecialty(string specialtyName)
        {
            var doctors = new List<Doctor>();
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "SELECT * FROM doctors WHERE Specialty=@Specialty AND IsActive=1";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Specialty", specialtyName);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                doctors.Add(new Doctor
                {
                    Id = (int)reader["Id"],
                    FullName = reader["FullName"].ToString()!,
                    Phone = reader["Phone"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Specialty = reader["Specialty"].ToString()!,
                    ConsultationFee = (decimal)reader["ConsultationFee"],
                    IsActive = (bool)reader["IsActive"]
                });
            }
            return doctors;
        }

        public bool DeactivateDoctor(int doctorId)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            // Check for future appointments
            string checkQuery = @"SELECT COUNT(*) FROM appointments 
                                  WHERE DoctorId=@Id AND AppointmentDate >= GETDATE()";
            using var checkCmd = new SqlCommand(checkQuery, connection);
            checkCmd.Parameters.AddWithValue("@Id", doctorId);
            int futureCount = (int)checkCmd.ExecuteScalar();

            if (futureCount > 0)
            {
                Console.WriteLine("❌ Cannot deactivate. Doctor has future appointments.");
                return false;
            }

            string updateQuery = "UPDATE doctors SET IsActive=0 WHERE Id=@Id";
            using var command = new SqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@Id", doctorId);
            command.ExecuteNonQuery();
            return true;
        }
    }
}
