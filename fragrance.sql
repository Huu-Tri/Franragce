USE [master]
GO
/****** Object:  Database [fragrance]    Script Date: 25/03/2023 3:16:35 CH ******/
CREATE DATABASE [fragrance]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'fragrance', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\fragrance.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'fragrance_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\fragrance_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [fragrance] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [fragrance].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [fragrance] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [fragrance] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [fragrance] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [fragrance] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [fragrance] SET ARITHABORT OFF 
GO
ALTER DATABASE [fragrance] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [fragrance] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [fragrance] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [fragrance] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [fragrance] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [fragrance] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [fragrance] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [fragrance] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [fragrance] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [fragrance] SET  ENABLE_BROKER 
GO
ALTER DATABASE [fragrance] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [fragrance] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [fragrance] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [fragrance] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [fragrance] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [fragrance] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [fragrance] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [fragrance] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [fragrance] SET  MULTI_USER 
GO
ALTER DATABASE [fragrance] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [fragrance] SET DB_CHAINING OFF 
GO
ALTER DATABASE [fragrance] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [fragrance] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [fragrance] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [fragrance] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [fragrance] SET QUERY_STORE = OFF
GO
USE [fragrance]
GO
/****** Object:  Table [dbo].[acc_user]    Script Date: 25/03/2023 3:16:36 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[acc_user](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[name_user] [varchar](255) NOT NULL,
	[email_user] [varchar](255) NOT NULL,
	[phone_user] [varchar](20) NOT NULL,
	[password_user] [varchar](255) NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 25/03/2023 3:16:36 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[id_ad] [int] IDENTITY(1,1) NOT NULL,
	[name_ad] [varchar](255) NOT NULL,
	[email_ad] [varchar](255) NOT NULL,
	[password_ad] [varchar](100) NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_ad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email_ad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[collection]    Script Date: 25/03/2023 3:16:36 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[collection](
	[id_collect] [int] IDENTITY(1,1) NOT NULL,
	[name_collect] [varchar](250) NOT NULL,
	[desc_collect] [ntext] NULL,
	[image_collect] [varchar](500) NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_collect] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[collection_child]    Script Date: 25/03/2023 3:16:36 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[collection_child](
	[id_c_child] [int] IDENTITY(1,1) NOT NULL,
	[name_c_child] [varchar](250) NOT NULL,
	[desc_c_child] [ntext] NULL,
	[id_c_collect] [int] NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_c_child] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[contact]    Script Date: 25/03/2023 3:16:36 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contact](
	[id_contact] [int] IDENTITY(1,1) NOT NULL,
	[info_contact] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_contact] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[message]    Script Date: 25/03/2023 3:16:36 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[message](
	[id_mes] [int] NULL,
	[fullname_mes] [varchar](100) NULL,
	[email_mes] [varchar](200) NOT NULL,
	[content_mes] [ntext] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_details]    Script Date: 25/03/2023 3:16:36 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_details](
	[id_order_dt] [int] NOT NULL,
	[id_pro] [int] NOT NULL,
	[amount_order_dt] [int] NULL,
	[order_details_total] [decimal](9, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_order_dt] ASC,
	[id_pro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 25/03/2023 3:16:36 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id_pr] [int] IDENTITY(1,1) NOT NULL,
	[name_pr] [varchar](255) NOT NULL,
	[image_pr] [varchar](500) NULL,
	[volume_pr] [int] NOT NULL,
	[price_pr] [decimal](10, 4) NOT NULL,
	[amount_pr] [int] NOT NULL,
	[desc_pr] [ntext] NULL,
	[notes_pr] [ntext] NULL,
	[tips_pr] [ntext] NULL,
	[status_pr] [int] NOT NULL,
	[id_pro_typeof] [int] NULL,
	[id_pro_coll] [int] NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_type]    Script Date: 25/03/2023 3:16:36 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_type](
	[id_prt] [int] IDENTITY(1,1) NOT NULL,
	[name_prt] [varchar](100) NOT NULL,
	[image_prt] [varchar](500) NULL,
	[desc_prt] [ntext] NULL,
	[forgender_prt] [varchar](100) NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_prt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_order]    Script Date: 25/03/2023 3:16:36 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_order](
	[id_order] [int] IDENTITY(1,1) NOT NULL,
	[receiver_oder] [varchar](100) NOT NULL,
	[status_order] [varchar](100) NOT NULL,
	[address_order] [varchar](500) NOT NULL,
	[date_order] [date] NULL,
	[phone_order] [varchar](100) NOT NULL,
	[action_order] [int] NOT NULL,
	[id_order_user] [int] NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_order] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[acc_user] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[admin] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[collection] ADD  DEFAULT (NULL) FOR [desc_collect]
GO
ALTER TABLE [dbo].[collection] ADD  DEFAULT (NULL) FOR [image_collect]
GO
ALTER TABLE [dbo].[collection] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[collection_child] ADD  DEFAULT (NULL) FOR [desc_c_child]
GO
ALTER TABLE [dbo].[collection_child] ADD  DEFAULT (NULL) FOR [id_c_collect]
GO
ALTER TABLE [dbo].[collection_child] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[contact] ADD  DEFAULT (NULL) FOR [info_contact]
GO
ALTER TABLE [dbo].[message] ADD  DEFAULT (NULL) FOR [id_mes]
GO
ALTER TABLE [dbo].[message] ADD  DEFAULT (NULL) FOR [fullname_mes]
GO
ALTER TABLE [dbo].[order_details] ADD  DEFAULT (NULL) FOR [amount_order_dt]
GO
ALTER TABLE [dbo].[product] ADD  DEFAULT (NULL) FOR [desc_pr]
GO
ALTER TABLE [dbo].[product] ADD  DEFAULT (NULL) FOR [notes_pr]
GO
ALTER TABLE [dbo].[product] ADD  DEFAULT (NULL) FOR [tips_pr]
GO
ALTER TABLE [dbo].[product] ADD  DEFAULT (NULL) FOR [id_pro_typeof]
GO
ALTER TABLE [dbo].[product] ADD  DEFAULT (NULL) FOR [id_pro_coll]
GO
ALTER TABLE [dbo].[product] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[product_type] ADD  DEFAULT (NULL) FOR [image_prt]
GO
ALTER TABLE [dbo].[product_type] ADD  DEFAULT (NULL) FOR [desc_prt]
GO
ALTER TABLE [dbo].[product_type] ADD  DEFAULT (NULL) FOR [forgender_prt]
GO
ALTER TABLE [dbo].[product_type] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[user_order] ADD  DEFAULT (NULL) FOR [date_order]
GO
ALTER TABLE [dbo].[user_order] ADD  DEFAULT (NULL) FOR [id_order_user]
GO
ALTER TABLE [dbo].[user_order] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[collection_child]  WITH CHECK ADD  CONSTRAINT [fk_child_collect] FOREIGN KEY([id_c_collect])
REFERENCES [dbo].[collection] ([id_collect])
GO
ALTER TABLE [dbo].[collection_child] CHECK CONSTRAINT [fk_child_collect]
GO
ALTER TABLE [dbo].[order_details]  WITH CHECK ADD  CONSTRAINT [fk_dt_od] FOREIGN KEY([id_order_dt])
REFERENCES [dbo].[user_order] ([id_order])
GO
ALTER TABLE [dbo].[order_details] CHECK CONSTRAINT [fk_dt_od]
GO
ALTER TABLE [dbo].[order_details]  WITH CHECK ADD  CONSTRAINT [fk_dt_pr] FOREIGN KEY([id_pro])
REFERENCES [dbo].[product] ([id_pr])
GO
ALTER TABLE [dbo].[order_details] CHECK CONSTRAINT [fk_dt_pr]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [fk_pro_type] FOREIGN KEY([id_pro_typeof])
REFERENCES [dbo].[product_type] ([id_prt])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [fk_pro_type]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [id_pro_coll] FOREIGN KEY([id_pro_coll])
REFERENCES [dbo].[collection_child] ([id_c_child])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [id_pro_coll]
GO
ALTER TABLE [dbo].[user_order]  WITH CHECK ADD  CONSTRAINT [fk_od_user] FOREIGN KEY([id_order_user])
REFERENCES [dbo].[acc_user] ([id_user])
GO
ALTER TABLE [dbo].[user_order] CHECK CONSTRAINT [fk_od_user]
GO
USE [master]
GO
ALTER DATABASE [fragrance] SET  READ_WRITE 
GO
