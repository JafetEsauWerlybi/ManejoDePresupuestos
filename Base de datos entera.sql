USE [master]
GO
/****** Object:  Database [ManejoDePresupuesto]    Script Date: 31/05/2023 08:37:41 a. m. ******/
CREATE DATABASE [ManejoDePresupuesto]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ManejoDePresupuesto', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\ManejoDePresupuesto.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ManejoDePresupuesto_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\ManejoDePresupuesto_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ManejoDePresupuesto] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ManejoDePresupuesto].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ManejoDePresupuesto] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET ARITHABORT OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ManejoDePresupuesto] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ManejoDePresupuesto] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ManejoDePresupuesto] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ManejoDePresupuesto] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ManejoDePresupuesto] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ManejoDePresupuesto] SET  MULTI_USER 
GO
ALTER DATABASE [ManejoDePresupuesto] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ManejoDePresupuesto] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ManejoDePresupuesto] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ManejoDePresupuesto] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ManejoDePresupuesto] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ManejoDePresupuesto] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ManejoDePresupuesto] SET QUERY_STORE = OFF
GO
USE [ManejoDePresupuesto]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 31/05/2023 08:37:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[idCategoria] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[TipoOperacionId] [int] NOT NULL,
	[UsuarioId] [int] NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CuentasClientes]    Script Date: 31/05/2023 08:37:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CuentasClientes](
	[idCuenta] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[TipoCuentaId] [int] NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[Descripcion] [nvarchar](200) NULL,
 CONSTRAINT [PK_CuentasClientes] PRIMARY KEY CLUSTERED 
(
	[idCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoCuenta]    Script Date: 31/05/2023 08:37:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoCuenta](
	[idTipoCuenta] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[Orden] [int] NOT NULL,
 CONSTRAINT [PK_TipoCuenta] PRIMARY KEY CLUSTERED 
(
	[idTipoCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDeOperaciones]    Script Date: 31/05/2023 08:37:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDeOperaciones](
	[idOperaciones] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TipoDeOperaciones] PRIMARY KEY CLUSTERED 
(
	[idOperaciones] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transacciones]    Script Date: 31/05/2023 08:37:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transacciones](
	[idTransaccion] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[FechaTransaccion] [date] NOT NULL,
	[Monto] [decimal](18, 2) NOT NULL,
	[Nota] [nvarchar](50) NULL,
	[CuentaId] [int] NOT NULL,
	[CategoriaId] [int] NULL,
 CONSTRAINT [PK_Transacciones] PRIMARY KEY CLUSTERED 
(
	[idTransaccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 31/05/2023 08:37:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[EmailNormalizado] [nvarchar](250) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 
GO
INSERT [dbo].[Categorias] ([idCategoria], [Nombre], [TipoOperacionId], [UsuarioId]) VALUES (1, N'Cena parís ', 2, 1)
GO
INSERT [dbo].[Categorias] ([idCategoria], [Nombre], [TipoOperacionId], [UsuarioId]) VALUES (2, N'Salario semanal', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[CuentasClientes] ON 
GO
INSERT [dbo].[CuentasClientes] ([idCuenta], [Nombre], [TipoCuentaId], [Balance], [Descripcion]) VALUES (3, N'Laskdflaks', 4, CAST(20192.00 AS Decimal(18, 2)), N'asdjfkasldfjlak')
GO
INSERT [dbo].[CuentasClientes] ([idCuenta], [Nombre], [TipoCuentaId], [Balance], [Descripcion]) VALUES (5, N'Inversión con escala', 6, CAST(450.00 AS Decimal(18, 2)), N'una cuenta para poder invertir en empresas directas de México')
GO
INSERT [dbo].[CuentasClientes] ([idCuenta], [Nombre], [TipoCuentaId], [Balance], [Descripcion]) VALUES (11, N'Desarrollo personal', 7, CAST(-7461.00 AS Decimal(18, 2)), N'Préstamo de desarrollo personal')
GO
INSERT [dbo].[CuentasClientes] ([idCuenta], [Nombre], [TipoCuentaId], [Balance], [Descripcion]) VALUES (12, N'Prestamos', 4, CAST(-12.10 AS Decimal(18, 2)), N'll;;;;;;;;;;;;;;;;;;;;;;;')
GO
INSERT [dbo].[CuentasClientes] ([idCuenta], [Nombre], [TipoCuentaId], [Balance], [Descripcion]) VALUES (20, N'Probando pasivos', 4, CAST(1164.00 AS Decimal(18, 2)), N'Probar como se ve un negativo en esa mae')
GO
INSERT [dbo].[CuentasClientes] ([idCuenta], [Nombre], [TipoCuentaId], [Balance], [Descripcion]) VALUES (21, N'Probando pasivos', 4, CAST(-264.00 AS Decimal(18, 2)), N'Probar como se ve un negativo en esa mae')
GO
SET IDENTITY_INSERT [dbo].[CuentasClientes] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoCuenta] ON 
GO
INSERT [dbo].[TipoCuenta] ([idTipoCuenta], [Nombre], [UsuarioId], [Orden]) VALUES (1, N'prestamossss', 1, 2)
GO
INSERT [dbo].[TipoCuenta] ([idTipoCuenta], [Nombre], [UsuarioId], [Orden]) VALUES (4, N'Tarjeta de Crédito ', 1, 1)
GO
INSERT [dbo].[TipoCuenta] ([idTipoCuenta], [Nombre], [UsuarioId], [Orden]) VALUES (5, N'Perro Mojadp', 1, 4)
GO
INSERT [dbo].[TipoCuenta] ([idTipoCuenta], [Nombre], [UsuarioId], [Orden]) VALUES (6, N'Inversión', 1, 3)
GO
INSERT [dbo].[TipoCuenta] ([idTipoCuenta], [Nombre], [UsuarioId], [Orden]) VALUES (7, N'Cuenta de ahorro', 1, 5)
GO
INSERT [dbo].[TipoCuenta] ([idTipoCuenta], [Nombre], [UsuarioId], [Orden]) VALUES (8, N'PROBANDO FUNCION', 1, 6)
GO
SET IDENTITY_INSERT [dbo].[TipoCuenta] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoDeOperaciones] ON 
GO
INSERT [dbo].[TipoDeOperaciones] ([idOperaciones], [descripcion]) VALUES (1, N'Ingresos')
GO
INSERT [dbo].[TipoDeOperaciones] ([idOperaciones], [descripcion]) VALUES (2, N'Gastos')
GO
INSERT [dbo].[TipoDeOperaciones] ([idOperaciones], [descripcion]) VALUES (3, N'Transferencias')
GO
SET IDENTITY_INSERT [dbo].[TipoDeOperaciones] OFF
GO
SET IDENTITY_INSERT [dbo].[Transacciones] ON 
GO
INSERT [dbo].[Transacciones] ([idTransaccion], [UsuarioId], [FechaTransaccion], [Monto], [Nota], [CuentaId], [CategoriaId]) VALUES (1, 1, CAST(N'2023-05-29' AS Date), CAST(40.00 AS Decimal(18, 2)), N'Ejemplo de transaacion jsjs', 5, 2)
GO
INSERT [dbo].[Transacciones] ([idTransaccion], [UsuarioId], [FechaTransaccion], [Monto], [Nota], [CuentaId], [CategoriaId]) VALUES (2, 1, CAST(N'2023-05-29' AS Date), CAST(10.00 AS Decimal(18, 2)), N'Cuenta prueba', 3, 1)
GO
INSERT [dbo].[Transacciones] ([idTransaccion], [UsuarioId], [FechaTransaccion], [Monto], [Nota], [CuentaId], [CategoriaId]) VALUES (3, 1, CAST(N'2023-05-29' AS Date), CAST(10.00 AS Decimal(18, 2)), N'+sdflsdfgsdfg', 11, 1)
GO
INSERT [dbo].[Transacciones] ([idTransaccion], [UsuarioId], [FechaTransaccion], [Monto], [Nota], [CuentaId], [CategoriaId]) VALUES (5, 1, CAST(N'2023-05-30' AS Date), CAST(570.00 AS Decimal(18, 2)), N'Probandooo papa123', 20, 2)
GO
INSERT [dbo].[Transacciones] ([idTransaccion], [UsuarioId], [FechaTransaccion], [Monto], [Nota], [CuentaId], [CategoriaId]) VALUES (6, 1, CAST(N'2023-05-30' AS Date), CAST(84.00 AS Decimal(18, 2)), N'No', 21, 1)
GO
INSERT [dbo].[Transacciones] ([idTransaccion], [UsuarioId], [FechaTransaccion], [Monto], [Nota], [CuentaId], [CategoriaId]) VALUES (7, 1, CAST(N'2023-05-30' AS Date), CAST(1955.00 AS Decimal(18, 2)), N'jaksdj', 20, 1)
GO
INSERT [dbo].[Transacciones] ([idTransaccion], [UsuarioId], [FechaTransaccion], [Monto], [Nota], [CuentaId], [CategoriaId]) VALUES (8, 1, CAST(N'2023-05-30' AS Date), CAST(100.00 AS Decimal(18, 2)), NULL, 11, 2)
GO
INSERT [dbo].[Transacciones] ([idTransaccion], [UsuarioId], [FechaTransaccion], [Monto], [Nota], [CuentaId], [CategoriaId]) VALUES (9, 1, CAST(N'2023-05-01' AS Date), CAST(50.00 AS Decimal(18, 2)), NULL, 11, 1)
GO
INSERT [dbo].[Transacciones] ([idTransaccion], [UsuarioId], [FechaTransaccion], [Monto], [Nota], [CuentaId], [CategoriaId]) VALUES (10, 1, CAST(N'2023-05-31' AS Date), CAST(500.00 AS Decimal(18, 2)), NULL, 11, 2)
GO
INSERT [dbo].[Transacciones] ([idTransaccion], [UsuarioId], [FechaTransaccion], [Monto], [Nota], [CuentaId], [CategoriaId]) VALUES (11, 1, CAST(N'2023-05-30' AS Date), CAST(1999.00 AS Decimal(18, 2)), NULL, 11, 2)
GO
SET IDENTITY_INSERT [dbo].[Transacciones] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 
GO
INSERT [dbo].[Usuarios] ([idUsuario], [Email], [EmailNormalizado], [PasswordHash]) VALUES (1, N'jecko@gmail.com', N'JECKO@GMAIL.COM', N'asd')
GO
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Categorias_TipoDeOperaciones] FOREIGN KEY([TipoOperacionId])
REFERENCES [dbo].[TipoDeOperaciones] ([idOperaciones])
GO
ALTER TABLE [dbo].[Categorias] CHECK CONSTRAINT [FK_Categorias_TipoDeOperaciones]
GO
ALTER TABLE [dbo].[Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Categorias_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Categorias] CHECK CONSTRAINT [FK_Categorias_Usuarios]
GO
ALTER TABLE [dbo].[CuentasClientes]  WITH CHECK ADD  CONSTRAINT [FK_CuentasClientes_TipoCuenta] FOREIGN KEY([TipoCuentaId])
REFERENCES [dbo].[TipoCuenta] ([idTipoCuenta])
GO
ALTER TABLE [dbo].[CuentasClientes] CHECK CONSTRAINT [FK_CuentasClientes_TipoCuenta]
GO
ALTER TABLE [dbo].[TipoCuenta]  WITH CHECK ADD  CONSTRAINT [FK_TipoCuenta_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[TipoCuenta] CHECK CONSTRAINT [FK_TipoCuenta_Usuarios]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Categorias] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([idCategoria])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_Categorias]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_CuentasClientes] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[CuentasClientes] ([idCuenta])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_CuentasClientes]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_Usuarios]
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertarTipoCuenta]    Script Date: 31/05/2023 08:37:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertarTipoCuenta]
	@Nombre varchar(250),
	@UsuarioId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Orden int;
	SELECT @Orden = COALESCE(MAX(Orden),0)+1
	FROM TipoCuenta
	WHERE UsuarioId = @UsuarioId

	INSERT INto TipoCuenta(Nombre,UsuarioId, Orden)
	values (@Nombre, @UsuarioId, @Orden)

	select SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[Transacciones_Borrar]    Script Date: 31/05/2023 08:37:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Transacciones_Borrar]
	@idTransaccion int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @Monto decimal(18,2);
	DECLARE @CuentaId int;
	DECLARE @TipoOperacionId int;

	SELECT @Monto = Monto, @CuentaId = CuentaId, @TipoOperacionId = Cat.TipoOperacionId
	FROM Transacciones
	inner join Categorias Cat
	ON Cat.idCategoria = Transacciones.CategoriaId
	WHERE Transacciones.idTransaccion = @idTransaccion

	DECLARE @FactorMultiplicativo int = 1;

	if(@TipoOperacionId = 2)
		SET @FactorMultiplicativo = -1;
	
	SET @Monto = @Monto * @FactorMultiplicativo;

	UPDATE CuentasClientes
	set Balance -= @Monto
	where idCuenta=@CuentaId;

	delete Transacciones where idTransaccion= @idTransaccion
END
GO
/****** Object:  StoredProcedure [dbo].[Transacciones_Insertar]    Script Date: 31/05/2023 08:37:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Transacciones_Insertar]
	@UsuarioId int,
	@FechaTransaccion datetime,
	@Monto decimal,
	@CategoriaId int,
	@CuentaId int,
	@Nota nvarchar(100) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO Transacciones (UsuarioId, FechaTransaccion, Monto, CategoriaId, CuentaId, Nota) 
	values (@UsuarioId, @FechaTransaccion, ABS(@Monto),@CategoriaId,@CuentaId,@Nota)

	UPDATE CuentasClientes SET Balance += @Monto
	WHERE idCuenta = @CuentaId;

	SELECT SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[TransaccionesActualizar]    Script Date: 31/05/2023 08:37:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TransaccionesActualizar]
	@idTransaccion int,
	@FechaTransaccion datetime,
	@Monto decimal (18,2),
	@MontoAnterior decimal(18,2),
	@CuentaId int,
	@CuentaAnteriorId int,
	@CategoriaId int,
	@Nota nvarchar(1000)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Revertir transaccion
	UPDATE CuentasClientes
	SET Balance -= @MontoAnterior
	where idCuenta = @CuentaAnteriorId
	--
	Update CuentasClientes 
	SET	Balance += @Monto
	where idCuenta = @CuentaId

	update Transacciones 
	set Monto =ABS(@Monto), FechaTransaccion= @FechaTransaccion,
	CategoriaId = @CategoriaId,  CuentaId = @CuentaId, Nota = @Nota

	where idTransaccion = @idTransaccion
END
GO
USE [master]
GO
ALTER DATABASE [ManejoDePresupuesto] SET  READ_WRITE 
GO
