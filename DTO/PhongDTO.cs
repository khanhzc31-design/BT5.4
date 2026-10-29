namespace HotelManagement.DTO
{
    public class PhongDTO
    {
        public string SoPhong { get; set; } = string.Empty;
        public string LoaiPhong { get; set; } = string.Empty;
        public decimal GiaTheoGio { get; set; }
        public string TrangThai { get; set; } = "Trống";
    }
}