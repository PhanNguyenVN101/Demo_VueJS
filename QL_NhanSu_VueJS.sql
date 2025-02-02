USE [QL_NhanSu_VueJS]
GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 12/06/2024 22:14:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucVu](
	[Id_CV] [int] IDENTITY(1,1) NOT NULL,
	[Ten_CV] [nvarchar](100) NULL,
	[Id_PB] [int] NULL,
	[DeletedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [pk_Id_CV_CV] PRIMARY KEY CLUSTERED 
(
	[Id_CV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 12/06/2024 22:14:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[Id_NV] [int] IDENTITY(1,1) NOT NULL,
	[HoTen_NV] [nvarchar](100) NULL,
	[GioiTinh_NV] [nvarchar](10) NULL,
	[DiaChi_NV] [nvarchar](max) NULL,
	[Email_NV] [varchar](50) NULL,
	[NgaySinh_NV] [date] NULL,
	[DeletedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[Id_PB] [int] NULL,
	[Id_CV] [int] NULL,
 CONSTRAINT [pk_Id_NV_NV] PRIMARY KEY CLUSTERED 
(
	[Id_NV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhongBan]    Script Date: 12/06/2024 22:14:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhongBan](
	[Id_PB] [int] IDENTITY(1,1) NOT NULL,
	[Ten_PB] [nvarchar](100) NULL,
	[ViTri_PB] [nvarchar](100) NULL,
	[DeletedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [pk_Id_PB_PB] PRIMARY KEY CLUSTERED 
(
	[Id_PB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 12/06/2024 22:14:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[Ten_TK] [varchar](100) NOT NULL,
	[MK_TK] [varchar](max) NOT NULL,
	[DeletedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[Id_NV] [int] NULL,
 CONSTRAINT [pk_Ten_TK_TK] PRIMARY KEY CLUSTERED 
(
	[Ten_TK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ChucVu] ON 

INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (1, N'Kế toán viên', 1, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (2, N'Trưởng phòng kế toán', 1, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (3, N'Trưởng phòng kinh doanh', 2, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (4, N'Nhân viên kinh doanh', 2, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (5, N'Trưởng phòng nhân sự', 3, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (6, N'Nhân viên nhân sự', 3, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (7, N'Trưởng phòng dịch vụ', 4, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (8, N'Nhân viên dịch vụ', 4, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (9, N'Trưởng phòng thiết kế', 5, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (10, N'Nhân viên thiết kế', 5, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (11, N'Trưởng phòng CNTT', 6, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (12, N'Nhân viên CNTT', 6, NULL, CAST(N'2024-06-05T22:00:53.447' AS DateTime))
INSERT [dbo].[ChucVu] ([Id_CV], [Ten_CV], [Id_PB], [DeletedDate], [CreatedDate]) VALUES (13, N'sfdsfsd', 4, CAST(N'2024-06-10T17:57:23.083' AS DateTime), CAST(N'2024-06-10T17:54:10.540' AS DateTime))
SET IDENTITY_INSERT [dbo].[ChucVu] OFF
GO
SET IDENTITY_INSERT [dbo].[NhanVien] ON 

INSERT [dbo].[NhanVien] ([Id_NV], [HoTen_NV], [GioiTinh_NV], [DiaChi_NV], [Email_NV], [NgaySinh_NV], [DeletedDate], [CreatedDate], [Id_PB], [Id_CV]) VALUES (1, N'Nguyễn Văn Hà', N'Nam', N'123 Trần Hải, Tân Phú, Tân Bình, TP.Hồ Chí Minh', N'nguyenvanha@gmail.com', CAST(N'1999-02-11' AS Date), NULL, CAST(N'2024-06-05T22:00:53.460' AS DateTime), 1, 1)
INSERT [dbo].[NhanVien] ([Id_NV], [HoTen_NV], [GioiTinh_NV], [DiaChi_NV], [Email_NV], [NgaySinh_NV], [DeletedDate], [CreatedDate], [Id_PB], [Id_CV]) VALUES (2, N'Trần Thị Bình', N'Nữ', N'110 Âu Cơ, Phú Thọ Hòa, Tân Phú, TP.Hồ Chí Mnh', N'tranthibinh@gmail.com', CAST(N'2000-11-12' AS Date), NULL, CAST(N'2024-06-05T22:00:53.460' AS DateTime), 3, 5)
INSERT [dbo].[NhanVien] ([Id_NV], [HoTen_NV], [GioiTinh_NV], [DiaChi_NV], [Email_NV], [NgaySinh_NV], [DeletedDate], [CreatedDate], [Id_PB], [Id_CV]) VALUES (3, N'Phạm Văn Cao', N'Nam', N'226 Thống Nhất, Phú Thọ Hòa, Tân Phú, TP.Hồ Chí Minh', N'phamvancao@gmail.com', CAST(N'2001-05-14' AS Date), NULL, CAST(N'2024-06-05T22:00:53.460' AS DateTime), 3, 5)
INSERT [dbo].[NhanVien] ([Id_NV], [HoTen_NV], [GioiTinh_NV], [DiaChi_NV], [Email_NV], [NgaySinh_NV], [DeletedDate], [CreatedDate], [Id_PB], [Id_CV]) VALUES (4, N'Nguyễn Thị Thu', N'Nữ', N'12/9/10 Âu Cơ, Phú Thọ Hòa, Tân Phú, TP.Hồ Chí Mnh', N'nguyenthithu@gmail.com', CAST(N'2002-08-14' AS Date), NULL, CAST(N'2024-06-05T22:00:53.460' AS DateTime), 4, 7)
INSERT [dbo].[NhanVien] ([Id_NV], [HoTen_NV], [GioiTinh_NV], [DiaChi_NV], [Email_NV], [NgaySinh_NV], [DeletedDate], [CreatedDate], [Id_PB], [Id_CV]) VALUES (6, N'dsdcds', N'Nam', N'ewfwefwe', N'cxdsa@gmail.com', CAST(N'2002-10-10' AS Date), CAST(N'2024-06-10T10:44:04.133' AS DateTime), CAST(N'2024-06-10T04:51:29.093' AS DateTime), 5, 10)
INSERT [dbo].[NhanVien] ([Id_NV], [HoTen_NV], [GioiTinh_NV], [DiaChi_NV], [Email_NV], [NgaySinh_NV], [DeletedDate], [CreatedDate], [Id_PB], [Id_CV]) VALUES (7, N'fdsf', N'Nam', N'dasdas', N'fsdfs@gmail.com', CAST(N'2002-02-21' AS Date), CAST(N'2024-06-10T18:00:53.567' AS DateTime), CAST(N'2024-06-10T17:59:28.427' AS DateTime), 4, 7)
INSERT [dbo].[NhanVien] ([Id_NV], [HoTen_NV], [GioiTinh_NV], [DiaChi_NV], [Email_NV], [NgaySinh_NV], [DeletedDate], [CreatedDate], [Id_PB], [Id_CV]) VALUES (8, N'dqdq', N'Nam', N'dasd', N'dqwdq@gmail.com', CAST(N'2003-06-11' AS Date), NULL, CAST(N'2024-06-11T02:09:55.630' AS DateTime), 4, 8)
SET IDENTITY_INSERT [dbo].[NhanVien] OFF
GO
SET IDENTITY_INSERT [dbo].[PhongBan] ON 

INSERT [dbo].[PhongBan] ([Id_PB], [Ten_PB], [ViTri_PB], [DeletedDate], [CreatedDate]) VALUES (1, N'Kế toán', N'Tầng 102', NULL, CAST(N'2024-06-05T22:00:53.430' AS DateTime))
INSERT [dbo].[PhongBan] ([Id_PB], [Ten_PB], [ViTri_PB], [DeletedDate], [CreatedDate]) VALUES (2, N'Kinh doanh', N'Tầng 204', NULL, CAST(N'2024-06-05T22:00:53.430' AS DateTime))
INSERT [dbo].[PhongBan] ([Id_PB], [Ten_PB], [ViTri_PB], [DeletedDate], [CreatedDate]) VALUES (3, N'Nhân sự', N'Tầng 305', NULL, CAST(N'2024-06-05T22:00:53.430' AS DateTime))
INSERT [dbo].[PhongBan] ([Id_PB], [Ten_PB], [ViTri_PB], [DeletedDate], [CreatedDate]) VALUES (4, N'Dịch vụ', N'Tầng 206', NULL, CAST(N'2024-06-05T22:00:53.430' AS DateTime))
INSERT [dbo].[PhongBan] ([Id_PB], [Ten_PB], [ViTri_PB], [DeletedDate], [CreatedDate]) VALUES (5, N'Thiết kế', N'Tầng 303', NULL, CAST(N'2024-06-05T22:00:53.430' AS DateTime))
INSERT [dbo].[PhongBan] ([Id_PB], [Ten_PB], [ViTri_PB], [DeletedDate], [CreatedDate]) VALUES (6, N'CNTT', N'Tầng 105', NULL, CAST(N'2024-06-05T22:00:53.430' AS DateTime))
INSERT [dbo].[PhongBan] ([Id_PB], [Ten_PB], [ViTri_PB], [DeletedDate], [CreatedDate]) VALUES (7, N'kklk', N'kjk', CAST(N'2024-06-10T18:00:44.957' AS DateTime), CAST(N'2024-06-10T18:00:35.187' AS DateTime))
INSERT [dbo].[PhongBan] ([Id_PB], [Ten_PB], [ViTri_PB], [DeletedDate], [CreatedDate]) VALUES (8, N'swswq', N'dsfwefwef', CAST(N'2024-06-10T18:06:35.677' AS DateTime), CAST(N'2024-06-10T18:01:18.533' AS DateTime))
INSERT [dbo].[PhongBan] ([Id_PB], [Ten_PB], [ViTri_PB], [DeletedDate], [CreatedDate]) VALUES (9, N'fds', N'dsadas', NULL, CAST(N'2024-06-10T20:28:47.533' AS DateTime))
SET IDENTITY_INSERT [dbo].[PhongBan] OFF
GO
INSERT [dbo].[TaiKhoan] ([Ten_TK], [MK_TK], [DeletedDate], [CreatedDate], [Id_NV]) VALUES (N'Admin', N'123', NULL, CAST(N'2024-06-05T22:00:53.463' AS DateTime), 1)
GO
ALTER TABLE [dbo].[ChucVu]  WITH CHECK ADD  CONSTRAINT [fk_Id_PB_CV] FOREIGN KEY([Id_PB])
REFERENCES [dbo].[PhongBan] ([Id_PB])
GO
ALTER TABLE [dbo].[ChucVu] CHECK CONSTRAINT [fk_Id_PB_CV]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [fk_Id_Id_CV] FOREIGN KEY([Id_CV])
REFERENCES [dbo].[ChucVu] ([Id_CV])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [fk_Id_Id_CV]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [fk_Id_PB_NV] FOREIGN KEY([Id_PB])
REFERENCES [dbo].[PhongBan] ([Id_PB])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [fk_Id_PB_NV]
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD  CONSTRAINT [fk_Id_NV_TK] FOREIGN KEY([Id_NV])
REFERENCES [dbo].[NhanVien] ([Id_NV])
GO
ALTER TABLE [dbo].[TaiKhoan] CHECK CONSTRAINT [fk_Id_NV_TK]
GO
