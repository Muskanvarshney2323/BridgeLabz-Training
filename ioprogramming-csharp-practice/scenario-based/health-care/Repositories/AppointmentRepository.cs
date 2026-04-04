using System;
using System.Collections.Generic;
using HealthCareClinicSystem.Interfaces;
using HealthCareClinicSystem.Models;
using HealthCareClinicSystem.Utilities;
using Microsoft.Data.SqlClient;

namespace HealthCareClinicSystem.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public int BookAppointment(Appointment appointment)
        {
            if (
                !CheckDoctorAvailability(
                    appointment.DoctorId,
                    appointment.AppointmentDate,
                    appointment.AppointmentTime
                )
            )
                return 0;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"INSERT INTO Appointments (patient_id, doctor_id, appointment_date, appointment_time, status) 
                               VALUES (@PatientId, @DoctorId, @AppointmentDate, @AppointmentTime, 'SCHEDULED');
                               SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                cmd.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                cmd.Parameters.AddWithValue("@AppointmentTime", appointment.AppointmentTime);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool CheckDoctorAvailability(int doctorId, DateTime date, TimeSpan time)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT COUNT(*) FROM Appointments 
                               WHERE doctor_id = @DoctorId 
                               AND appointment_date = @AppointmentDate
                               AND appointment_time = @AppointmentTime
                               AND status IN ('SCHEDULED', 'RESCHEDULED')";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorId", doctorId);
                cmd.Parameters.AddWithValue("@AppointmentDate", date);
                cmd.Parameters.AddWithValue("@AppointmentTime", time);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count == 0;
            }
        }

        public Dictionary<TimeSpan, int> GetDoctorSlotAvailability(
            int doctorId,
            DateTime date,
            int maxSlotsPerDay
        )
        {
            Dictionary<TimeSpan, int> slotAvailability = new Dictionary<TimeSpan, int>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT appointment_time, COUNT(*) as BookedCount 
                               FROM Appointments 
                               WHERE doctor_id = @DoctorId 
                               AND appointment_date = @AppointmentDate
                               AND status IN ('SCHEDULED', 'RESCHEDULED')
                               GROUP BY appointment_time";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorId", doctorId);
                cmd.Parameters.AddWithValue("@AppointmentDate", date);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TimeSpan time = (TimeSpan)reader["appointment_time"];
                    int bookedCount = Convert.ToInt32(reader["BookedCount"]);
                    slotAvailability[time] = bookedCount;
                }
            }

            return slotAvailability;
        }

        public bool CancelAppointment(int appointmentId, string remarks)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string query =
                        "UPDATE Appointments SET status = 'CANCELLED' WHERE appointment_id = @AppointmentId";
                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    cmd.ExecuteNonQuery();

                    string auditQuery =
                        @"INSERT INTO Appointment_Audit (appointment_id, action, remarks) 
                                        VALUES (@AppointmentId, 'CANCELLED', @Remarks)";
                    SqlCommand auditCmd = new SqlCommand(auditQuery, conn, transaction);
                    auditCmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    auditCmd.Parameters.AddWithValue("@Remarks", remarks);
                    auditCmd.ExecuteNonQuery();

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

        public bool RescheduleAppointment(
            int appointmentId,
            DateTime newDate,
            TimeSpan newTime,
            int? newDoctorId
        )
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string getQuery =
                        "SELECT doctor_id FROM Appointments WHERE appointment_id = @AppointmentId";
                    SqlCommand getCmd = new SqlCommand(getQuery, conn, transaction);
                    getCmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    int currentDoctorId = Convert.ToInt32(getCmd.ExecuteScalar());

                    int doctorIdToUse = newDoctorId ?? currentDoctorId;

                    string checkQuery =
                        @"SELECT COUNT(*) FROM Appointments 
                                        WHERE doctor_id = @DoctorId 
                                        AND appointment_date = @NewDate
                                        AND appointment_time = @NewTime
                                        AND status IN ('SCHEDULED', 'RESCHEDULED')
                                        AND appointment_id != @AppointmentId";

                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction);
                    checkCmd.Parameters.AddWithValue("@DoctorId", doctorIdToUse);
                    checkCmd.Parameters.AddWithValue("@NewDate", newDate);
                    checkCmd.Parameters.AddWithValue("@NewTime", newTime);
                    checkCmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    string updateQuery =
                        @"UPDATE Appointments 
                                         SET appointment_date = @NewDate, 
                                             appointment_time = @NewTime,
                                             doctor_id = @DoctorId,
                                             status = 'RESCHEDULED'
                                         WHERE appointment_id = @AppointmentId";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction);
                    updateCmd.Parameters.AddWithValue("@NewDate", newDate);
                    updateCmd.Parameters.AddWithValue("@NewTime", newTime);
                    updateCmd.Parameters.AddWithValue("@DoctorId", doctorIdToUse);
                    updateCmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    updateCmd.ExecuteNonQuery();

                    string auditQuery =
                        @"INSERT INTO Appointment_Audit (appointment_id, action, remarks) 
                                        VALUES (@AppointmentId, 'RESCHEDULED', 'Rescheduled to ' + CAST(@NewDate AS VARCHAR) + ' ' + CAST(@NewTime AS VARCHAR))";
                    SqlCommand auditCmd = new SqlCommand(auditQuery, conn, transaction);
                    auditCmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    auditCmd.Parameters.AddWithValue("@NewDate", newDate);
                    auditCmd.Parameters.AddWithValue("@NewTime", newTime);
                    auditCmd.ExecuteNonQuery();

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

        public List<Appointment> GetDailyAppointmentSchedule(DateTime date)
        {
            List<Appointment> appointments = new List<Appointment>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT a.*, p.full_name as PatientName, d.full_name as DoctorName
                               FROM Appointments a
                               INNER JOIN Patients p ON a.patient_id = p.patient_id
                               INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
                               WHERE a.appointment_date = @AppointmentDate
                               AND a.status IN ('SCHEDULED', 'RESCHEDULED')
                               ORDER BY a.appointment_time";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AppointmentDate", date);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    appointments.Add(
                        new Appointment
                        {
                            AppointmentId = Convert.ToInt32(reader["appointment_id"]),
                            PatientId = Convert.ToInt32(reader["patient_id"]),
                            PatientName = reader["PatientName"].ToString(),
                            DoctorId = Convert.ToInt32(reader["doctor_id"]),
                            DoctorName = reader["DoctorName"].ToString(),
                            AppointmentDate = Convert.ToDateTime(reader["appointment_date"]),
                            AppointmentTime = (TimeSpan)reader["appointment_time"],
                            Status = reader["status"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"]),
                        }
                    );
                }
            }
            return appointments;
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"SELECT a.*, p.full_name as PatientName, d.full_name as DoctorName
                               FROM Appointments a
                               INNER JOIN Patients p ON a.patient_id = p.patient_id
                               INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
                               WHERE a.appointment_id = @AppointmentId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Appointment
                    {
                        AppointmentId = Convert.ToInt32(reader["appointment_id"]),
                        PatientId = Convert.ToInt32(reader["patient_id"]),
                        PatientName = reader["PatientName"].ToString(),
                        DoctorId = Convert.ToInt32(reader["doctor_id"]),
                        DoctorName = reader["DoctorName"].ToString(),
                        AppointmentDate = Convert.ToDateTime(reader["appointment_date"]),
                        AppointmentTime = (TimeSpan)reader["appointment_time"],
                        Status = reader["status"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["created_at"]),
                    };
                }
                return null;
            }
        }
    }
}