using System.Data;
using Microsoft.Data.SqlClient;

namespace HotelManagement.DAL
{
    public class DichVuDAL
    {
        public DataTable GetAllServices()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaDV, TenDV, DonGia FROM DichVu", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}