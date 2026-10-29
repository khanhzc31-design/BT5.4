using System;
using System.Data;
using Microsoft.Data.SqlClient;
using HotelManagement.DTO;

namespace HotelManagement.DAL
{
    public class PhieuDatPhongDAL
    {
        // 1. Lập phiếu Check-in & cập nhật trạng thái phòng thành "Đang ở"
        public bool CheckIn(string soPhong, int maKH)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string insertPhieu = "INSERT INTO PhieuDatPhong (SoPhong, MaKH, NgayNhanPhong) VALUES (@SoPhong, @MaKH, GETDATE())";
                    SqlCommand cmd1 = new SqlCommand(insertPhieu, conn, trans);
                    cmd1.Parameters.AddWithValue("@SoPhong", soPhong);
                    cmd1.Parameters.AddWithValue("@MaKH", maKH);
                    cmd1.ExecuteNonQuery();

                    string updatePhong = "UPDATE Phong SET TrangThai = N'Đang ở' WHERE SoPhong = @SoPhong";
                    SqlCommand cmd2 = new SqlCommand(updatePhong, conn, trans);
                    cmd2.Parameters.AddWithValue("@SoPhong", soPhong);
                    cmd2.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        // Lấy phiếu đặt phòng chưa thanh toán theo số phòng
        public DataRow? GetActivePhieuByRoom(string soPhong)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"SELECT p.MaPhieu, p.SoPhong, p.NgayNhanPhong, k.HoTen, ph.GiaTheoGio
                                 FROM PhieuDatPhong p
                                 JOIN KhachHang k ON p.MaKH = k.MaKH
                                 JOIN Phong ph ON p.SoPhong = ph.SoPhong
                                 WHERE p.SoPhong = @SoPhong AND p.NgayTraPhong IS NULL";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SoPhong", soPhong);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }

        // 2. Thêm sử dụng dịch vụ
        public bool AddService(SuDungDichVuDTO sddv)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "INSERT INTO SuDungDichVu (MaPhieu, MaDV, SoLuong, ThoiGian) VALUES (@MaPhieu, @MaDV, @SoLuong, GETDATE())";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", sddv.MaPhieu);
                cmd.Parameters.AddWithValue("@MaDV", sddv.MaDV);
                cmd.Parameters.AddWithValue("@SoLuong", sddv.SoLuong);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Lấy danh sách dịch vụ đã dùng của một phiếu
        public DataTable GetServicesByPhieu(int maPhieu)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"SELECT dv.TenDV, sd.SoLuong, dv.DonGia, (sd.SoLuong * dv.DonGia) AS ThanhTien, sd.ThoiGian
                                 FROM SuDungDichVu sd
                                 JOIN DichVu dv ON sd.MaDV = dv.MaDV
                                 WHERE sd.MaPhieu = @MaPhieu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 3. Thực thi Check-out & Xuất hóa đơn bằng SqlTransaction
        public bool CheckOutTransaction(int maPhieu, string soPhong, decimal tongTien)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // Cập nhật phiếu đặt phòng
                    string updatePhieu = "UPDATE PhieuDatPhong SET NgayTraPhong = GETDATE(), TongTienThanhToan = @TongTien WHERE MaPhieu = @MaPhieu";
                    SqlCommand cmd1 = new SqlCommand(updatePhieu, conn, trans);
                    cmd1.Parameters.AddWithValue("@TongTien", tongTien);
                    cmd1.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    cmd1.ExecuteNonQuery();

                    // Chuyển trạng thái phòng sang "Đang dọn dẹp"
                    string updatePhong = "UPDATE Phong SET TrangThai = N'Đang dọn dẹp' WHERE SoPhong = @SoPhong";
                    SqlCommand cmd2 = new SqlCommand(updatePhong, conn, trans);
                    cmd2.Parameters.AddWithValue("@SoPhong", soPhong);
                    cmd2.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
    }
}