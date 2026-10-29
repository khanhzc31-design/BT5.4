using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HotelManagement.BUS;
using HotelManagement.DTO;

namespace HotelManagement
{
    public partial class Form1 : Form
    {
        private PhongBUS phongBUS = new PhongBUS();
        private KhachHangBUS khachHangBUS = new KhachHangBUS();
        private DichVuBUS dichVuBUS = new DichVuBUS();
        private PhieuDatPhongBUS phieuBUS = new PhieuDatPhongBUS();

        private DataTable dtRooms = new DataTable();
        private DataView dvRooms = new DataView();

        private int currentCustomerMaKH = -1;
        private decimal calculatedGrandTotal = 0;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDisconnectedRooms();
            LoadCheckInCombos();
            LoadCheckOutCombos();

            // Event Bindings
            cboFilterType.SelectedIndexChanged += FilterRooms;
            cboFilterStatus.SelectedIndexChanged += FilterRooms;
            btnSearchCCCD.Click += BtnSearchCCCD_Click;
            btnCheckIn.Click += BtnCheckIn_Click;
            cboOccupiedRooms.SelectedIndexChanged += CboOccupiedRooms_SelectedIndexChanged;
            btnAddService.Click += BtnAddService_Click;
            btnCheckOut.Click += BtnCheckOut_Click;
        }

        #region Chức năng 4: Lọc & Tìm kiếm phòng ngắt kết nối (DataView)
        private void LoadDisconnectedRooms()
        {
            dtRooms = phongBUS.GetAllRooms();
            dvRooms = new DataView(dtRooms);
            dgvRooms.DataSource = dvRooms;

            cboFilterType.Items.Clear();
            cboFilterType.Items.Add("Tất cả");
            cboFilterType.Items.Add("Đơn");
            cboFilterType.Items.Add("Đôi");
            cboFilterType.Items.Add("VIP");
            cboFilterType.SelectedIndex = 0;

            cboFilterStatus.Items.Clear();
            cboFilterStatus.Items.Add("Tất cả");
            cboFilterStatus.Items.Add("Trống");
            cboFilterStatus.Items.Add("Đang ở");
            cboFilterStatus.Items.Add("Đang dọn dẹp");
            cboFilterStatus.SelectedIndex = 0;
        }

        private void FilterRooms(object sender, EventArgs e)
        {
            string filterType = cboFilterType.SelectedItem?.ToString() ?? "Tất cả";
            string filterStatus = cboFilterStatus.SelectedItem?.ToString() ?? "Tất cả";

            string query = "";
            if (filterType != "Tất cả") query += $"LoaiPhong = '{filterType}'";
            if (filterStatus != "Tất cả")
            {
                if (query != "") query += " AND ";
                query += $"TrangThai = '{filterStatus}'";
            }

            dvRooms.RowFilter = query;
        }
        #endregion

        #region Chức năng 1: Check-in (Nhận phòng)
        private void LoadCheckInCombos()
        {
            DataView dvAvailable = new DataView(phongBUS.GetAllRooms());
            dvAvailable.RowFilter = "TrangThai = 'Trống'";
            cboAvailableRooms.DataSource = dvAvailable;
            cboAvailableRooms.DisplayMember = "SoPhong";
            cboAvailableRooms.ValueMember = "SoPhong";
        }

        private void BtnSearchCCCD_Click(object sender, EventArgs e)
        {
            string cccd = txtCCCD.Text.Trim();
            if (string.IsNullOrEmpty(cccd))
            {
                MessageBox.Show("Vui lòng nhập số CCCD!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KhachHangDTO? kh = khachHangBUS.GetByCCCD(cccd);
            if (kh != null)
            {
                currentCustomerMaKH = kh.MaKH;
                txtHoTen.Text = kh.HoTen;
                txtSĐT.Text = kh.SoDienThoai;
                MessageBox.Show("Tìm thấy thông tin khách hàng cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                currentCustomerMaKH = -1;
                txtHoTen.Clear();
                txtSĐT.Clear();
                MessageBox.Show("Khách hàng mới! Vui lòng nhập Họ tên và SĐT để tạo thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
            }
        }

        private void BtnCheckIn_Click(object sender, EventArgs e)
        {
            if (cboAvailableRooms.SelectedValue == null)
            {
                MessageBox.Show("Không có phòng trống nào được chọn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string soPhong = cboAvailableRooms.SelectedValue.ToString()!;

            // Nếu là khách mới
            if (currentCustomerMaKH == -1)
            {
                string hoTen = txtHoTen.Text.Trim();
                string cccd = txtCCCD.Text.Trim();
                string sdt = txtSĐT.Text.Trim();

                if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(cccd))
                {
                    MessageBox.Show("Họ tên và CCCD không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                KhachHangDTO khMoi = new KhachHangDTO { HoTen = hoTen, CCCD = cccd, SoDienThoai = sdt };
                currentCustomerMaKH = khachHangBUS.Insert(khMoi);
            }

            if (phieuBUS.CheckIn(soPhong, currentCustomerMaKH))
            {
                MessageBox.Show($"Check-in thành công phòng {soPhong}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDisconnectedRooms();
                LoadCheckInCombos();
                LoadCheckOutCombos();
            }
        }
        #endregion

        #region Chức năng 2 & 3: Gọi Dịch Vụ & Check-out (SqlTransaction)
        private void LoadCheckOutCombos()
        {
            DataView dvOccupied = new DataView(phongBUS.GetAllRooms());
            dvOccupied.RowFilter = "TrangThai = 'Đang ở'";
            cboOccupiedRooms.DataSource = dvOccupied;
            cboOccupiedRooms.DisplayMember = "SoPhong";
            cboOccupiedRooms.ValueMember = "SoPhong";

            cboServices.DataSource = dichVuBUS.GetAllServices();
            cboServices.DisplayMember = "TenDV";
            cboServices.ValueMember = "MaDV";
        }

        private void CboOccupiedRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOccupiedRooms.SelectedValue == null) return;
            string soPhong = cboOccupiedRooms.SelectedValue.ToString()!;

            DataRow? phieu = phieuBUS.GetActivePhieuByRoom(soPhong);
            if (phieu == null) return;

            int maPhieu = Convert.ToInt32(phieu["MaPhieu"]);
            DateTime ngayNhan = Convert.ToDateTime(phieu["NgayNhanPhong"]);
            decimal giaTheoGio = Convert.ToDecimal(phieu["GiaTheoGio"]);

            // Tính tiền phòng theo giờ
            TimeSpan duration = DateTime.Now - ngayNhan;
            double hours = Math.Ceiling(duration.TotalHours);
            if (hours < 1) hours = 1;

            decimal roomTotal = (decimal)hours * giaTheoGio;
            lblRoomTotalMoney.Text = $"Tiền phòng ({hours}h): {roomTotal:#,##0} VNĐ";

            // Load dịch vụ đã dùng
            DataTable dtServices = phieuBUS.GetServicesByPhieu(maPhieu);
            dgvUsedServices.DataSource = dtServices;

            decimal serviceTotal = 0;
            foreach (DataRow row in dtServices.Rows)
            {
                serviceTotal += Convert.ToDecimal(row["ThanhTien"]);
            }
            lblServiceTotalMoney.Text = $"Tiền dịch vụ: {serviceTotal:#,##0} VNĐ";

            calculatedGrandTotal = roomTotal + serviceTotal;
            lblGrandTotal.Text = $"TỔNG THANH TOÁN: {calculatedGrandTotal:#,##0} VNĐ";
        }

        private void BtnAddService_Click(object sender, EventArgs e)
        {
            if (cboOccupiedRooms.SelectedValue == null || cboServices.SelectedValue == null) return;

            string soPhong = cboOccupiedRooms.SelectedValue.ToString()!;
            DataRow? phieu = phieuBUS.GetActivePhieuByRoom(soPhong);
            if (phieu == null) return;

            SuDungDichVuDTO sddv = new SuDungDichVuDTO
            {
                MaPhieu = Convert.ToInt32(phieu["MaPhieu"]),
                MaDV = Convert.ToInt32(cboServices.SelectedValue),
                SoLuong = (int)nudServiceQty.Value
            };

            if (phieuBUS.AddService(sddv))
            {
                MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CboOccupiedRooms_SelectedIndexChanged(null, null);
            }
        }

        private void BtnCheckOut_Click(object sender, EventArgs e)
        {
            if (cboOccupiedRooms.SelectedValue == null) return;

            string soPhong = cboOccupiedRooms.SelectedValue.ToString()!;
            DataRow? phieu = phieuBUS.GetActivePhieuByRoom(soPhong);
            if (phieu == null) return;

            int maPhieu = Convert.ToInt32(phieu["MaPhieu"]);

            DialogResult confirm = MessageBox.Show(
                $"Xác nhận Check-out và xuất hóa đơn cho phòng {soPhong}?\nTổng thanh toán: {calculatedGrandTotal:#,##0} VNĐ",
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            if (phieuBUS.CheckOutTransaction(maPhieu, soPhong, calculatedGrandTotal))
            {
                MessageBox.Show($"Check-out thành công phòng {soPhong}! Trạng thái phòng chuyển sang 'Đang dọn dẹp'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDisconnectedRooms();
                LoadCheckInCombos();
                LoadCheckOutCombos();
            }
            else
            {
                MessageBox.Show("Lỗi trong quá trình Check-out (Transaction Rollback)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}