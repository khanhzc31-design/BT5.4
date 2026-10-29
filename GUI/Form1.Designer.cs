using System.Drawing;
using System.Windows.Forms;

namespace HotelManagement
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private TabControl tabMain;
        private TabPage tabRooms, tabCheckIn, tabCheckOut;

        // Tab 1 Controls
        private DataGridView dgvRooms;
        private ComboBox cboFilterType, cboFilterStatus;
        private Label lblFilterType, lblFilterStatus;

        // Tab 2 Controls
        private TextBox txtCCCD, txtHoTen, txtSĐT;
        private ComboBox cboAvailableRooms;
        private Button btnSearchCCCD, btnCheckIn;
        private Label lblCCCD, lblHoTen, lblSĐT, lblRoomSelect;

        // Tab 3 Controls
        private ComboBox cboOccupiedRooms, cboServices;
        private NumericUpDown nudServiceQty;
        private Button btnAddService, btnCheckOut;
        private DataGridView dgvUsedServices;
        private Label lblOccupiedRooms, lblServices, lblServiceQty, lblRoomTotalMoney, lblServiceTotalMoney, lblGrandTotal;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabMain = new TabControl { Dock = DockStyle.Fill };
            this.tabRooms = new TabPage("Lọc & Tìm Kiếm Phòng");
            this.tabCheckIn = new TabPage("Check-in Nhận Phòng");
            this.tabCheckOut = new TabPage("Gọi Dịch Vụ & Check-out");

            // Tab 1 UI
            this.lblFilterType = new Label { Text = "Loại phòng:", Location = new Point(20, 20), AutoSize = true };
            this.cboFilterType = new ComboBox { Location = new Point(100, 17), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            this.lblFilterStatus = new Label { Text = "Trạng thái:", Location = new Point(280, 20), AutoSize = true };
            this.cboFilterStatus = new ComboBox { Location = new Point(360, 17), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            this.dgvRooms = new DataGridView { Location = new Point(20, 60), Size = new Size(1000, 520), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true };

            this.tabRooms.Controls.AddRange(new Control[] { lblFilterType, cboFilterType, lblFilterStatus, cboFilterStatus, dgvRooms });

            // Tab 2 UI
            this.lblCCCD = new Label { Text = "Số CCCD:", Location = new Point(30, 30), AutoSize = true };
            this.txtCCCD = new TextBox { Location = new Point(130, 27), Width = 200 };
            this.btnSearchCCCD = new Button { Text = "Tìm Khách", Location = new Point(340, 25), Width = 100, Height = 28 };

            this.lblHoTen = new Label { Text = "Họ và tên:", Location = new Point(30, 80), AutoSize = true };
            this.txtHoTen = new TextBox { Location = new Point(130, 77), Width = 310 };

            this.lblSĐT = new Label { Text = "Số điện thoại:", Location = new Point(30, 130), AutoSize = true };
            this.txtSĐT = new TextBox { Location = new Point(130, 127), Width = 310 };

            this.lblRoomSelect = new Label { Text = "Chọn phòng trống:", Location = new Point(30, 180), AutoSize = true };
            this.cboAvailableRooms = new ComboBox { Location = new Point(150, 177), Width = 290, DropDownStyle = ComboBoxStyle.DropDownList };

            this.btnCheckIn = new Button { Text = "XÁC NHẬN CHECK-IN", Location = new Point(130, 240), Width = 310, Height = 45, BackColor = Color.LightGreen, Font = new Font("Arial", 10, FontStyle.Bold) };

            this.tabCheckIn.Controls.AddRange(new Control[] { lblCCCD, txtCCCD, btnSearchCCCD, lblHoTen, txtHoTen, lblSĐT, txtSĐT, lblRoomSelect, cboAvailableRooms, btnCheckIn });

            // Tab 3 UI
            this.lblOccupiedRooms = new Label { Text = "Chọn phòng đang ở:", Location = new Point(20, 20), AutoSize = true };
            this.cboOccupiedRooms = new ComboBox { Location = new Point(150, 17), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };

            this.lblServices = new Label { Text = "Chọn dịch vụ:", Location = new Point(360, 20), AutoSize = true };
            this.cboServices = new ComboBox { Location = new Point(450, 17), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };

            this.lblServiceQty = new Label { Text = "Số lượng:", Location = new Point(650, 20), AutoSize = true };
            this.nudServiceQty = new NumericUpDown { Location = new Point(720, 17), Width = 60, Minimum = 1, Maximum = 100, Value = 1 };

            this.btnAddService = new Button { Text = "Thêm Dịch Vụ", Location = new Point(800, 15), Width = 120, Height = 28, BackColor = Color.LightSkyBlue };

            this.dgvUsedServices = new DataGridView { Location = new Point(20, 60), Size = new Size(1000, 380), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true };

            this.lblRoomTotalMoney = new Label { Text = "Tiền phòng: 0 VNĐ", Location = new Point(20, 460), Font = new Font("Arial", 11, FontStyle.Bold), AutoSize = true };
            this.lblServiceTotalMoney = new Label { Text = "Tiền dịch vụ: 0 VNĐ", Location = new Point(300, 460), Font = new Font("Arial", 11, FontStyle.Bold), AutoSize = true };
            this.lblGrandTotal = new Label { Text = "TỔNG THANH TOÁN: 0 VNĐ", Location = new Point(600, 460), Font = new Font("Arial", 12, FontStyle.Bold), ForeColor = Color.DarkRed, AutoSize = true };

            this.btnCheckOut = new Button { Text = "CHECK-OUT & XUẤT HÓA ĐƠN", Location = new Point(740, 500), Width = 280, Height = 45, BackColor = Color.Coral, Font = new Font("Arial", 11, FontStyle.Bold) };

            this.tabCheckOut.Controls.AddRange(new Control[] {
                lblOccupiedRooms, cboOccupiedRooms, lblServices, cboServices, lblServiceQty, nudServiceQty, btnAddService,
                dgvUsedServices, lblRoomTotalMoney, lblServiceTotalMoney, lblGrandTotal, btnCheckOut
            });

            this.tabMain.Controls.AddRange(new Control[] { tabRooms, tabCheckIn, tabCheckOut });
            this.ClientSize = new Size(1050, 640);
            this.Controls.Add(tabMain);
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Tiện Ích Dịch Vụ & Khách Sạn (Hotel Management System)";
        }
    }
}