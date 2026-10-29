using Microsoft.Data.SqlClient;

namespace HotelManagement.DAL
{
    public class DatabaseHelper
    {
        public static string ConnectionString = @"Server=.;Database=HotelDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}