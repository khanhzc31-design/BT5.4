using System;
using System.Data;
using Microsoft.Data.SqlClient;
using HotelManagement.DTO;

namespace HotelManagement.DAL
{
    public class KhachHangDAL
    {
        // Tìm kiếm khách hàng theo CCCD bằng SqlParameter chống SQL Injection
        public KhachHangDTO? GetByCCCD(string cccd)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT MaKH, HoTen, CCCD, SoDienThoai FROM KhachHang WHERE CCCD = @CCCD";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add(new SqlParameter("@CCCD", SqlDbType.VarChar) { Value = cccd });

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new KhachHangDTO
                        {
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            HoTen = reader["HoTen"].ToString()!,
                            CCCD = reader["CCCD"].ToString()!,
                            SoDienThoai = reader["SoDienThoai"]?.ToString() ?? ""
                        };
                    }
                }
            }
            return null;
        }

        // Thêm khách hàng mới
        public int Insert(KhachHangDTO kh)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"INSERT INTO KhachHang (HoTen, CCCD, SoDienThoai) 
                                 OUTPUT INSERTED.MaKH 
                                 VALUES (@HoTen, @CCCD, @SĐT)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add(new SqlParameter("@HoTen", SqlDbType.NVarChar) { Value = kh.HoTen });
                cmd.Parameters.Add(new SqlParameter("@CCCD", SqlDbType.VarChar) { Value = kh.CCCD });
                cmd.Parameters.Add(new SqlParameter("@SĐT", SqlDbType.VarChar) { Value = kh.SoDienThoai });

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}