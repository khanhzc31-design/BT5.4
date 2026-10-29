using System.Data;
using HotelManagement.DAL;

namespace HotelManagement.BUS
{
    public class PhongBUS
    {
        private PhongDAL dal = new PhongDAL();
        public DataTable GetAllRooms() => dal.GetAllRooms();
    }
}