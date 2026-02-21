using Microsoft.Data.SqlClient;

namespace HealthClinicApp.Utilities
{
    public class DbConnectionFactory
    {
        private static readonly string connectionString =
            "Server=localhost\\SQLEXPRESS;Database=HealthClinicDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
