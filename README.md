# BÁO CÁO BÀI TẬP 5.4: HỆ THỐNG QUẢN LÝ TIỆN ÍCH DỊCH VỤ & KHÁCH SẠN (HOTEL MANAGEMENT SYSTEM)

## MỤC 1: THÔNG TIN SINH VIÊN
- **Họ và tên sinh viên:** lê đức khánh
- **Mã sinh viên:** 24810310438
- **Tên Đề tài / Bài tập:** Bài tập 5.4 - Hệ thống Quản lý Tiện ích Dịch vụ & Khách sạn (Hotel Management System)
- **Mô hình kiến trúc:** 3-Tier Architecture (GUI - BUS - DAL - DTO)

---

## MỤC 2: HƯỚNG DẪN CÀI ĐẶT & CHẠY DỰ ÁN

### Các bước cài đặt:
1. Mở **SQL Server Management Studio (SSMS)**, thực thi file `Database/ScriptDB.sql` để tạo CSDL `HotelDB` và khởi tạo dữ liệu mẫu.
2. Mở file `HotelManagement.sln` bằng **Visual Studio 2022**.
3. Mở file `DAL/DatabaseHelper.cs`, cập nhật lại chuỗi kết nối `ConnectionString` cho phù hợp với SQL Server trên máy cá nhân.
4. Cài đặt gói `Microsoft.Data.SqlClient` nếu chưa có.
5. Nhấn `F5` hoặc gõ `dotnet run` trong Terminal để Build & Run ứng dụng.

---

## MỤC 3: HÌNH ẢNH KẾT QUẢ CÁC MÀN HÌNH CHỨC NĂNG

1. **Màn hình Lọc & Tìm kiếm phòng ngắt kết nối (DataView / DataTable):**
<img width="1051" height="677" alt="image" src="https://github.com/user-attachments/assets/e138022a-ab48-4ed1-8b0e-a00a61d5e5f6" />


2. **Màn hình Tra cứu Khách hàng (SqlParameter) & Check-in Nhận phòng:**
<img width="1052" height="679" alt="image" src="https://github.com/user-attachments/assets/14182964-7897-4246-b51c-2582e39ec472" />


3. **Màn hình Gọi Dịch vụ lưu trú (Chuyển dữ liệu qua DTO, BUS, DAL):**
<img width="1050" height="674" alt="image" src="https://github.com/user-attachments/assets/cf8a4447-fc9a-48a8-a9d9-94cf2c1c050f" />

4. **Màn hình Check-out & Xuất hóa đơn (Thực thi SqlTransaction):**
<img width="1053" height="675" alt="image" src="https://github.com/user-attachments/assets/c89c36ad-2b0d-4f02-960e-d851de2e2ecd" />

5. **Minh họa Xử lý lỗi & Giao dịch (Rollback / Validation):**
<img width="1052" height="674" alt="image" src="https://github.com/user-attachments/assets/c740ef2b-4cd8-44a3-99ce-c9bf4d1e6f91" />
