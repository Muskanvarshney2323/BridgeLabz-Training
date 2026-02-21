using HealthClinicApp.Interfaces;
using HealthClinicApp.Models;
using HealthClinicApp.Utilities;
using HealthClinicApp.Exceptions;
using Microsoft.Data.SqlClient;

namespace HealthClinicApp.Services
{
    public class PatientService : IPatientService
    {

        // UC-1.1 Register Patient
        public void RegisterPatient(Patient patient)
        {
            PatientValidator.Validate(patient);

            using var conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string checkQuery = "SELECT COUNT(*) FROM Patients WHERE Phone=@Phone OR Email=@Email";
            using var checkCmd = new SqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@Phone", patient.Phone);
            checkCmd.Parameters.AddWithValue("@Email", patient.Email);

            if ((int)checkCmd.ExecuteScalar() > 0)
                throw new ClinicException("Patient already exists.");

            string insertQuery = @"INSERT INTO Patients(FullName,DOB,Phone,Email,Address,BloodGroup)
                                   VALUES(@Name,@DOB,@Phone,@Email,@Address,@Blood);
                                   SELECT SCOPE_IDENTITY();";

            using var cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@Name", patient.FullName);
            cmd.Parameters.AddWithValue("@DOB", patient.DOB);
            cmd.Parameters.AddWithValue("@Phone", patient.Phone);
            cmd.Parameters.AddWithValue("@Email", patient.Email);
            cmd.Parameters.AddWithValue("@Address", patient.Address);
            cmd.Parameters.AddWithValue("@Blood", patient.BloodGroup);

            int newId = Convert.ToInt32(cmd.ExecuteScalar());
            Console.WriteLine($"Patient registered. ID = {newId}");
        }

        // UC-1.2 Search before Update
        public Patient GetPatientById(int id)
        {
            using var conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string query = "SELECT * FROM Patients WHERE PatientID=@Id";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Patient
                {
                    PatientID = (int)reader["PatientID"],
                    FullName = reader["FullName"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    BloodGroup = reader["BloodGroup"].ToString(),
                    DOB = (DateTime)reader["DOB"]
                };
            }
            throw new ClinicException("Patient not found.");
        }

        public void UpdatePatient(Patient patient)
        {
            using var conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string query = @"UPDATE Patients
                             SET FullName=@Name, Address=@Address, BloodGroup=@Blood
                             WHERE PatientID=@Id";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", patient.FullName);
            cmd.Parameters.AddWithValue("@Address", patient.Address);
            cmd.Parameters.AddWithValue("@Blood", patient.BloodGroup);
            cmd.Parameters.AddWithValue("@Id", patient.PatientID);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Patient updated.");
        }

        // UC-1.3 Search
        public List<Patient> SearchPatients(string keyword)
        {
            var list = new List<Patient>();
            using var conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string query = @"SELECT * FROM Patients
                             WHERE FullName LIKE @NameKey
                             OR Phone = @ExactKey
                             OR PatientID = TRY_CAST(@ExactKey AS INT)";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NameKey", "%" + keyword + "%");
            cmd.Parameters.AddWithValue("@ExactKey", keyword);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Patient
                {
                    PatientID = (int)reader["PatientID"],
                    FullName = reader["FullName"].ToString(),
                    Phone = reader["Phone"].ToString()
                });
            }
            return list;
        }

        // UC-1.4 Visit History
        public void ViewVisitHistory(int patientId)
        {
            using var conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string query = @"SELECT a.AppointmentDate, v.VisitDate, d.FullName AS Doctor, v.Diagnosis
                             FROM Visits v
                             JOIN Appointments a ON v.AppointmentID = a.AppointmentID
                             JOIN Doctors d ON v.DoctorID = d.DoctorID
                             WHERE v.PatientID=@Id
                             ORDER BY v.VisitDate DESC";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", patientId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["VisitDate"]} | Dr.{reader["Doctor"]} | {reader["Diagnosis"]}");
            }
        }
    }
}
