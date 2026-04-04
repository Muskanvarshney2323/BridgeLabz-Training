using Microsoft.Data.SqlClient;
using HealthClinicApp.Interfaces;
using HealthClinicApp.Models;
using HealthClinicApp.Utilities;

namespace HealthClinicApp.Services
{
    public class AppointmentService : IAppointmentService
    {
        // UC-3.1 BOOK APPOINTMENT
        public void BookAppointment(Appointment appt)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string checkQuery = @"SELECT COUNT(*) FROM Appointments 
                                  WHERE DoctorId=@docId AND AppointmentDate=@date 
                                  AND Status='SCHEDULED'";

            using SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@docId", appt.DoctorId);
            checkCmd.Parameters.AddWithValue("@date", appt.AppointmentDate);

            int count = (int)checkCmd.ExecuteScalar();

            if (count >= 5)
            {
                Console.WriteLine("Doctor slot full!");
                return;
            }

            string insertQuery = @"INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Status)
                                   VALUES (@pid,@did,@date,'SCHEDULED')";

            using SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@pid", appt.PatientId);
            cmd.Parameters.AddWithValue("@did", appt.DoctorId);
            cmd.Parameters.AddWithValue("@date", appt.AppointmentDate);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Appointment booked!");
        }

        // UC-3.2 CHECK AVAILABILITY
        public void CheckDoctorAvailability(int doctorId, DateTime date)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM Appointments 
                             WHERE DoctorId=@doc AND CAST(AppointmentDate AS DATE)=@date 
                             AND Status='SCHEDULED'";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@doc", doctorId);
            cmd.Parameters.AddWithValue("@date", date.Date);

            int count = (int)cmd.ExecuteScalar();
            Console.WriteLine($"Bookings for doctor: {count}/5");
        }

        // UC-3.3 CANCEL
        public void CancelAppointment(int appointmentId)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();

            try
            {
                string update = "UPDATE Appointments SET Status='CANCELLED' WHERE AppointmentId=@id";
                SqlCommand cmd = new SqlCommand(update, conn, tran);
                cmd.Parameters.AddWithValue("@id", appointmentId);
                cmd.ExecuteNonQuery();

                string audit = "INSERT INTO Appointment_Audit (AppointmentId, ActionDate) VALUES (@id, GETDATE())";
                SqlCommand auditCmd = new SqlCommand(audit, conn, tran);
                auditCmd.Parameters.AddWithValue("@id", appointmentId);
                auditCmd.ExecuteNonQuery();

                tran.Commit();
                Console.WriteLine("Appointment cancelled.");
            }
            catch
            {
                tran.Rollback();
                Console.WriteLine("Cancellation failed.");
            }
        }

        // UC-3.4 RESCHEDULE
        public void RescheduleAppointment(int appointmentId, int doctorId, DateTime newDate)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();

            try
            {
                string check = @"SELECT COUNT(*) FROM Appointments 
                                 WHERE DoctorId=@doc AND AppointmentDate=@date 
                                 AND Status='SCHEDULED'";

                SqlCommand checkCmd = new SqlCommand(check, conn, tran);
                checkCmd.Parameters.AddWithValue("@doc", doctorId);
                checkCmd.Parameters.AddWithValue("@date", newDate);

                int count = (int)checkCmd.ExecuteScalar();
                if (count >= 5)
                {
                    Console.WriteLine("New slot full!");
                    tran.Rollback();
                    return;
                }

                string update = @"UPDATE Appointments 
                                  SET DoctorId=@doc, AppointmentDate=@date 
                                  WHERE AppointmentId=@id";

                SqlCommand cmd = new SqlCommand(update, conn, tran);
                cmd.Parameters.AddWithValue("@doc", doctorId);
                cmd.Parameters.AddWithValue("@date", newDate);
                cmd.Parameters.AddWithValue("@id", appointmentId);

                cmd.ExecuteNonQuery();
                tran.Commit();
                Console.WriteLine("Rescheduled!");
            }
            catch
            {
                tran.Rollback();
                Console.WriteLine("Reschedule failed.");
            }
        }

        // UC-3.5 DAILY SCHEDULE
        public void ViewDailySchedule(DateTime date)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string query = @"SELECT a.AppointmentId, p.Name AS Patient, d.Name AS Doctor, a.AppointmentDate
                             FROM Appointments a
                             JOIN Patients p ON a.PatientId=p.PatientId
                             JOIN Doctors d ON a.DoctorId=d.DoctorId
                             WHERE CAST(a.AppointmentDate AS DATE)=@date
                             ORDER BY a.AppointmentDate";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@date", date.Date);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["AppointmentId"]} | {reader["Patient"]} | {reader["Doctor"]} | {reader["AppointmentDate"]}");
            }
        }
    }
}
