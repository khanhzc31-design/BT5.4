using System;
using System.Data;
using HotelManagement.DAL;
using HotelManagement.DTO;

namespace HotelManagement.BUS
{
    public class PhieuDatPhongBUS
    {
        private PhieuDatPhongDAL dal = new PhieuDatPhongDAL();

        public bool CheckIn(string soPhong, int maKH) => dal.CheckIn(soPhong, maKH);
        public DataRow? GetActivePhieuByRoom(string soPhong) => dal.GetActivePhieuByRoom(soPhong);
        public bool AddService(SuDungDichVuDTO sddv) => dal.AddService(sddv);
        public DataTable GetServicesByPhieu(int maPhieu) => dal.GetServicesByPhieu(maPhieu);
        public bool CheckOutTransaction(int maPhieu, string soPhong, decimal tongTien) => dal.CheckOutTransaction(maPhieu, soPhong, tongTien);
    }
}