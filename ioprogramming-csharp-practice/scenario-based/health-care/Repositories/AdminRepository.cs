using System;
using System.Collections.Generic;
using System.IO;
using HealthCareClinicSystem.Interfaces;
using HealthCareClinicSystem.Models;
using HealthCareClinicSystem.Utilities;
using Microsoft.Data.SqlClient;

namespace HealthCareClinicSystem.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        public int AddSpecialty(Specialty specialty)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"INSERT INTO Specialties (specialty_name, description) 
                               VALUES (@SpecialtyName, @Description);
                               SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SpecialtyName", specialty.SpecialtyName);
                cmd.Parameters.AddWithValue(
                    "@Description",
                    specialty.Description ?? (object)DBNull.Value
                );

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool UpdateSpecialty(Specialty specialty)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query =
                    @"UPDATE Specialties 
                               SET specialty_name = @SpecialtyName, description = @Description
                               WHERE specialty_id = @SpecialtyId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SpecialtyId", specialty.SpecialtyId);
                cmd.Parameters.AddWithValue("@SpecialtyName", specialty.SpecialtyName);
                cmd.Parameters.AddWithValue(
                    "@Description",
                    specialty.Description ?? (object)DBNull.Value
                );

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteSpecialty(int specialtyId)
        {
            if (!CanDeleteSpecialty(specialtyId))
                return false;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM Specialties WHERE specialty_id = @SpecialtyId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SpecialtyId", specialtyId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
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

        public bool CanDeleteSpecialty(int specialtyId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM Doctors WHERE specialty_id = @SpecialtyId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SpecialtyId", specialtyId);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count == 0;
            }
        }

        public List<AuditLog> GetAuditLogs(string tableName = null, string actionType = null)
        {
            List<AuditLog> logs = new List<AuditLog>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Audit_Log WHERE 1=1";

                if (!string.IsNullOrEmpty(tableName))
                    query += " AND table_name = @TableName";
                if (!string.IsNullOrEmpty(actionType))
                    query += " AND action_type = @ActionType";

                query += " ORDER BY action_date DESC";

                SqlCommand cmd = new SqlCommand(query, conn);

                if (!string.IsNullOrEmpty(tableName))
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                if (!string.IsNullOrEmpty(actionType))
                    cmd.Parameters.AddWithValue("@ActionType", actionType);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    logs.Add(
                        new AuditLog
                        {
                            AuditId = Convert.ToInt32(reader["audit_id"]),
                            TableName = reader["table_name"].ToString(),
                            ActionType = reader["action_type"].ToString(),
                            RecordId = Convert.ToInt32(reader["record_id"]),
                            ActionDate = Convert.ToDateTime(reader["action_date"]),
                            PerformedBy = reader["performed_by"]?.ToString(),
                        }
                    );
                }
            }
            return logs;
        }

        public void PerformDatabaseBackup(string backupPath)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string databaseName = conn.Database;
                string backupFile = Path.Combine(
                    backupPath,
                    $"{databaseName}_{DateTime.Now:yyyyMMdd_HHmmss}.bak"
                );

                string query =
                    $@"BACKUP DATABASE [{databaseName}] 
                                TO DISK = '{backupFile}' 
                                WITH FORMAT, MEDIANAME = 'SQLServerBackups', NAME = 'Full Backup';";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}