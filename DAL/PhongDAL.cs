using System.Data;
using Microsoft.Data.SqlClient;

namespace HotelManagement.DAL
{
    public class PhongDAL
    {
        // Lấy danh sách phòng cho mô hình ngắt kết nối
        public DataTable GetAllRooms()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT SoPhong, LoaiPhong, GiaTheoGio, TrangThai FROM Phong", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}