CREATE DATABASE [ReservaTurnos];
GO
USE [ReservaTurnos]
GO
/****** Object:  Table [dbo].[comercios]    Script Date: 12/06/2024 2:23:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comercios](
	[id_comercio] [int] IDENTITY(1,1) NOT NULL,
	[nom_comercio] [varchar](50) NOT NULL,
	[aforo_maximo] [int] NULL,
 CONSTRAINT [PK_comercios] PRIMARY KEY CLUSTERED 
(
	[id_comercio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[servicios]    Script Date: 12/06/2024 2:23:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[servicios](
	[id_servicio] [int] IDENTITY(1,1) NOT NULL,
	[id_comercio] [int] NOT NULL,
	[nom_servicio] [varchar](50) NOT NULL,
	[hora_apertura] [time](7) NOT NULL,
	[hora_cierre] [time](7) NOT NULL,
	[duracion] [int] NOT NULL,
 CONSTRAINT [PK_servicios] PRIMARY KEY CLUSTERED 
(
	[id_servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[turnos]    Script Date: 12/06/2024 2:23:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[turnos](
	[id_turno] [int] IDENTITY(1,1) NOT NULL,
	[id_servicio] [int] NOT NULL,
	[fecha_turno] [date] NOT NULL,
	[hora_inicio] [time](7) NOT NULL,
	[hora_fin] [time](7) NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_turnos] PRIMARY KEY CLUSTERED 
(
	[id_turno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[servicios]  WITH CHECK ADD  CONSTRAINT [FK_servicios_comercios] FOREIGN KEY([id_comercio])
REFERENCES [dbo].[comercios] ([id_comercio])
GO
ALTER TABLE [dbo].[servicios] CHECK CONSTRAINT [FK_servicios_comercios]
GO
ALTER TABLE [dbo].[turnos]  WITH CHECK ADD  CONSTRAINT [FK_turnos_servicios] FOREIGN KEY([id_servicio])
REFERENCES [dbo].[servicios] ([id_servicio])
GO
ALTER TABLE [dbo].[turnos] CHECK CONSTRAINT [FK_turnos_servicios]
GO
/****** Object:  StoredProcedure [dbo].[GenerarTurnos]    Script Date: 12/06/2024 2:23:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerarTurnos]
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @id_servicio INT
AS
BEGIN
	SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
			DECLARE @hora_apertura TIME, @hora_cierre TIME, @duracion INT;
    
			-- Obtener hora de apertura, hora de cierre y duración del servicio
			SELECT @hora_apertura = hora_apertura, @hora_cierre = hora_cierre, @duracion = duracion
			FROM servicios
			WHERE id_servicio = @id_servicio;

			UPDATE turnos SET estado = 0 WHERE id_servicio = @id_servicio AND fecha_turno BETWEEN @fecha_inicio AND @fecha_fin AND estado = 1
    
			DECLARE @fecha_actual DATE;
			SET @fecha_actual = @fecha_inicio;
    
			-- Bucle para generar turnos diarios
			WHILE @fecha_actual <= @fecha_fin
			BEGIN
				DECLARE @hora_inicio TIME, @hora_fin TIME;
				SET @hora_inicio = @hora_apertura;
        
				-- Generar turnos para el día actual
				WHILE @hora_inicio < @hora_cierre
				BEGIN
					SET @hora_fin = DATEADD(MINUTE, @duracion, @hora_inicio);
            
					-- Insertar turno en la tabla de turnos
					INSERT INTO turnos (id_servicio, fecha_turno, hora_inicio, hora_fin, estado)
					VALUES (@id_servicio, @fecha_actual, @hora_inicio, @hora_fin, 1);
            
					SET @hora_inicio = @hora_fin;
				END;
        
				SET @fecha_actual = DATEADD(DAY, 1, @fecha_actual);
			END;

			COMMIT TRANSACTION;
    
			-- Devolver los turnos generados
			SELECT *  FROM turnos  WHERE id_servicio = @id_servicio AND fecha_turno BETWEEN @fecha_inicio AND @fecha_fin AND estado = 1;
	END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;        
    END CATCH;
END;
GO

SET IDENTITY_INSERT [dbo].[comercios] ON 
GO
INSERT [dbo].[comercios] ([id_comercio], [nom_comercio], [aforo_maximo]) VALUES (1, N'Caribe Plaza', 100)
GO
INSERT [dbo].[comercios] ([id_comercio], [nom_comercio], [aforo_maximo]) VALUES (2, N'La Castellana', 80)
GO
SET IDENTITY_INSERT [dbo].[comercios] OFF
GO
SET IDENTITY_INSERT [dbo].[servicios] ON 
GO
INSERT [dbo].[servicios] ([id_servicio], [id_comercio], [nom_servicio], [hora_apertura], [hora_cierre], [duracion]) VALUES (1, 1, N'Monica Cruz', CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), 60)
GO
INSERT [dbo].[servicios] ([id_servicio], [id_comercio], [nom_servicio], [hora_apertura], [hora_cierre], [duracion]) VALUES (2, 1, N'Panamericana', CAST(N'08:30:00' AS Time), CAST(N'18:30:00' AS Time), 30)
GO
SET IDENTITY_INSERT [dbo].[servicios] OFF
GO
