using HotelManagement.DAL;
using HotelManagement.DTO;

namespace HotelManagement.BUS
{
    public class KhachHangBUS
    {
        private KhachHangDAL dal = new KhachHangDAL();

        public KhachHangDTO? GetByCCCD(string cccd) => dal.GetByCCCD(cccd);
        public int Insert(KhachHangDTO kh) => dal.Insert(kh);
    }
}