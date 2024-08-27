-- INSTRUÇÕES PARA CONECTAR O BANCO DE DADOS AO PROJETO MVC:

-- 1º Execute o script 'SARVA v9.0' no MS SQL

-- 2º Na pasta Models da solução do Projeto MVC 'SARVA v9.0', 
-- exclua o Model de Entity Framework

-- 3º Vá no arquivo WebConfig do projeto MVC 'SARVA v9.0' e 
-- procure pela tag <connectionStrings>

-- 4º Apague, dentro da tag <connectionStrings>, a tag <add>,
-- que contém a connectionString do Model antigo

-- 5º Adicione um novo item na pasta Models 
-- do tipo "Dados" -> "ADO.NET Entity Data Model"

-- 6º Selecione o servidor do SQL em que foi adicionado o banco
-- e selecione o banco SARVA

-- 7º Defina o nome da nova connectionString como "connSARVA"

-- 8º Adicione o novo Model ao projeto, conectado ao banco de dados






USE [master]
GO
/****** Object:  Database [SARVA]    Script Date: 06/01/2024 15:42:28 ******/
CREATE DATABASE [SARVA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SARVA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SARVA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SARVA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SARVA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SARVA] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SARVA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SARVA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SARVA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SARVA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SARVA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SARVA] SET ARITHABORT OFF 
GO
ALTER DATABASE [SARVA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SARVA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SARVA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SARVA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SARVA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SARVA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SARVA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SARVA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SARVA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SARVA] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SARVA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SARVA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SARVA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SARVA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SARVA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SARVA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SARVA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SARVA] SET RECOVERY FULL 
GO
ALTER DATABASE [SARVA] SET  MULTI_USER 
GO
ALTER DATABASE [SARVA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SARVA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SARVA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SARVA] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SARVA] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SARVA] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SARVA', N'ON'
GO
ALTER DATABASE [SARVA] SET QUERY_STORE = OFF
GO
USE [SARVA]
GO
/****** Object:  Table [dbo].[Ciclo]    Script Date: 06/01/2024 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciclo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_empresa] [int] NULL,
	[nome] [varchar](50) NULL,
	[dataInicio] [date] NULL,
	[dataFim] [date] NULL,
 CONSTRAINT [PK_Ciclo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 06/01/2024 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[nome] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[aniversario] [date] NULL,
	[scoreId] [int] NULL,
 CONSTRAINT [PK__Cliente__3213E83FE753010F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 06/01/2024 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresa](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[razao_social] [varchar](50) NULL,
	[flag] [bit] NULL,
 CONSTRAINT [PK__Empresa__3213E83F88A4D44A] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item_Pedido]    Script Date: 06/01/2024 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item_Pedido](
	[codigo_IV] [int] NOT NULL,
	[id_ciclo_IV] [int] NOT NULL,
	[id_venda_IV] [int] NOT NULL,
	[id_pedido] [int] NOT NULL,
	[valor] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Item_Pedido_1] PRIMARY KEY CLUSTERED 
(
	[codigo_IV] ASC,
	[id_ciclo_IV] ASC,
	[id_venda_IV] ASC,
	[id_pedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item_Venda]    Script Date: 06/01/2024 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item_Venda](
	[codigo_produto] [int] NOT NULL,
	[id_ciclo_produto] [int] NOT NULL,
	[id_venda] [int] NOT NULL,
	[valor] [decimal](18, 2) NULL,
	[quantidade] [int] NULL,
	[data_validade] [date] NULL,
 CONSTRAINT [PK_Item_Venda] PRIMARY KEY CLUSTERED 
(
	[codigo_produto] ASC,
	[id_ciclo_produto] ASC,
	[id_venda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 06/01/2024 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_empresa] [int] NULL,
	[valor] [decimal](18, 2) NULL,
	[data_pedido] [date] NULL,
	[data_vencimento] [date] NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produto]    Script Date: 06/01/2024 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto](
	[id_ciclo] [int] NOT NULL,
	[codigo] [int] NOT NULL,
	[id_usuario] [int] NULL,
	[id_empresa] [int] NULL,
	[nome] [varchar](50) NULL,
	[valor] [decimal](18, 2) NULL,
	[pontos] [int] NULL,
	[flag] [bit] NULL,
 CONSTRAINT [PK_Produto_1] PRIMARY KEY CLUSTERED 
(
	[id_ciclo] ASC,
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 06/01/2024 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Score](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [varchar](50) NULL,
 CONSTRAINT [PK_Score] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 06/01/2024 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[senha] [varchar](50) NULL,
	[roleId] [int] NULL,
 CONSTRAINT [PK__Usuario__3213E83FC63111C1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuarioRole]    Script Date: 06/01/2024 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioRole](
	[roleId] [int] NOT NULL,
	[roleName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venda]    Script Date: 06/01/2024 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venda](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_cliente] [int] NULL,
	[id_empresa] [int] NULL,
	[valor] [decimal](18, 2) NULL,
	[data_venda] [date] NULL,
	[data_vencimento] [date] NULL,
	[data_pagamento] [date] NULL,
	[desconto] [decimal](18, 2) NULL,
	[valorFinal] [decimal](18, 2) NULL,
 CONSTRAINT [PK__Venda__3213E83FB0F14735] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Ciclo] ON 

INSERT [dbo].[Ciclo] ([id], [id_empresa], [nome], [dataInicio], [dataFim]) VALUES (11, 2, N'Ciclo 23', CAST(N'2023-12-01' AS Date), CAST(N'2024-01-15' AS Date))
INSERT [dbo].[Ciclo] ([id], [id_empresa], [nome], [dataInicio], [dataFim]) VALUES (12, 3, N'Ciclo 19', CAST(N'2023-12-01' AS Date), CAST(N'2023-12-31' AS Date))
INSERT [dbo].[Ciclo] ([id], [id_empresa], [nome], [dataInicio], [dataFim]) VALUES (13, 3, N'Ciclo 20', CAST(N'2024-01-01' AS Date), CAST(N'2024-01-31' AS Date))
SET IDENTITY_INSERT [dbo].[Ciclo] OFF
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([id], [id_usuario], [nome], [email], [aniversario], [scoreId]) VALUES (21, 3, N'Maria', N'masf1205@gmail.com', CAST(N'1973-07-22' AS Date), 1)
INSERT [dbo].[Cliente] ([id], [id_usuario], [nome], [email], [aniversario], [scoreId]) VALUES (1013, 3, N'Fernando', N'fen@gmail.com', CAST(N'2024-01-03' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[Empresa] ON 

INSERT [dbo].[Empresa] ([id], [id_usuario], [razao_social], [flag]) VALUES (2, 4, N'Natura', 1)
INSERT [dbo].[Empresa] ([id], [id_usuario], [razao_social], [flag]) VALUES (3, 4, N'Avon', 1)
SET IDENTITY_INSERT [dbo].[Empresa] OFF
GO
INSERT [dbo].[Item_Pedido] ([codigo_IV], [id_ciclo_IV], [id_venda_IV], [id_pedido], [valor]) VALUES (4556, 11, 1246, 162, CAST(119.90 AS Decimal(18, 2)))
INSERT [dbo].[Item_Pedido] ([codigo_IV], [id_ciclo_IV], [id_venda_IV], [id_pedido], [valor]) VALUES (4556, 11, 1246, 1151, CAST(119.90 AS Decimal(18, 2)))
INSERT [dbo].[Item_Pedido] ([codigo_IV], [id_ciclo_IV], [id_venda_IV], [id_pedido], [valor]) VALUES (4556, 11, 1259, 162, CAST(119.90 AS Decimal(18, 2)))
INSERT [dbo].[Item_Pedido] ([codigo_IV], [id_ciclo_IV], [id_venda_IV], [id_pedido], [valor]) VALUES (4556, 11, 1259, 1151, CAST(119.90 AS Decimal(18, 2)))
GO
INSERT [dbo].[Item_Venda] ([codigo_produto], [id_ciclo_produto], [id_venda], [valor], [quantidade], [data_validade]) VALUES (4556, 11, 1246, CAST(119.90 AS Decimal(18, 2)), 2, NULL)
INSERT [dbo].[Item_Venda] ([codigo_produto], [id_ciclo_produto], [id_venda], [valor], [quantidade], [data_validade]) VALUES (4556, 11, 1259, CAST(119.90 AS Decimal(18, 2)), 1, NULL)
INSERT [dbo].[Item_Venda] ([codigo_produto], [id_ciclo_produto], [id_venda], [valor], [quantidade], [data_validade]) VALUES (4556, 11, 2249, CAST(119.90 AS Decimal(18, 2)), 1, NULL)
INSERT [dbo].[Item_Venda] ([codigo_produto], [id_ciclo_produto], [id_venda], [valor], [quantidade], [data_validade]) VALUES (8997, 11, 2246, CAST(65.00 AS Decimal(18, 2)), 2, NULL)
INSERT [dbo].[Item_Venda] ([codigo_produto], [id_ciclo_produto], [id_venda], [valor], [quantidade], [data_validade]) VALUES (8997, 11, 2249, CAST(65.00 AS Decimal(18, 2)), 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[Pedido] ON 

INSERT [dbo].[Pedido] ([id], [id_usuario], [id_empresa], [valor], [data_pedido], [data_vencimento]) VALUES (162, 3, 2, CAST(251.79 AS Decimal(18, 2)), CAST(N'2024-01-02' AS Date), CAST(N'2024-01-23' AS Date))
INSERT [dbo].[Pedido] ([id], [id_usuario], [id_empresa], [valor], [data_pedido], [data_vencimento]) VALUES (1151, 3, 2, NULL, CAST(N'2024-01-05' AS Date), CAST(N'2024-01-26' AS Date))
SET IDENTITY_INSERT [dbo].[Pedido] OFF
GO
INSERT [dbo].[Produto] ([id_ciclo], [codigo], [id_usuario], [id_empresa], [nome], [valor], [pontos], [flag]) VALUES (11, 4556, 4, 2, N'Perfume Kaiak', CAST(119.90 AS Decimal(18, 2)), NULL, 1)
INSERT [dbo].[Produto] ([id_ciclo], [codigo], [id_usuario], [id_empresa], [nome], [valor], [pontos], [flag]) VALUES (11, 6789, 4, 2, N'Perfume Lua', CAST(100.00 AS Decimal(18, 2)), NULL, 1)
INSERT [dbo].[Produto] ([id_ciclo], [codigo], [id_usuario], [id_empresa], [nome], [valor], [pontos], [flag]) VALUES (11, 8997, 4, 2, N'Hidratante Todo Dia', CAST(65.00 AS Decimal(18, 2)), NULL, 1)
INSERT [dbo].[Produto] ([id_ciclo], [codigo], [id_usuario], [id_empresa], [nome], [valor], [pontos], [flag]) VALUES (11, 112765, 4, 2, N'Ekos Patauá Shampoo', CAST(39.90 AS Decimal(18, 2)), NULL, 1)
INSERT [dbo].[Produto] ([id_ciclo], [codigo], [id_usuario], [id_empresa], [nome], [valor], [pontos], [flag]) VALUES (13, 239856, 4, 3, N'Blush Bastão Sou Intensidade', CAST(41.99 AS Decimal(18, 2)), NULL, 1)
INSERT [dbo].[Produto] ([id_ciclo], [codigo], [id_usuario], [id_empresa], [nome], [valor], [pontos], [flag]) VALUES (13, 567845, 4, 3, N'Batom Ultramate', CAST(29.99 AS Decimal(18, 2)), NULL, 1)
INSERT [dbo].[Produto] ([id_ciclo], [codigo], [id_usuario], [id_empresa], [nome], [valor], [pontos], [flag]) VALUES (13, 890564, 4, 3, N'Máscara Limpeza Facial Avon Care', CAST(15.99 AS Decimal(18, 2)), NULL, 1)
INSERT [dbo].[Produto] ([id_ciclo], [codigo], [id_usuario], [id_empresa], [nome], [valor], [pontos], [flag]) VALUES (13, 904567, 4, 3, N'Batom Ultracremoso FPS 15', CAST(29.99 AS Decimal(18, 2)), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Score] ON 

INSERT [dbo].[Score] ([id], [descricao]) VALUES (1, N'Bom Pagador')
INSERT [dbo].[Score] ([id], [descricao]) VALUES (2, N'Mau Pagador')
INSERT [dbo].[Score] ([id], [descricao]) VALUES (3, N'Neutro')
SET IDENTITY_INSERT [dbo].[Score] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([id], [userName], [email], [senha], [roleId]) VALUES (3, N'Vendedor 1', N'v1@gmail.com', N'123', 2)
INSERT [dbo].[Usuario] ([id], [userName], [email], [senha], [roleId]) VALUES (4, N'Admin 1', N'admin1@gmail.com', N'admin123', 1)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
INSERT [dbo].[UsuarioRole] ([roleId], [roleName]) VALUES (1, N'Admin')
INSERT [dbo].[UsuarioRole] ([roleId], [roleName]) VALUES (2, N'Vendedor')
GO
SET IDENTITY_INSERT [dbo].[Venda] ON 

INSERT [dbo].[Venda] ([id], [id_usuario], [id_cliente], [id_empresa], [valor], [data_venda], [data_vencimento], [data_pagamento], [desconto], [valorFinal]) VALUES (1246, 3, 21, 2, CAST(239.80 AS Decimal(18, 2)), CAST(N'2023-12-30' AS Date), CAST(N'2023-12-31' AS Date), CAST(N'2024-01-05' AS Date), CAST(50.00 AS Decimal(18, 2)), CAST(189.80 AS Decimal(18, 2)))
INSERT [dbo].[Venda] ([id], [id_usuario], [id_cliente], [id_empresa], [valor], [data_venda], [data_vencimento], [data_pagamento], [desconto], [valorFinal]) VALUES (1259, 3, 1013, 2, CAST(119.90 AS Decimal(18, 2)), CAST(N'2024-01-02' AS Date), CAST(N'2024-01-31' AS Date), CAST(N'2024-01-21' AS Date), NULL, CAST(119.90 AS Decimal(18, 2)))
INSERT [dbo].[Venda] ([id], [id_usuario], [id_cliente], [id_empresa], [valor], [data_venda], [data_vencimento], [data_pagamento], [desconto], [valorFinal]) VALUES (2246, 3, 21, 2, CAST(130.00 AS Decimal(18, 2)), CAST(N'2024-01-05' AS Date), CAST(N'2024-01-31' AS Date), CAST(N'2024-01-05' AS Date), NULL, NULL)
INSERT [dbo].[Venda] ([id], [id_usuario], [id_cliente], [id_empresa], [valor], [data_venda], [data_vencimento], [data_pagamento], [desconto], [valorFinal]) VALUES (2247, 3, 1013, 2, CAST(0.00 AS Decimal(18, 2)), CAST(N'2024-01-05' AS Date), CAST(N'2024-01-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Venda] ([id], [id_usuario], [id_cliente], [id_empresa], [valor], [data_venda], [data_vencimento], [data_pagamento], [desconto], [valorFinal]) VALUES (2249, 3, 21, 2, CAST(249.90 AS Decimal(18, 2)), CAST(N'2024-01-05' AS Date), CAST(N'2024-01-31' AS Date), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Venda] OFF
GO
ALTER TABLE [dbo].[Ciclo]  WITH CHECK ADD  CONSTRAINT [FK_Ciclo_Empresa] FOREIGN KEY([id_empresa])
REFERENCES [dbo].[Empresa] ([id])
GO
ALTER TABLE [dbo].[Ciclo] CHECK CONSTRAINT [FK_Ciclo_Empresa]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [FK__Cliente__id_usua__300424B4] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [FK__Cliente__id_usua__300424B4]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Score] FOREIGN KEY([scoreId])
REFERENCES [dbo].[Score] ([id])
GO
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [FK_Cliente_Score]
GO
ALTER TABLE [dbo].[Empresa]  WITH CHECK ADD  CONSTRAINT [FK_Empresa_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Empresa] CHECK CONSTRAINT [FK_Empresa_Usuario]
GO
ALTER TABLE [dbo].[Item_Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Item_Pedido_Item_Venda] FOREIGN KEY([codigo_IV], [id_ciclo_IV], [id_venda_IV])
REFERENCES [dbo].[Item_Venda] ([codigo_produto], [id_ciclo_produto], [id_venda])
GO
ALTER TABLE [dbo].[Item_Pedido] CHECK CONSTRAINT [FK_Item_Pedido_Item_Venda]
GO
ALTER TABLE [dbo].[Item_Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Item_Pedido_Pedido2] FOREIGN KEY([id_pedido])
REFERENCES [dbo].[Pedido] ([id])
GO
ALTER TABLE [dbo].[Item_Pedido] CHECK CONSTRAINT [FK_Item_Pedido_Pedido2]
GO
ALTER TABLE [dbo].[Item_Venda]  WITH CHECK ADD  CONSTRAINT [FK__Item_Vend__id_ve__38996AB5] FOREIGN KEY([id_venda])
REFERENCES [dbo].[Venda] ([id])
GO
ALTER TABLE [dbo].[Item_Venda] CHECK CONSTRAINT [FK__Item_Vend__id_ve__38996AB5]
GO
ALTER TABLE [dbo].[Item_Venda]  WITH CHECK ADD  CONSTRAINT [FK_Item_Venda_Produto1] FOREIGN KEY([id_ciclo_produto], [codigo_produto])
REFERENCES [dbo].[Produto] ([id_ciclo], [codigo])
GO
ALTER TABLE [dbo].[Item_Venda] CHECK CONSTRAINT [FK_Item_Venda_Produto1]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Empresa] FOREIGN KEY([id_empresa])
REFERENCES [dbo].[Empresa] ([id])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Empresa]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Usuario]
GO
ALTER TABLE [dbo].[Produto]  WITH CHECK ADD  CONSTRAINT [FK__Produto__id_empr__2D27B809] FOREIGN KEY([id_empresa])
REFERENCES [dbo].[Empresa] ([id])
GO
ALTER TABLE [dbo].[Produto] CHECK CONSTRAINT [FK__Produto__id_empr__2D27B809]
GO
ALTER TABLE [dbo].[Produto]  WITH CHECK ADD  CONSTRAINT [FK__Produto__id_usua__2C3393D0] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Produto] CHECK CONSTRAINT [FK__Produto__id_usua__2C3393D0]
GO
ALTER TABLE [dbo].[Produto]  WITH CHECK ADD  CONSTRAINT [FK_Produto_Ciclo1] FOREIGN KEY([id_ciclo])
REFERENCES [dbo].[Ciclo] ([id])
GO
ALTER TABLE [dbo].[Produto] CHECK CONSTRAINT [FK_Produto_Ciclo1]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK__Usuario__roleId__267ABA7A] FOREIGN KEY([roleId])
REFERENCES [dbo].[UsuarioRole] ([roleId])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK__Usuario__roleId__267ABA7A]
GO
ALTER TABLE [dbo].[Venda]  WITH CHECK ADD  CONSTRAINT [FK__Venda__id_client__33D4B598] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[Cliente] ([id])
GO
ALTER TABLE [dbo].[Venda] CHECK CONSTRAINT [FK__Venda__id_client__33D4B598]
GO
ALTER TABLE [dbo].[Venda]  WITH CHECK ADD  CONSTRAINT [FK__Venda__id_empres__34C8D9D1] FOREIGN KEY([id_empresa])
REFERENCES [dbo].[Empresa] ([id])
GO
ALTER TABLE [dbo].[Venda] CHECK CONSTRAINT [FK__Venda__id_empres__34C8D9D1]
GO
ALTER TABLE [dbo].[Venda]  WITH CHECK ADD  CONSTRAINT [FK__Venda__id_usuari__32E0915F] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Venda] CHECK CONSTRAINT [FK__Venda__id_usuari__32E0915F]
GO
USE [master]
GO
ALTER DATABASE [SARVA] SET  READ_WRITE 
GO
