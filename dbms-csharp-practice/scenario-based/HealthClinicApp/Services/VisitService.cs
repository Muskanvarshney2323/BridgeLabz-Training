using HealthClinicApp.Models;
using HealthClinicApp.Utilities;
using Microsoft.Data.SqlClient;

namespace HealthClinicApp.Services
{
    public class VisitService
    {
        // ✅ UC-4.1 Record Visit (TRANSACTION)
        public int RecordVisit(Visit visit)
        {
            using var conn = DbConnectionFactory.GetConnection();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                string insertVisit = @"INSERT INTO Visits(AppointmentID,PatientID,DoctorID,VisitDate,Diagnosis,Notes)
                                       VALUES(@AppId,@Pid,@Did,@Date,@Diag,@Notes);
                                       SELECT SCOPE_IDENTITY();";

                using var cmd = new SqlCommand(insertVisit, conn, tx);
                cmd.Parameters.AddWithValue("@AppId", visit.AppointmentId);
                cmd.Parameters.AddWithValue("@Pid", visit.PatientId);
                cmd.Parameters.AddWithValue("@Did", visit.DoctorId);
                cmd.Parameters.AddWithValue("@Date", visit.VisitDate);
                cmd.Parameters.AddWithValue("@Diag", visit.Diagnosis);
                cmd.Parameters.AddWithValue("@Notes", visit.Notes);

                int visitId = Convert.ToInt32(cmd.ExecuteScalar());

                string updateAppointment = "UPDATE Appointments SET Status='COMPLETED' WHERE AppointmentID=@Id";
                using var cmd2 = new SqlCommand(updateAppointment, conn, tx);
                cmd2.Parameters.AddWithValue("@Id", visit.AppointmentId);
                cmd2.ExecuteNonQuery();

                tx.Commit();
                Console.WriteLine("Visit recorded successfully.");
                return visitId;
            }
            catch
            {
                tx.Rollback();
                Console.WriteLine("Visit failed.");
                return 0;
            }
        }

        // ✅ UC-4.2 View Medical History
        public void ViewMedicalHistory(int patientId)
        {
            using var conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string query = @"SELECT v.VisitDate, v.Diagnosis, p.MedicineName, p.Dosage, p.Duration
                             FROM Visits v
                             LEFT JOIN Prescriptions p ON v.VisitId = p.VisitId
                             JOIN Appointments a ON v.AppointmentId = a.AppointmentId
                             WHERE v.PatientId=@Pid
                             ORDER BY v.VisitDate DESC";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Pid", patientId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["VisitDate"]} | {reader["Diagnosis"]} | {reader["MedicineName"]} {reader["Dosage"]} {reader["Duration"]}");
            }
        }

        // ✅ UC-4.3 Batch Prescription Insert
        public void AddPrescriptions(int visitId, List<Prescription> list)
        {
            using var conn = DbConnectionFactory.GetConnection();
            conn.Open();

            foreach (var p in list)
            {
                string query = @"INSERT INTO Prescriptions(VisitId,MedicineName,Dosage,Duration)
                                 VALUES(@Vid,@Med,@Dose,@Dur)";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Vid", visitId);
                cmd.Parameters.AddWithValue("@Med", p.MedicineName);
                cmd.Parameters.AddWithValue("@Dose", p.Dosage);
                cmd.Parameters.AddWithValue("@Dur", p.Duration);
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Prescriptions added.");
        }
    }
}
