CREATE DATABASE HotelDB;


USE HotelDB;


CREATE TABLE Phong (
    SoPhong VARCHAR(10) PRIMARY KEY,
    LoaiPhong NVARCHAR(50) NOT NULL,
    GiaTheoGio DECIMAL(18,2) NOT NULL CONSTRAINT CK_Phong_Gia CHECK (GiaTheoGio >= 0),
    TrangThai NVARCHAR(30) CONSTRAINT DF_Phong_TrangThai DEFAULT N'Trống'
);

CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    CCCD VARCHAR(20) UNIQUE NOT NULL,
    SoDienThoai VARCHAR(15)
);

CREATE TABLE DichVu (
    MaDV INT IDENTITY(1,1) PRIMARY KEY,
    TenDV NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL CONSTRAINT CK_DichVu_Gia CHECK (DonGia >= 0)
);

CREATE TABLE PhieuDatPhong (
    MaPhieu INT IDENTITY(1,1) PRIMARY KEY,
    SoPhong VARCHAR(10) CONSTRAINT FK_Phieu_Phong FOREIGN KEY REFERENCES Phong(SoPhong),
    MaKH INT CONSTRAINT FK_Phieu_KhachHang FOREIGN KEY REFERENCES KhachHang(MaKH),
    NgayNhanPhong DATETIME DEFAULT GETDATE(),
    NgayTraPhong DATETIME NULL,
    TongTienThanhToan DECIMAL(18,2) DEFAULT 0
);

CREATE TABLE SuDungDichVu (
    MaPhieu INT CONSTRAINT FK_SDDV_Phieu FOREIGN KEY REFERENCES PhieuDatPhong(MaPhieu),
    MaDV INT CONSTRAINT FK_SDDV_DichVu FOREIGN KEY REFERENCES DichVu(MaDV),
    SoLuong INT NOT NULL CONSTRAINT CK_SDDV_SoLuong CHECK (SoLuong > 0),
    ThoiGian DATETIME DEFAULT GETDATE(),
    CONSTRAINT PK_SuDungDichVu PRIMARY KEY (MaPhieu, MaDV, ThoiGian)
);

INSERT INTO Phong (SoPhong, LoaiPhong, GiaTheoGio, TrangThai) VALUES
('P101', N'Đơn', 50000, N'Trống'),
('P102', N'Đơn', 50000, N'Trống'),
('P201', N'Đôi', 80000, N'Trống'),
('P202', N'Đôi', 80000, N'Trống'),
('P301', N'VIP', 150000, N'Trống');

INSERT INTO DichVu (TenDV, DonGia) VALUES
(N'Nước suối', 10000),
(N'Cà phê', 20000),
(N'Giặt ủi (bộ)', 30000),
(N'Mì ly', 15000);
