using Microsoft.Data.SqlClient;
using HealthClinicApp.Utilities;
using System.Data;

namespace HealthClinicApp.Services
{
    public class AdminService
    {
        // UC-6.1 CREATE
        public void AddSpecialty(string name, string desc)
        {
            using var con = DbConnectionFactory.GetConnection();
            con.Open();

            string q = "INSERT INTO Specialties(SpecialtyName,Description) VALUES(@n,@d)";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@d", desc);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Specialty Added");
        }

        // READ
        public void ViewSpecialties()
        {
            using var con = DbConnectionFactory.GetConnection();
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Specialties", con);
            var r = cmd.ExecuteReader();
            while (r.Read())
                Console.WriteLine($"{r["SpecialtyID"]} | {r["SpecialtyName"]}");
        }

        // UPDATE
        public void UpdateSpecialty(int id, string name, string desc)
        {
            using var con = DbConnectionFactory.GetConnection();
            con.Open();

            string q = "UPDATE Specialties SET SpecialtyName=@n, Description=@d WHERE SpecialtyID=@id";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@d", desc);
            cmd.ExecuteNonQuery();
        }

        // DELETE with FK check
        public void DeleteSpecialty(int id)
        {
            using var con = DbConnectionFactory.GetConnection();
            con.Open();

            string check = "SELECT COUNT(*) FROM Doctors WHERE SpecialtyID=@id";
            SqlCommand chk = new SqlCommand(check, con);
            chk.Parameters.AddWithValue("@id", id);

            int count = (int)chk.ExecuteScalar();
            if (count > 0)
            {
                Console.WriteLine("Cannot delete — used by doctors");
                return;
            }

            SqlCommand del = new SqlCommand("DELETE FROM Specialties WHERE SpecialtyID=@id", con);
            del.Parameters.AddWithValue("@id", id);
            del.ExecuteNonQuery();
        }

        // UC-6.2 BACKUP SIMULATION
        public void BackupCriticalTables()
        {
            using var con = DbConnectionFactory.GetConnection();
            con.Open();

            Console.WriteLine("Tables in DB:");
            DataTable schema = con.GetSchema("Tables");

            foreach (DataRow row in schema.Rows)
                Console.WriteLine(row["TABLE_NAME"]);

            Console.WriteLine("Backup Validation Done");
        }

        // UC-6.3 VIEW AUDIT LOGS
        public void ViewAuditLogs(string user, string table, DateTime from, DateTime to)
        {
            using var con = DbConnectionFactory.GetConnection();
            con.Open();

            string q = @"SELECT * FROM AuditLog
                         WHERE UserName=@u AND TableName=@t
                         AND ActionTime BETWEEN @f AND @to";

            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@u", user);
            cmd.Parameters.AddWithValue("@t", table);
            cmd.Parameters.AddWithValue("@f", from);
            cmd.Parameters.AddWithValue("@to", to);

            var r = cmd.ExecuteReader();
            while (r.Read())
                Console.WriteLine($"{r["TableName"]} | {r["ActionType"]} | {r["ActionTime"]}");
        }
    }
}
