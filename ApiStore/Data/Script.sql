USE [APPNegoDB]
GO

/****** Object:  Table [dbo].[Productos]    Script Date: 16/9/2024 18:56:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/* PRODUCTOS */
CREATE TABLE [dbo].[Productos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCategoria] [int] NULL,
	[NumeroProducto] [varchar](50) NOT NULL,
	[NombreProducto] [varchar](50) NULL,
	[Medidas] [varchar](50) NULL,
	[Cantidad] [int] NULL,
	[Peso] [int] NULL,
	[Precio] [decimal] NULL,
	[TipoDeMoneda] [varchar](50) NULL,
	[Descripcion] [varchar](300) NULL,
	[Stock] [int] NULL,
	[Imagen] [varchar](300) NULL,
	[RutaImagen] [varchar](300) NULL,
	[RutaImagenLocal] [varchar](300) NULL,
	[Fecha] [datetime] NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/* Usuarios */
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Password] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Domicilio] [varchar](50) NULL,
	[IdRol] [int] NULL, /* 0:Admin, 1:Clientes, 2:Vendedores, niveles */
	[Fecha] [datetime] NULL,
	[Activo] [varchar](50) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/* Carrito de Compras */
CREATE TABLE [dbo].[CarritoCompra](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuarios] [int] NULL,
	[IdProducto] [int] NULL,
	[CostoTotal] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Descripcion] [varchar](400) NULL,
	[Fecha] [DateTime] NULL,
 CONSTRAINT [PK_CarritoCompra] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

Go

/* Registro */
CREATE TABLE [dbo].[Registro](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Password] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Domicilio] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[Celular] [varchar](50) NULL,
	[Provincia] [varchar](50) NULL,
	[Localidad] [varchar](50) NULL,
	[CodigoPostal] [int] NULL,
	[IdRol] [int] NULL, /* 0:Admin, 1:Clientes, 2:Vendedores, niveles */
	[Fecha] [datetime] NULL,
	[Activo] [varchar](50) NULL,
 CONSTRAINT [PK_Registro] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

Go

/* Mis Favoritos */

/* Mis Compras */

/* MetodoPago */

/* Envios*/

/* PuntosXCompras */


