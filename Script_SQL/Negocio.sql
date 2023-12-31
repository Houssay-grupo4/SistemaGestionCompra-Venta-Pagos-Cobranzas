
USE [Negocio]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 02/10/2023 15:41:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[id_Cliente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Completo] [varchar](50) NULL,
	[Telefono] [int] NULL,
	[Email] [varchar](50) NULL,
	[Fecha_Cobro] [date] NULL,
	[Importe] [money] NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[id_Cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 02/10/2023 15:41:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compras](
	[CodigoProducto] [varchar](50) NULL,
	[NombreProductos] [varchar](50) NULL,
	[Proveedores] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pagos]    Script Date: 02/10/2023 15:41:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagos](
	[Fecha] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 02/10/2023 15:41:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[Id_Producto] [int] NOT NULL,
	[Codigo_Producto] [int] NULL,
	[Nombre] [varchar](50) NULL,
	[Cantidad] [int] NULL,
	[Precio] [money] NULL,
 CONSTRAINT [PK_Productos_Id_Producto] PRIMARY KEY CLUSTERED 
(
	[Id_Producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 02/10/2023 15:41:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[id_Proveedor] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Completo] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Fecha_Pago] [date] NULL,
	[Importe] [money] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Clientes] ON 

INSERT [dbo].[Clientes] ([id_Cliente], [Nombre_Completo], [Telefono], [Email], [Fecha_Cobro], [Importe]) VALUES (1, N'Pepito Perez', 258978, N'pepito@', CAST(N'2023-12-01' AS Date), 2000000.0000)
INSERT [dbo].[Clientes] ([id_Cliente], [Nombre_Completo], [Telefono], [Email], [Fecha_Cobro], [Importe]) VALUES (2, N'Papusa Perez', 148796, N'papu@', CAST(N'2023-12-14' AS Date), 11111111.0000)
INSERT [dbo].[Clientes] ([id_Cliente], [Nombre_Completo], [Telefono], [Email], [Fecha_Cobro], [Importe]) VALUES (8, N'Cacho', 2589, N'elcacho@mail', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Clientes] OFF
GO
SET IDENTITY_INSERT [dbo].[Proveedores] ON 

INSERT [dbo].[Proveedores] ([id_Proveedor], [Nombre_Completo], [Telefono], [Email], [Fecha_Pago], [Importe]) VALUES (1, N'Jesus Sanchez', N'152879632', N'porotito', CAST(N'2023-12-15' AS Date), 187000.0000)
INSERT [dbo].[Proveedores] ([id_Proveedor], [Nombre_Completo], [Telefono], [Email], [Fecha_Pago], [Importe]) VALUES (2, N'Claudio Verduga', N'0114589632', N'elclaudio@gmai.com', CAST(N'2023-10-01' AS Date), 152.0000)
INSERT [dbo].[Proveedores] ([id_Proveedor], [Nombre_Completo], [Telefono], [Email], [Fecha_Pago], [Importe]) VALUES (3, N'Pedro Polo', N'123456789', N'polo@', CAST(N'2023-10-01' AS Date), 147896.0000)
INSERT [dbo].[Proveedores] ([id_Proveedor], [Nombre_Completo], [Telefono], [Email], [Fecha_Pago], [Importe]) VALUES (4, N'Yo Cacho', N'3659', N'cacho@com', CAST(N'2023-10-01' AS Date), 5.0000)
SET IDENTITY_INSERT [dbo].[Proveedores] OFF
GO
USE [master]
GO
ALTER DATABASE [Negocio] SET  READ_WRITE 
GO
