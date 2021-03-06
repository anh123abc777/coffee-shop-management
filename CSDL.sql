USE [master]
GO
/****** Object:  Database [QLCF]    Script Date: 06/05/2021 16:43:19 ******/
CREATE DATABASE [QLCF] ON  PRIMARY 
( NAME = N'QLCF', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\QLCF.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLCF_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\QLCF_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLCF] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLCF].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLCF] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [QLCF] SET ANSI_NULLS OFF
GO
ALTER DATABASE [QLCF] SET ANSI_PADDING OFF
GO
ALTER DATABASE [QLCF] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [QLCF] SET ARITHABORT OFF
GO
ALTER DATABASE [QLCF] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [QLCF] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [QLCF] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [QLCF] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [QLCF] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [QLCF] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [QLCF] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [QLCF] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [QLCF] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [QLCF] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [QLCF] SET  DISABLE_BROKER
GO
ALTER DATABASE [QLCF] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [QLCF] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [QLCF] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [QLCF] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [QLCF] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [QLCF] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [QLCF] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [QLCF] SET  READ_WRITE
GO
ALTER DATABASE [QLCF] SET RECOVERY SIMPLE
GO
ALTER DATABASE [QLCF] SET  MULTI_USER
GO
ALTER DATABASE [QLCF] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [QLCF] SET DB_CHAINING OFF
GO
USE [QLCF]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 06/05/2021 16:43:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[account] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[admin] [bit] NOT NULL,
 CONSTRAINT [PK_TaiKhoan_1] PRIMARY KEY CLUSTERED 
(
	[account] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[TaiKhoan] ([account], [password], [admin]) VALUES (N'a', N'aaa', 1)
INSERT [dbo].[TaiKhoan] ([account], [password], [admin]) VALUES (N'admin', N'admin', 1)
INSERT [dbo].[TaiKhoan] ([account], [password], [admin]) VALUES (N'hao', N'hao', 0)
/****** Object:  Table [dbo].[LoaiNuoc]    Script Date: 06/05/2021 16:43:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiNuoc](
	[MaLoaiNuoc] [nvarchar](7) NOT NULL,
	[TenLoaiNuoc] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LoaiNuoc] PRIMARY KEY CLUSTERED 
(
	[MaLoaiNuoc] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[LoaiNuoc] ([MaLoaiNuoc], [TenLoaiNuoc]) VALUES (N'cfe', N'Cà Phê')
INSERT [dbo].[LoaiNuoc] ([MaLoaiNuoc], [TenLoaiNuoc]) VALUES (N'dua', N'Dừa')
INSERT [dbo].[LoaiNuoc] ([MaLoaiNuoc], [TenLoaiNuoc]) VALUES (N'sda', N'Soda')
INSERT [dbo].[LoaiNuoc] ([MaLoaiNuoc], [TenLoaiNuoc]) VALUES (N'sto', N'Sinh tố')
INSERT [dbo].[LoaiNuoc] ([MaLoaiNuoc], [TenLoaiNuoc]) VALUES (N'tra', N'Trà')
INSERT [dbo].[LoaiNuoc] ([MaLoaiNuoc], [TenLoaiNuoc]) VALUES (N'ytt', N'Yaourt')
/****** Object:  Table [dbo].[ThucUong]    Script Date: 06/05/2021 16:43:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThucUong](
	[MaNuoc] [nvarchar](6) NOT NULL,
	[TenNuoc] [nvarchar](50) NULL,
	[Gia] [int] NULL,
	[MaLoaiNuoc] [nvarchar](7) NULL,
	[image] [nvarchar](100) NULL,
 CONSTRAINT [PK_ThucUong_1] PRIMARY KEY CLUSTERED 
(
	[MaNuoc] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'cfe101', N'Cà phê sữa', 30000, N'cfe', N'../water/cach-pha-che-ca-phe-sua-de.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'cfe103', N'sữa bạc ', 35000, N'cfe', N'C:\Users\haohao\Pictures\water\tiet-lo-bi-quyet-lam-ca-phe-bac-ha-ngon-nhat-kteti.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'cfe104', N'Cà Phê', 25000, N'cfe', N'C:\Users\haohao\Pictures\water\cafe-den-da_18234c186f2f44f0a2d7ec1ce0e58158_master.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'cfe105', N'Bạc Sĩu', 20000, N'cfe', N'C:\Users\haohao\Pictures\water\cafe-den-da_18234c186f2f44f0a2d7ec1ce0e58158_master.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'sda101', N'Soda Việt Quất', 26000, N'sda', N'C:\Users\haohao\Pictures\ct243\taobaocaochonhanvien.PNG')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'sto100', N'Sinh tố Xoài', 25000, N'sto', N'C:\Users\haohao\Pictures\water\cach-lam-sinh-to-xoai-sua-dac.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'sto101', N'Sinh tố Bơ', 30000, N'sto', N'C:\Users\haohao\Pictures\water\sinh-to-bo-sua-dua-thumbnail.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'sto102', N'Sinh tố Sapoche', 28000, N'sto', N'C:\Users\haohao\Pictures\water\download.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'sto103', N'Sinh tố Đu đủ', 28000, N'sto', N'C:\Users\haohao\Pictures\water\cach-lam-mon-sinh-to-du-du.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'tra100', N'Trà đường', 25000, N'tra', N'C:\Users\haohao\Pictures\water\tra-duong-thuc-uong-thom-ngon.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'tra101', N'Trà đá', 20000, N'tra', N'C:\Users\haohao\Pictures\water\tra-chanh.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'tra102', N'Trà Chanh', 25000, N'tra', N'C:\Users\haohao\Pictures\water\tra-chanh.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'tra103', N'Trà Lipton', 25000, N'tra', N'C:\Users\haohao\Pictures\water\image2-29.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'tra104', N'Trà đào', 25000, N'tra', N'C:\Users\haohao\Pictures\water\tra-dao-cam-sapng_58268b7877cd4209b8fc3fa1d4909511_master.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'yrt100', N'Yaourt đá', 25000, N'ytt', N'C:\Users\haohao\Pictures\water\yaourt-da.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'yrt101', N'Yaourt dâu', 30000, N'ytt', N'C:\Users\haohao\Pictures\water\yaourt-dau.jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'yrt102', N'Yaourt xoài', 30000, N'ytt', N'C:\Users\haohao\Pictures\water\download (1).jpg')
INSERT [dbo].[ThucUong] ([MaNuoc], [TenNuoc], [Gia], [MaLoaiNuoc], [image]) VALUES (N'ytt103', N'Yaourt cam', 30000, N'ytt', N'C:\Users\haohao\Pictures\water\sua-chua-cam-di-an-pho.png')
/****** Object:  Table [dbo].[HoaDon]    Script Date: 06/05/2021 16:43:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[IDBill] [nvarchar](10) NOT NULL,
	[Date] [datetime] NULL,
	[TotalAmount] [int] NULL,
	[Account] [nvarchar](50) NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[IDBill] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[HoaDon] ([IDBill], [Date], [TotalAmount], [Account]) VALUES (N'10', CAST(0x0000AD3E0101DEE5 AS DateTime), 142200, N'hao')
INSERT [dbo].[HoaDon] ([IDBill], [Date], [TotalAmount], [Account]) VALUES (N'5', CAST(0x0000AD3E00F58B60 AS DateTime), 226000, N'hao')
INSERT [dbo].[HoaDon] ([IDBill], [Date], [TotalAmount], [Account]) VALUES (N'6', CAST(0x0000AD3E00F5D1B0 AS DateTime), 90000, N'hao')
INSERT [dbo].[HoaDon] ([IDBill], [Date], [TotalAmount], [Account]) VALUES (N'7', CAST(0x0000AD3E00FA3EF4 AS DateTime), 210000, N'hao')
INSERT [dbo].[HoaDon] ([IDBill], [Date], [TotalAmount], [Account]) VALUES (N'8', CAST(0x0000AD3E00FDB945 AS DateTime), 109800, N'hao')
INSERT [dbo].[HoaDon] ([IDBill], [Date], [TotalAmount], [Account]) VALUES (N'9', CAST(0x0000AD3E01001BD3 AS DateTime), 130500, N'hao')
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 06/05/2021 16:43:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[IDBill] [nvarchar](10) NULL,
	[NameWater] [nvarchar](50) NULL,
	[Quantity] [int] NULL,
	[TotalPrice] [int] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'7', N'sữa bạc ', 1, 35000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'7', N'Bạc sĩu', 2, 64000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'7', N'Sinh tố Sapoche', 2, 56000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'7', N'Sinh tố Xoài', 1, 25000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'7', N'Cà phê sữa', 1, 30000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'8', N'sữa bạc ', 1, 35000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'8', N'Bạc sĩu', 1, 32000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'8', N'Sinh tố Bơ', 1, 30000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'8', N'Sinh tố Xoài', 1, 25000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'9', N'Cà Phê', 1, 25000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'9', N'sữa bạc ', 1, 35000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'5', N'Sinh tố Bơ', 4, 120000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'5', N'Sinh tố Sapoche', 2, 56000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'5', N'Sinh tố Xoài', 2, 50000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'6', N'Sinh tố Bơ', 3, 90000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'9', N'Sinh tố Bơ', 2, 60000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'9', N'Sinh tố Xoài', 1, 25000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'10', N'Cà Phê', 2, 50000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'10', N'sữa bạc ', 1, 35000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'10', N'Sinh tố Sapoche', 1, 28000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'10', N'Sinh tố Xoài', 1, 25000)
INSERT [dbo].[ChiTietHoaDon] ([IDBill], [NameWater], [Quantity], [TotalPrice]) VALUES (N'10', N'Trà đá', 1, 20000)
/****** Object:  ForeignKey [FK_ThucUong_LoaiNuoc]    Script Date: 06/05/2021 16:43:19 ******/
ALTER TABLE [dbo].[ThucUong]  WITH CHECK ADD  CONSTRAINT [FK_ThucUong_LoaiNuoc] FOREIGN KEY([MaLoaiNuoc])
REFERENCES [dbo].[LoaiNuoc] ([MaLoaiNuoc])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ThucUong] CHECK CONSTRAINT [FK_ThucUong_LoaiNuoc]
GO
/****** Object:  ForeignKey [FK_HoaDon_TaiKhoan]    Script Date: 06/05/2021 16:43:19 ******/
ALTER TABLE [dbo].[HoaDon]  WITH NOCHECK ADD  CONSTRAINT [FK_HoaDon_TaiKhoan] FOREIGN KEY([Account])
REFERENCES [dbo].[TaiKhoan] ([account])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_TaiKhoan]
GO
/****** Object:  ForeignKey [FK_ChiTietHoaDon_HoaDon]    Script Date: 06/05/2021 16:43:19 ******/
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_HoaDon] FOREIGN KEY([IDBill])
REFERENCES [dbo].[HoaDon] ([IDBill])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_HoaDon]
GO
