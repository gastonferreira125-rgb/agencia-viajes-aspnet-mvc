USE master
GO

IF EXISTS(Select * FROM SysDataBases WHERE name='PaquetesViaje')
BEGIN
	DROP DATABASE PaquetesViaje
END
go

CREATE DATABASE PaquetesViaje
GO

USE PaquetesViaje
GO
--------------------------CREACION DE TABLAS-------------------------
 
CREATE TABLE Estados (
    CodigoEstado VARCHAR(4) PRIMARY KEY,--el codigo de Estado debe tener 4 caracteres
    NombreEstado VARCHAR(50) NOT NULL,
    Pais VARCHAR(50) NOT NULL
)
GO
 
CREATE TABLE Servicios (
    NumeroOmnibus INT NOT NULL,
    FechaHoraPartida DATETIME NOT NULL,
    FechaHoraLlegada DATETIME NOT NULL,
    PrecioPasajeIndividual decimal(10,2) NOT NULL,
    PRIMARY KEY (NumeroOmnibus, FechaHoraPartida)--aca es una pk compuesta
)
GO
 
CREATE TABLE Hospedajes (
    CodigoInterno VARCHAR(10) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Direccion VARCHAR(150) NOT NULL,
    TipoHospedaje VARCHAR(20) NOT NULL,
    PrecioPorNoche DECIMAL(10,2) NOT NULL, 
    CodigoEstado VARCHAR(4) NOT NULL FOREIGN KEY REFERENCES Estados(CodigoEstado)
)
GO
 
CREATE TABLE PaquetesViaje (
    IdPaquete INT IDENTITY(1,1) PRIMARY KEY,--este es autogenerado
    Titulo VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(300) NOT NULL,
    CodigoEstado varchar(4) NOT NULL FOREIGN KEY REFERENCES Estados(CodigoEstado),
    CantidadDias INT NOT NULL, 
    PrecioIndividual DECIMAL(10,2) NOT NULL, 
    PrecioBaseDoble DECIMAL(10,2) NOT NULL, 
    PrecioBaseTriple DECIMAL(10,2) NOT NULL, 
    NumeroOmnibus INT NOT NULL,
    FechaHoraPartidaServicio DATETIME NOT NULL,
    CodigoInternoHospedaje VARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Hospedajes(CodigoInterno),
    CantidadNochesAlojamiento INT NOT NULL,
    FOREIGN KEY (NumeroOmnibus, FechaHoraPartidaServicio) REFERENCES Servicios(NumeroOmnibus, FechaHoraPartida)--fk compuesta
)
GO

-------------------------DATOS DE PRUEBA----------------------------

INSERT Estados VALUES ('MARA', 'Maragogi', 'Brasil')
INSERT Estados VALUES ('RNEG', 'Rio Negro', 'Argentina')
INSERT Estados VALUES ('FLOR', 'Florianopolis', 'Brasil')
INSERT Estados VALUES ('BSAS', 'Buenos Aires', 'Argentina')
INSERT Estados VALUES ('MEND', 'Mendoza', 'Argentina')
INSERT Estados VALUES ('SAOP', 'Sao Paulo', 'Brasil')
INSERT Estados VALUES ('SALT', 'Salta', 'Argentina')
INSERT Estados VALUES ('RIOJ', 'Rio de Janeiro', 'Brasil')
INSERT Estados VALUES ('CORD', 'Cordoba', 'Argentina')
INSERT Estados VALUES ('BAHA', 'Bahia', 'Brasil')
GO
 
INSERT Servicios VALUES (15, '20260512 08:00', '20260526 08:00', 380.00)
INSERT Servicios VALUES (22, '20261005 09:00', '20261019 09:00', 410.00)
INSERT Servicios VALUES (7,  '20260915 07:30', '20260922 07:30', 250.00)
INSERT Servicios VALUES (33, '20261101 10:00', '20261108 10:00', 300.00)
INSERT Servicios VALUES (44, '20261210 06:00', '20261220 06:00', 450.00)
INSERT Servicios VALUES (5,  '20260920 08:00', '20260925 08:00', 180.00)
INSERT Servicios VALUES (61, '20260801 07:00', '20260810 07:00', 320.00)
INSERT Servicios VALUES (18, '20270105 09:00', '20270115 09:00', 500.00)
INSERT Servicios VALUES (29, '20261020 08:30', '20261102 08:30', 480.00)
INSERT Servicios VALUES (50, '20260905 06:00', '20260912 06:00', 220.00)
INSERT Servicios VALUES (12, '20260701 07:00', '20260708 07:00', 260.00)
GO
 
INSERT Hospedajes VALUES ('NAHP01',    'Nahuel Huapi Hostel',      'Av. Bustillo 1234, Bariloche',  'Hotel STD',     150.00, 'RNEG')
INSERT Hospedajes VALUES ('MARPAR01',  'Maragogi Paradise Resort', 'Rua da Praia 10, Maragogi',      'All Inclusive', 220.00, 'MARA')
INSERT Hospedajes VALUES ('FLORIST01', 'Floripa Stay Pousada',     'Av. Beira Mar 500, Floripa',     'Posada',         95.00, 'FLOR')
INSERT Hospedajes VALUES ('BSASHTL01', 'Recoleta Hotel BA',        'Av. Santa Fe 1500, CABA',        'Hotel STD',     180.00, 'BSAS')
INSERT Hospedajes VALUES ('MZAWINE01', 'Mendoza Wine Lodge',       'Ruta 40 km 5, Mendoza',          'All Inclusive', 260.00, 'MEND')
INSERT Hospedajes VALUES ('SPAUL0001', 'Sao Paulo City Hotel',     'Av. Paulista 900, SP',           'Hotel STD',     200.00, 'SAOP')
INSERT Hospedajes VALUES ('SALTAPO01', 'Salta Colonial Posada',    'Calle Caseros 300, Salta',       'Posada',        110.00, 'SALT')
INSERT Hospedajes VALUES ('RIODEJ001', 'Copacabana Suites',        'Av. Atlantica 200, Rio',         'All Inclusive', 300.00, 'RIOJ')
INSERT Hospedajes VALUES ('CORDHTL01', 'Cordoba Centro Hotel',     'Av. Colon 700, Cordoba',         'Hotel STD',     140.00, 'CORD')
INSERT Hospedajes VALUES ('BAHIAPO01', 'Salvador Bahia Posada',    'Rua do Sol 45, Salvador',        'Posada',        130.00, 'BAHA')
GO
 
-- para estos datos d eprueba lo hacemos tal cul pide la letra "El precio base es la suma del precio del servicio seleccionado, más el precio del hospedaje incluido (cantidad de noches * precio individual)
--"A este resultado se lo debe aumentar un 35% para obtener el precio individual.

--El precio en base doble (2 personas) y el precio en base triple (3 personas), se obtienen multiplicando el precio base 
--por la cantidad de personas, y aumentando en un 10% el resultado"

--(PrecioBase = PrecioPasajeIndividual + Noches*PrecioPorNoche; Individual = Base*1.35; Doble = Base*2*1.10; Triple = Base*3*1.10)
INSERT PaquetesViaje VALUES ('Aventura en Bariloche',     'Recorre los paisajes de la Patagonia argentina',           'RNEG', 14, 2578.50, 4202.00, 6303.00, 22, '20261005 09:00', 'NAHP01',    10)
INSERT PaquetesViaje VALUES ('Playas de Maragogi',        'Sol, mar y aguas cristalinas en el nordeste brasileno',     'MARA', 14, 3780.00, 6160.00, 9240.00, 15, '20260512 08:00', 'MARPAR01',  11)
INSERT PaquetesViaje VALUES ('Florianopolis al Sol',      'Playas y vida nocturna en la isla de Santa Catarina',       'FLOR', 7,   978.75, 1595.00, 2392.50, 7,  '20260915 07:30', 'FLORIST01', 5)
INSERT PaquetesViaje VALUES ('Buenos Aires Cultural',     'Tango, arquitectura e historia portena',                    'BSAS', 7,  1377.00, 2244.00, 3366.00, 33, '20261101 10:00', 'BSASHTL01', 4)
INSERT PaquetesViaje VALUES ('Ruta del Vino Mendocina',   'Bodegas y montanas de Cuyo',                                'MEND', 10, 3064.50, 4994.00, 7491.00, 44, '20261210 06:00', 'MZAWINE01', 7)
INSERT PaquetesViaje VALUES ('Sao Paulo Urbano',          'La gran metropoli brasilena',                               'SAOP', 5,  1053.00, 1716.00, 2574.00, 5,  '20260920 08:00', 'SPAUL0001', 3)
INSERT PaquetesViaje VALUES ('Salta la Linda',            'Paisajes del noroeste argentino',                           'SALT', 9,  1323.00, 2156.00, 3234.00, 61, '20260801 07:00', 'SALTAPO01', 6)
INSERT PaquetesViaje VALUES ('Rio de Janeiro Full',       'Copacabana, Corcovado y Pan de Azucar',                     'RIOJ', 10, 3915.00, 6380.00, 9570.00, 18, '20270105 09:00', 'RIODEJ001', 8)
INSERT PaquetesViaje VALUES ('Cordoba Serrana',           'Sierras y tranquilidad cordobesa',                          'CORD', 13, 2349.00, 3828.00, 5742.00, 29, '20261020 08:30', 'CORDHTL01', 9)
INSERT PaquetesViaje VALUES ('Bahia Tropical',            'Cultura afrobrasilena y playas de Salvador',                'BAHA', 7,  1174.50, 1914.00, 2871.00, 50, '20260905 06:00', 'BAHIAPO01', 5)
INSERT PaquetesViaje VALUES ('Bariloche Express',         'Escapada corta a la Patagonia',                             'RNEG', 7,  1161.00, 1892.00, 2838.00, 12, '20260701 07:00', 'NAHP01',    4)
GO
 
 ------------------PROCEDIMIENTOS ALMACENADOS---------------------------

--CRUD DE ESTADOS--

CREATE PROC AltaEstado
@CodigoEstado VARCHAR(4),
@NombreEstado VARCHAR(50),
@Pais VARCHAR(50)
AS
BEGIN
	--validamos que no exista el codigo
	IF EXISTS(SELECT * FROM Estados WHERE CodigoEstado =@CodigoEstado)
	return -1

	INSERT Estados VALUES(@CodigoEstado, @NombreEstado, @Pais)

	IF @@ERROR <>0
		RETURN -2

	RETURN 1
END
GO

CREATE PROC ModificarEstado
@CodigoEstado VARCHAR(4),
@NombreEstado VARCHAR(50),
@Pais VARCHAR(50)
AS
BEGIN
	--VALIDAMOS QUE EXISTA EL ESTADO
	IF NOT EXISTS(SELECT * FROM Estados WHERE CodigoEstado =@CodigoEstado)
		RETURN -1

	UPDATE Estados
	SET NombreEstado =@NombreEstado, Pais =@Pais
	WHERE CodigoEstado =@CodigoEstado

	IF @@ERROR <>0
		RETURN -2

	RETURN 1
END
GO

CREATE PROC EliminarEstado
@CodigoEstado VARCHAR(4)
AS
BEGIN
	--VALIDAMOS QUE EXISTA EL ESTADO
	IF NOT EXISTS(SELECT * FROM Estados where CodigoEstado =@CodigoEstado)
		RETURN -1

	--VERIFICAMOS QUE NO TENGAN HOSPEDAJES ASOCIADOS
	IF EXISTS(SELECT * FROM Hospedajes WHERE CodigoEstado =@CodigoEstado)
		RETURN -2

	--VERIFICAMOS QUE NO TENGAN PAQUETES DE VIJE ASOCIADOS
	IF EXISTS(SELECT * FROM PaquetesViaje WHERE CodigoEstado =@CodigoEstado)
		RETURN -3

	DELETE FROM Estados WHERE CodigoEstado =@CodigoEstado

	IF @@ERROR <> 0
		RETURN -4

	RETURN 1
END
GO

CREATE PROC BuscarEstado
@CodigoEstado VARCHAR(4)
AS
BEGIN
	--VALIDAMOS QUE EXISTA EL ESTADO
	IF NOT EXISTS(SELECT * FROM Estados WHERE CodigoEstado = @CodigoEstado)
		RETURN -1

	SELECT CodigoEstado, NombreEstado, Pais
	from Estados
	where CodigoEstado =@CodigoEstado
end
go

CREATE PROC ListarEstados
AS
BEGIN
	SELECT CodigoEstado, NombreEstado, Pais
	FROM Estados
	ORDER BY NombreEstado
end
go

EXEC ListarEstados
GO

--CRUD DE SERVICIOS--

CREATE PROC AltaServicio --GF: corijo acá RO cambio Alata por ALTA
@NumeroOmnibus INT,--GF: cambio NombreOmnibus por NumeroOMnibus
@FechaHoraPartida datetime,
@FechaHoraLlegada datetime,
@PrecioPasajeIndividual decimal(10,2)
AS
BEGIN
	--NUMERICO DEL 1 AL 100
	IF @NumeroOmnibus < 1 OR @NumeroOmnibus >100
		RETURN -1

	--VALIDAMOS QUE LA LLEGADA SEA POSTERIOR A LA PARTIDA
	IF @FechaHoraLlegada <= @FechaHoraPartida
		RETURN -2

	--VALIDAMOS QUE NO EXISTA YA ESTE SERVICIO
	IF EXISTS(SELECT * FROM Servicios where NumeroOmnibus =@NumeroOmnibus and FechaHoraPartida =@FechaHoraPartida)
		return-3

	INSERT Servicios VALUES (@numeroOmnibus, @FechaHoraPartida, @FechaHoraLlegada, @PrecioPasajeIndividual)

	IF @@ERROR <>0
		RETURN -4

	RETURN 1
END
GO

USE PaquetesViaje
GO

CREATE PROC ModificarServicio --GF: corijo acá RO cambio ModificarUsuario por ModificarServicio
@NumeroOmnibus int,
@FechaHoraPartida datetime,
@FechaHoraLlegada datetime,
@PrecioPasajeIndividual decimal(10,2)
AS
BEGIN
	--VALIDAMOS QUE EXISTA EL SERVICIO
	IF NOT EXISTS(SELECT * FROM Servicios WHERE NumeroOmnibus = @NumeroOmnibus and FechaHoraPartida =@FechaHoraPartida)
		RETURN -1

	IF @FechaHoraLlegada <=@FechaHoraPartida
		RETURN -2

	UPDATE Servicios
	SET FechaHoraLlegada =@FechaHoraLlegada,
	PrecioPasajeIndividual =@PrecioPasajeIndividual
	WHERE NumeroOmnibus =@NumeroOmnibus AND FechaHoraPartida = @FechaHoraPartida

	IF @@ERROR <>0
		RETURN -3

	RETURN 1
END
GO

CREATE PROC EliminarServicio
@NumeroOmnibus int,
@FechaHoraPartida datetime
AS
BEGIN
	--VALIDAMOS QUE EXISTA EL SERVICIO
	IF NOT EXISTS(SELECT * FROM Servicios WHERE NumeroOmnibus =@NumeroOmnibus and FechaHoraPartida =@FechaHoraPartida)
		RETURN -1

	--VALIDAMOS QUE NO TENGAN PAQUETES ASOCIADOS
	--GF: corijo acá RO cambio FechaHoraPartida por FechaHoraPartidaServicio
	IF EXISTS(SELECT * FROM PaquetesViaje WHERE NumeroOmnibus =@NumeroOmnibus and FechaHoraPartidaServicio = @FechaHoraPartida)
		RETURN -2

	DELETE FROM Servicios WHERE NumeroOmnibus =@NumeroOmnibus and FechaHoraPartida =@FechaHoraPartida

	IF @@ERROR <>0
		RETURN -3

	RETURN 1
END
GO

CREATE PROC BuscarServicio
@NumeroOmnibus INT,
@FechaHoraPartida DATETIME
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM Servicios where NumeroOmnibus =@NumeroOmnibus and FechaHoraPartida =@FechaHoraPartida)
		RETURN -1

	SELECT NumeroOmnibus, FechaHoraPartida, FechaHoraLlegada, PrecioPasajeIndividual
	FROM Servicios
	where NumeroOmnibus =@NumeroOmnibus AND FechaHoraPartida =@FechaHoraPartida
END
GO

CREATE PROC ListarServicios
AS
BEGIN
	SELECT NumeroOmnibus,FechaHoraPartida, FechaHoraLlegada, PrecioPasajeIndividual
	FROM Servicios
	ORDER BY FechaHoraPartida
END
GO

EXEC ListarServicios
GO

--CRUD DE HOSPEDAJES--

CREATE PROC AltaHospedaje
@CodigoInterno VARCHAR(10),--HASTA 10LETRAS PIDE EL PROYECTO
@Nombre VARCHAR(100),
@Direccion VARCHAR(150),
@TipoHospedaje VARCHAR(20),
@PrecioPorNoche DECIMAL(10,2),
@CodigoEstado VARCHAR(4)
AS
BEGIN
	--VALIDAMOS QUE NO EXISTA EL COD INTERNO
	IF EXISTS(SELECT * FROM Hospedajes WHERE CodigoInterno =@CodigoInterno)
		RETURN -1

	--VALIDAMOS QUE EXISTA EL ESTADO
	--GF: corijo acá validaba contra la misma tabla de hospedajes pero debemos validar que exista
	--en la tabla Estados
	IF NOT EXISTS(SELECT * FROM Estados WHERE CodigoEstado =@CodigoEstado)
		RETURN -2

	--VALIDAMS QUE EL TIPO SEA UNO DE LOS TRES
	IF @TipoHospedaje NOT IN ('Hotel STD', 'Posada', 'All Inclusive')
		return -3

	INSERT Hospedajes VALUES (@CodigoInterno, @Nombre, @Direccion, @TipoHospedaje, @PrecioPorNoche, @CodigoEstado)
	IF @@ERROR <>0
		RETURN -4

	RETURN 1
END
GO

USE PaquetesViaje
GO

CREATE PROC ModificarHospedaje
@CodigoInterno VARCHAR(10),
@Nombre VARCHAR(100),
@Direccion VARCHAR(150),
@TipoHospedaje VARCHAR(20),
@PrecioPorNoche DECIMAL(10,2),
@CodigoEstado VARCHAR(4)
AS
BEGIN
	--validamos que exista el hospedaje
	IF NOT EXISTS (SELECT * FROM Hospedajes WHERE CodigoInterno = @CodigoInterno)
		RETURN -1
 
	--validamos que exista el estado
	IF NOT EXISTS (SELECT * FROM Estados WHERE CodigoEstado = @CodigoEstado)
		RETURN -2
 
	IF @TipoHospedaje NOT IN ('Hotel STD', 'Posada', 'All Inclusive')
		RETURN -3
 
	UPDATE Hospedajes
	SET Nombre = @Nombre,
		Direccion = @Direccion,
		TipoHospedaje = @TipoHospedaje,
		PrecioPorNoche = @PrecioPorNoche,
		CodigoEstado = @CodigoEstado
	WHERE CodigoInterno = @CodigoInterno
 
	IF @@ERROR <> 0
		RETURN -4
 
	RETURN 1
END
GO
 
CREATE PROC EliminarHospedaje
@CodigoInterno VARCHAR(10)
AS
BEGIN
	--validamos que exista el hospedaje
	IF NOT EXISTS (SELECT * FROM Hospedajes WHERE CodigoInterno =@CodigoInterno)
		RETURN -1
 
	--validamos que no tenga paquetes de viaje asociados
	IF EXISTS (SELECT * FROM PaquetesViaje WHERE CodigoInternoHospedaje =@CodigoInterno)
		RETURN -2
 
	DELETE FROM Hospedajes WHERE CodigoInterno =@CodigoInterno
 
	IF @@ERROR <> 0
		RETURN -3
 
	RETURN 1
END
GO
 
CREATE PROC BuscarHospedaje
@CodigoInterno VARCHAR(10)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Hospedajes WHERE CodigoInterno = @CodigoInterno)
		RETURN -1
 
	SELECT CodigoInterno, Nombre, Direccion, TipoHospedaje, PrecioPorNoche, CodigoEstado
	FROM Hospedajes
	WHERE CodigoInterno = @CodigoInterno
END
GO
 
CREATE PROC ListarHospedajes
AS
BEGIN
	SELECT CodigoInterno, Nombre, Direccion, TipoHospedaje, PrecioPorNoche, CodigoEstado
	FROM Hospedajes
	ORDER BY Nombre
END
GO

EXEC ListarHospedajes
GO

 
 --ALTA DE PAQUETES DE VIAJE
CREATE PROC AltaPaqueteViaje
@Titulo VARCHAR(100),
@Descripcion VARCHAR(300),
@CodigoEstado VARCHAR(4),
@CantidadDias INT,
@PrecioIndividual DECIMAL(10,2),
@PrecioBaseDoble DECIMAL(10,2),
@PrecioBaseTriple DECIMAL(10,2),
@NumeroOmnibus INT,
@FechaHoraPartidaServicio DATETIME,
@CodigoInternoHospedaje VARCHAR(10),
@CantidadNochesAlojamiento INT
AS
BEGIN
	DECLARE @IdPaquete INT
	DECLARE @CodigoEstadoHospedaje VARCHAR(4)

	--VALIDAMOS QUE EXISTA EL ESTADO
	IF NOT EXISTS (SELECT * FROM Estados WHERE CodigoEstado = @CodigoEstado)
		RETURN -1

	--validamos que exista el servicio
	IF NOT EXISTS (SELECT * FROM Servicios WHERE NumeroOmnibus = @NumeroOmnibus AND FechaHoraPartida = @FechaHoraPartidaServicio)
		RETURN -2

	--validamos que exista el hospedaje
	IF NOT EXISTS (SELECT * FROM Hospedajes WHERE CodigoInterno = @CodigoInternoHospedaje)
		RETURN -3

	SET @CodigoEstadoHospedaje = (SELECT CodigoEstado FROM Hospedajes WHERE CodigoInterno = @CodigoInternoHospedaje)

	--EL HOSPEDAJE TIENE QUE ESTAR EN EL MISMO ESTADO QUE EL PAQUETE(rne)
	IF @CodigoEstadoHospedaje <> @CodigoEstado
		RETURN -4

	--los dias, el precio base y las noches ya vienen calculados y validados desde la capa de Logica (clases Servicio, Hospedaje y PaqueteViaje)
	INSERT PaquetesViaje VALUES (@Titulo, @Descripcion, @CodigoEstado, @CantidadDias, @PrecioIndividual,
	@PrecioBaseDoble, @PrecioBaseTriple, @NumeroOmnibus, @FechaHoraPartidaServicio, @CodigoInternoHospedaje, @CantidadNochesAlojamiento)

	IF @@ERROR <>0 
		RETURN -5

	SET @IdPaquete = @@IDENTITY--devuelvo el id generado
	RETURN  @IdPaquete
end
go

USE PaquetesViaje
GO

DECLARE @RET INT
EXEC @RET = AltaPaqueteViaje
	'Ushuaia Fin del Mundo',
	'El destino mas austral de Argentina',
	'RNEG', 14, 2173.50, 3542.00, 5313.00, 22, '20261005 09:00', 'NAHP01', 8
PRINT @RET
GO

-----nos faltan los listados otro dia continuo

-- GF: creo los listados
CREATE PROC ListarPaquetesDisponibles
AS
BEGIN
    SELECT P.IdPaquete, P.Titulo, S.FechaHoraPartida, P.CantidadDias
    FROM PaquetesViaje P
    JOIN Servicios S ON P.NumeroOmnibus = S.NumeroOmnibus AND P.FechaHoraPartidaServicio = S.FechaHoraPartida
    WHERE S.FechaHoraPartida > GETDATE()
    ORDER BY S.FechaHoraPartida
END
GO

EXEC ListarPaquetesDisponibles
GO

CREATE PROC BuscarPaqueteViaje
@IdPaquete INT
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM PaquetesViaje WHERE IdPaquete = @IdPaquete)
        RETURN -1

    SELECT P.IdPaquete, P.Titulo, P.Descripcion, P.CantidadDias, P.PrecioIndividual, P.PrecioBaseDoble, P.PrecioBaseTriple,
        P.CantidadNochesAlojamiento, E.CodigoEstado,E.NombreEstado, E.Pais, S.NumeroOmnibus, S.FechaHoraPartida, S.FechaHoraLlegada,
        S.PrecioPasajeIndividual, H.CodigoInterno, H.Nombre, H.Direccion, H.TipoHospedaje,H.PrecioPorNoche

    FROM PaquetesViaje P
	JOIN Estados E ON P.CodigoEstado = E.CodigoEstado
	JOIN Servicios S ON P.NumeroOmnibus = S.NumeroOmnibus AND P.FechaHoraPartidaServicio = S.FechaHoraPartida
	JOIN Hospedajes H ON P.CodigoInternoHospedaje = H.CodigoInterno

	WHERE P.IdPaquete = @IdPaquete
END
GO

EXEC BuscarPaqueteViaje
    @IdPaquete = 1
GO

CREATE PROC ListarPaquetesPorEstado
@CodigoEstado VARCHAR(4)
AS
BEGIN
    SELECT P.IdPaquete, P.Titulo, P.Descripcion, P.CantidadDias, P.PrecioIndividual, P.PrecioBaseDoble,
           P.PrecioBaseTriple, P.CantidadNochesAlojamiento, P.NumeroOmnibus, P.FechaHoraPartidaServicio,
           P.CodigoInternoHospedaje
    FROM PaquetesViaje P
    WHERE P.CodigoEstado = @CodigoEstado
    ORDER BY P.Titulo
END
GO

EXEC ListarPaquetesPorEstado
    @CodigoEstado = 'RNEG'
GO


CREATE PROC ListarHospedajesPorEstado
@CodigoEstado VARCHAR(4)
AS
BEGIN
    SELECT CodigoInterno, Nombre, Direccion, TipoHospedaje, PrecioPorNoche, CodigoEstado
    FROM Hospedajes
    WHERE CodigoEstado = @CodigoEstado
    ORDER BY Nombre
END
GO

EXEC ListarHospedajesPorEstado
    @CodigoEstado = 'RNEG'
GO


--GF: creo un listado solamente con los servicios futuros, es decir, seleccionables
CREATE PROC ListarServiciosFuturos
AS
BEGIN
    SELECT NumeroOmnibus, FechaHoraPartida, FechaHoraLlegada, PrecioPasajeIndividual
    FROM Servicios
    WHERE FechaHoraPartida > GETDATE()
    ORDER BY FechaHoraPartida
END
GO

EXEC ListarServiciosFuturos
GO

CREATE PROC ListarPaquetesPorServicio
@NumeroOmnibus INT,
@FechaHoraPartida DATETIME
AS
BEGIN
    SELECT P.IdPaquete, P.Titulo, P.Descripcion, P.CantidadDias, P.PrecioIndividual,
           P.PrecioBaseDoble, P.PrecioBaseTriple, P.CantidadNochesAlojamiento,
           H.CodigoInterno, H.Nombre, H.Direccion, H.TipoHospedaje, H.PrecioPorNoche
    FROM PaquetesViaje P 
	--gaston viste que aca trae el cod no los datos del hospedaje falta un join con hosp
    JOIN Hospedajes H ON P.CodigoInternoHospedaje = H.CodigoInterno
    where P.NumeroOmnibus = @NumeroOmnibus and P.FechaHoraPartidaServicio = @FechaHoraPartida
    order by P.Titulo
END
GO
EXEC ListarPaquetesPorServicio
    @NumeroOmnibus = 22,
    @FechaHoraPartida = '20261005 09:00'
GO


