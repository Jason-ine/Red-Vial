USE [redvial]
GO

/****** Object:  StoredProcedure [dbo].[sp_InsertarOActualizarEstadisticas]    Script Date: 8/05/2025 14:06:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarOActualizarEstadisticas]
AS
BEGIN
    SET NOCOUNT ON;

    -- Agrupar datos desde DetalleSemaforo
    WITH DatosAgrupados AS (
        SELECT 
            NodoId,
            DireccionSemaforo,
            SUM(SumaCantidadEspera) AS SumaCantidadEspera,
            AVG(PromedioVehiculosPorCambio) AS PromedioVehiculosPorCambio,
            AVG(TiempoCrucePromedio) AS TiempoCrucePromedio,
            AVG(TiempoPromedioPorCarro) AS TiempoPromedioPorCarro,
            SUM(TotalCambios) AS TotalCambios
        FROM DetalleSemaforo
        GROUP BY NodoId, DireccionSemaforo
    )
    -- Iterar por cada grupo
    MERGE SemaforoEstadisticas AS target
    USING DatosAgrupados AS source
    ON target.NodoId = source.NodoId AND target.DireccionSemaforo = source.DireccionSemaforo
    WHEN MATCHED THEN
        UPDATE SET 
            SumaCantidadEspera = target.SumaCantidadEspera + source.SumaCantidadEspera,
            PromedioVehiculosPorCambio = (target.PromedioVehiculosPorCambio + source.PromedioVehiculosPorCambio) / 2,
            TiempoCrucePromedio = (target.TiempoCrucePromedio + source.TiempoCrucePromedio) / 2,
            TiempoPromedioPorCarro = (target.TiempoPromedioPorCarro + source.TiempoPromedioPorCarro) / 2,
            TotalCambios = target.TotalCambios + source.TotalCambios
    WHEN NOT MATCHED THEN
        INSERT (
            NodoId, DireccionSemaforo, SumaCantidadEspera,
            PromedioVehiculosPorCambio, TiempoCrucePromedio,
            TiempoPromedioPorCarro, TotalCambios
        )
        VALUES (
            source.NodoId, source.DireccionSemaforo, source.SumaCantidadEspera,
            source.PromedioVehiculosPorCambio, source.TiempoCrucePromedio,
            source.TiempoPromedioPorCarro, source.TotalCambios
        );
END
GO
USE [redvial]
GO

/****** Object:  StoredProcedure [dbo].[InterseccionesMasCongestionadas]    Script Date: 8/05/2025 14:29:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InterseccionesMasCongestionadas]
AS
BEGIN
    SELECT TOP 3
    NodoId,
    SUM(SumaCantidadEspera) AS TotalVehiculos,
    SUM(TotalCambios) AS TotalCambios,
    CASE 
        WHEN SUM(TotalCambios) = 0 THEN SUM(SumaCantidadEspera)
        ELSE CAST(SUM(SumaCantidadEspera) AS FLOAT) / SUM(TotalCambios)
    END AS IndicadorCongestion
FROM SemaforoEstadisticas
GROUP BY NodoId
ORDER BY IndicadorCongestion DESC;
END;
GO
USE [redvial]
GO

/****** Object:  StoredProcedure [dbo].[AnalisisCuelloBotella]    Script Date: 8/05/2025 14:29:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AnalisisCuelloBotella]
AS
BEGIN
    SELECT 
        NodoId,
        DireccionSemaforo,
        TotalCambios,
        SumaCantidadEspera,
        CASE 
            WHEN TotalCambios = 0 THEN SumaCantidadEspera
            ELSE CAST(SumaCantidadEspera AS FLOAT) / TotalCambios 
        END AS IndicadorCongestion
    FROM SemaforoEstadisticas
    ORDER BY IndicadorCongestion DESC;
END;
GO
USE [redvial]
GO

/****** Object:  Table [dbo].[DetalleSemaforo]    Script Date: 8/05/2025 14:29:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DetalleSemaforo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NodoId] [int] NOT NULL,
	[DireccionSemaforo] [nvarchar](100) NULL,
	[SumaCantidadEspera] [int] NOT NULL,
	[PromedioVehiculosPorCambio] [float] NOT NULL,
	[TiempoCrucePromedio] [float] NOT NULL,
	[TiempoPromedioPorCarro] [float] NOT NULL,
	[TotalCambios] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [redvial]
GO

/****** Object:  Table [dbo].[SemaforoEstadisticas]    Script Date: 8/05/2025 14:29:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SemaforoEstadisticas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NodoId] [int] NOT NULL,
	[DireccionSemaforo] [varchar](20) NOT NULL,
	[SumaCantidadEspera] [int] NOT NULL,
	[PromedioVehiculosPorCambio] [float] NOT NULL,
	[TiempoCrucePromedio] [float] NOT NULL,
	[TiempoPromedioPorCarro] [float] NOT NULL,
	[TotalCambios] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_NodoDireccion] UNIQUE NONCLUSTERED 
(
	[NodoId] ASC,
	[DireccionSemaforo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SemaforoEstadisticas] ADD  DEFAULT ((0)) FOR [SumaCantidadEspera]
GO

ALTER TABLE [dbo].[SemaforoEstadisticas] ADD  DEFAULT ((0)) FOR [PromedioVehiculosPorCambio]
GO

ALTER TABLE [dbo].[SemaforoEstadisticas] ADD  DEFAULT ((0)) FOR [TiempoCrucePromedio]
GO

ALTER TABLE [dbo].[SemaforoEstadisticas] ADD  DEFAULT ((0)) FOR [TiempoPromedioPorCarro]
GO

ALTER TABLE [dbo].[SemaforoEstadisticas] ADD  DEFAULT ((0)) FOR [TotalCambios]
GO

