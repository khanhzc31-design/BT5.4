using System.Data;
using HotelManagement.DAL;

namespace HotelManagement.BUS
{
    public class DichVuBUS
    {
        private DichVuDAL dal = new DichVuDAL();
        public DataTable GetAllServices() => dal.GetAllServices();
    }
}