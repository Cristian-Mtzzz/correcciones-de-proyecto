

create database reposuper
go

use reposuper
go

set nocount on

-- Tabla DecanoFacultad
create table DecanoFacultad (
    codigo_facu varchar(6),
    nombre_facultad varchar(30) primary key
);
go

-- Tabla empleados (facultad debe tener el mismo tipo que nombre_facultad)
create table empleados (
    codigo_empleado int primary key,
    facultad varchar(30) foreign key references DecanoFacultad(nombre_facultad),
    primer_nombre varchar(15),
    segundo_nombre varchar(15),
    primer_apellido varchar(15),
    segundo_apellido varchar(15),
    rol varchar(25),
    usuario varchar(25),
    contraseña varchar(25)
)
go

-- Tabla Periodo
create table Periodo (
    ID_Periodo int identity primary key,
    fechaInicio date,
    fechaFin date
)
go

-- Tabla Clases
create table Clases (
    cod_Asignatura varchar(7) primary key,
    Facultad varchar(30) foreign key references DecanoFacultad(nombre_facultad),
    asignatura varchar(70),
    edificio char,
    aula varchar(25),
    seccion varchar(10),
    inicioDia date,     
    finDia date,
    diasPermitidos INT
)
go

-- Tabla Asistencia corregida
create table Asistencia (
    ID_Asistencia int identity primary key,
    cod_Asignatura varchar(7) foreign key references Clases(cod_Asignatura),
    ID_Empleado int foreign key references empleados(codigo_empleado),
    Fecha date,
    Observacion nvarchar(150), 
    Fecha_Reposicion date,
    Presente bit
)
go


--inserts de prueba 
insert into DecanoFacultad (codigo_facu, nombre_facultad)
values 
('F001', 'Ciencias de la computación'),
('F002', 'Medicina'),
('F003', 'Gestión de Empresas'),
('F00', 'Derecho');
go

INSERT INTO empleados (codigo_empleado, facultad, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, rol, usuario, contraseña)
VALUES
(1001, 'Ciencias de la computación', 'Carlos', 'Eduardo', 'Ramírez', 'González', 'docente', 'carlosr', 'contraseña123'),
(1002, 'Medicina', 'Ana', 'María', 'López', 'Pérez', 'Decano', 'analp', 'contraseña123'),
(1003, 'Gestión de Empresas', 'Luis', 'Fernando', 'Martínez', 'Hernández', 'Supervisor', 'luism', 'contraseña123'),
(1004, 'Derecho', 'Juan', 'Carlos', 'Gómez', 'Ramírez', 'Administrador', 'juang', 'contraseña123');

go
/*
INSERT INTO Periodo (fechaInicio, fechaFin)
VALUES 
('2025-01-01', '2025-06-30'),
('2025-07-01', '2025-12-31');
go*/

INSERT INTO Clases (cod_Asignatura, Facultad, asignatura, edificio, aula, seccion, inicioDia, finDia, diasPermitidos)
VALUES 
('CS101', 'Ciencias de la computación', 'Matemáticas I', 'A', '101', '0902', '2025-01-20', '2025-04-12', 5),
('MED102', 'Medicina', 'Anatomía Humana', 'B', '202', '1201', '2025-01-20', '2025-04-12', 5),
('GE301', 'Gestión de Empresas', 'Fundamentos de Administración', 'C', '303', '1202', '2025-01-20', '2025-04-12', 5),
('DR404', 'Derecho', 'Derecho Constitucional', 'D', '404', '1001', '2025-01-20', '2025-04-12', 5);
go


select *
from Clases



--PROCEDIMIENTOS ALMACENADOS

--insertar asistencia

CREATE PROCEDURE sp_InsertarAsistencia
    @cod_Asignatura VARCHAR(7),
    @ID_Empleado INT,
    @Fecha DATE,
    @Observacion NVARCHAR(150),
    @Fecha_Reposicion DATE,
    @Presente BIT
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Asistencia (cod_Asignatura, ID_Empleado, Fecha, Observacion, Fecha_Reposicion, Presente)
    VALUES (@cod_Asignatura, @ID_Empleado, @Fecha, @Observacion, @Fecha_Reposicion, @Presente);
END;
GO

--LOGIN
ALTER PROCEDURE PA_Login
    @Usuario VARCHAR(25),
    @Contrasena VARCHAR(25)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT primer_nombre, primer_apellido, rol
    FROM empleados
    WHERE usuario = @Usuario 
    AND BINARY_CHECKSUM(contraseña) = BINARY_CHECKSUM(@Contrasena);
END;

GO

--contra

CREATE PROCEDURE PA_Contra
    @Usuario VARCHAR(25),         -- Usamos VARCHAR(25) para coincidir con el campo 'usuario' en la tabla empleados
    @Contraseña VARCHAR(25)  -- También, ajustamos el tamaño de la contraseña para que coincida con la base de datos
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE empleados
    SET contraseña = @Contraseña
    WHERE usuario = @Usuario;   -- Aquí utilizamos el campo 'usuario' para identificar al empleado

    IF @@ROWCOUNT = 0  -- Verifica si no se actualizó ninguna fila
        SELECT 0 AS Resultado;  -- Usuario no encontrado
    ELSE 
        SELECT 1 AS Resultado;  -- Actualización exitosa
END;
GO


--admin save

CREATE PROCEDURE PA_Admin_Save
    @Usuario VARCHAR(25)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 
        FROM empleados 
        WHERE usuario = @Usuario 
        AND rol = 'Administrador'
    )
        SELECT 1 AS EsAdmin;
    ELSE
        SELECT 0 AS EsAdmin;
END;
GO


--supervisor

ALTER PROCEDURE PA_Supervisor
WITH ENCRYPTION
AS
BEGIN
    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);
    DECLARE @AHora CHAR(2) = RIGHT('0' + CAST(DATEPART(HOUR, GETDATE()) AS VARCHAR(2)), 2);

    SET DATEFIRST 1; -- Lunes como primer día de la semana
    DECLARE @DiaSemana INT = DATEPART(WEEKDAY, GETDATE()); -- CORREGIDO: ahora es INT
    DECLARE @BitDiaSemana INT = POWER(2, @DiaSemana - 1); -- Sigue funcionando bien

    SELECT DISTINCT 
        (E.primer_nombre + ' ' + ISNULL(E.segundo_nombre, '') + ' ' + E.primer_apellido + ' ' + ISNULL(E.segundo_apellido, '')) AS Docente,
        C.asignatura,
        C.seccion,
        C.aula,
        C.edificio,
        CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM Asistencia A2
                WHERE A2.ID_Empleado = E.codigo_empleado
                  AND A2.cod_Asignatura = C.cod_Asignatura
                  AND CAST(A2.Fecha AS DATE) = @Hoy
                  AND A2.Presente = 1
            ) THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS AsistenciaHoy
    FROM Clases C
    JOIN Asistencia A ON A.cod_Asignatura = C.cod_Asignatura
    JOIN empleados E ON A.ID_Empleado = E.codigo_empleado
    WHERE 
        E.codigo_empleado != '037'
        AND LEFT(LTRIM(RTRIM(C.seccion)), 2) = @AHora
        AND (
            (@DiaSemana BETWEEN C.inicioDia AND C.finDia)
            OR
            (C.diasPermitidos IS NOT NULL AND (C.diasPermitidos & @BitDiaSemana) > 0)
        );
END;
GO




ALTER PROCEDURE PA_Buscar_Superv
    @Docente VARCHAR(50),
    @Clase VARCHAR(70),
    @Aula VARCHAR(25),
    @Edificio CHAR(1),
    @Seccion VARCHAR(25) -- Agregar el parámetro
AS
BEGIN
    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);
    DECLARE @AHora CHAR(2) = RIGHT('0' + CAST(DATEPART(HOUR, GETDATE()) AS VARCHAR(2)), 2);

    SET DATEFIRST 1; -- Configura lunes como primer día de la semana
    DECLARE @DiaSemana TINYINT = DATEPART(WEEKDAY, GETDATE());
    DECLARE @BitDiaSemana INT = POWER(2, @DiaSemana - 1);

    SET NOCOUNT ON;

    SELECT DISTINCT 
        (primer_nombre + ' ' + ISNULL(segundo_nombre, '') + ' ' + primer_apellido + ' ' + ISNULL(segundo_apellido, '')) AS Docente,
        asignatura, aula, edificio,
        CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM Asistencia A2
                WHERE A2.ID_Empleado = A.ID_Empleado
                  AND A2.cod_Asignatura = C.cod_Asignatura
                  AND CAST(A2.Fecha AS DATE) = @Hoy
                  AND A2.Presente = 1
            ) THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS AsistenciaHoy
    FROM Asistencia A
    JOIN empleados E ON A.ID_Empleado = E.codigo_empleado
    JOIN Clases C ON A.cod_Asignatura = C.cod_Asignatura
    WHERE ((primer_nombre + ' ' + ISNULL(segundo_nombre, '') + ' ' + primer_apellido + ' ' + ISNULL(segundo_apellido, '')) LIKE '%' + @Docente + '%' OR @Docente = '')
      AND (asignatura LIKE '%' + @Clase + '%' OR @Clase = '')
      AND (@Aula = '' OR aula = @Aula)
      AND (@Edificio = '' OR edificio = @Edificio)
      AND (@Seccion = '' OR C.Seccion = @Seccion); -- Agregar filtro por sección
END


GO

CREATE PROCEDURE PA_Periodo
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1 fechaInicio, fechaFin
    FROM Periodo;
END
GO

CREATE PROCEDURE PA_Admin
WITH ENCRYPTION
AS
BEGIN
    SELECT DISTINCT 
        (Facultad + ' - ' + cod_Asignatura) AS [Referencia],
        asignatura,
        seccion,
        (edificio + ' - ' + aula) AS [Aula],
        (codigo_empleado + ' - ' + primer_nombre + ' ' + ISNULL(segundo_nombre, '') + ' ' + primer_apellido + ' ' + ISNULL(segundo_apellido, '')) AS [Empleado]
    FROM Asistencia A
    JOIN Clases C ON A.cod_Asignatura = C.cod_Asignatura
    JOIN empleados E ON A.ID_Empleado = E.codigo_empleado;
END
GO


CREATE PROCEDURE PA_Asistencia_Admin
    @Referencia VARCHAR(16),
    @Curso VARCHAR(70),
    @Seccion VARCHAR(7),
    @Aula VARCHAR(29),
    @Empleado VARCHAR(38)
WITH ENCRYPTION
AS
BEGIN
    DECLARE @ID_Empleado INT;
    SELECT @ID_Empleado = codigo_empleado
    FROM empleados
    WHERE (codigo_empleado + ' - ' + primer_nombre + ' ' + ISNULL(segundo_nombre, '') + ' ' + primer_apellido + ' ' + ISNULL(segundo_apellido, '')) = @Empleado;

    DECLARE @cod_Asignatura VARCHAR(7);
    SELECT @cod_Asignatura = cod_Asignatura
    FROM Clases
    WHERE (Facultad + ' - ' + cod_Asignatura) = @Referencia AND asignatura = @Curso;

    SELECT Fecha
    FROM Asistencia
    WHERE Presente = 1 AND cod_Asignatura = @cod_Asignatura AND ID_Empleado = @ID_Empleado;
END;
GO


CREATE PROCEDURE PA_Justifica
    @CodigoDecano VARCHAR(4)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @codigo_facu VARCHAR(6);

    -- Obtener el código de la facultad del decano
    SELECT @codigo_facu = codigo_facu
    FROM DecanoFacultad DF
    JOIN empleados E ON DF.nombre_facultad = E.facultad
    WHERE codigo_empleado = @CodigoDecano;

    -- Obtener asistencias no justificadas de docentes dentro de esa facultad
    SELECT DISTINCT 
           ID_Asistencia,
           asignatura,
           Fecha AS [Fecha de Ausencia],
           (E.primer_nombre + ' ' + ISNULL(E.segundo_nombre, '') + ' ' + E.primer_apellido + ' ' + ISNULL(E.segundo_apellido, '')) AS [Docente],
           seccion,
           Observacion AS [Justificación]
    FROM Asistencia A
    JOIN Clases C ON A.cod_Asignatura = C.cod_Asignatura
    JOIN empleados E ON A.ID_Empleado = E.codigo_empleado
    WHERE C.Facultad = @codigo_facu  
      AND A.Presente = 0;
END;
GO


CREATE PROCEDURE PA_Insertar_Justificacion
    @ID_Asistencia INT,
    @Justificacion NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Asistencia
    SET Observacion = @Justificacion
    WHERE ID_Asistencia = @ID_Asistencia;
END;
GO




CREATE PROCEDURE PA_Buscar_Justo
    @Docente VARCHAR(50),
    @Edificio CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT 
          ID_Asistencia,
          asignatura,
          Fecha AS 'Fecha de Ausencia',
          seccion,
          (E.primer_nombre + ' ' + ISNULL(E.segundo_nombre, '') + ' ' + E.primer_apellido + ' ' + ISNULL(E.segundo_apellido, '')) AS Docente, 
          aula, edificio, Observacion
    FROM Asistencia A
    JOIN empleados E ON A.ID_Empleado = E.codigo_empleado
    JOIN Clases C ON A.cod_Asignatura = C.cod_Asignatura
    WHERE A.Presente = 0
      AND ((E.primer_nombre + ' ' + ISNULL(E.segundo_nombre, '') + ' ' + E.primer_apellido + ' ' + ISNULL(E.segundo_apellido, '')) LIKE '%' + @Docente + '%' OR @Docente = '')
      AND (@Edificio = '' OR edificio = @Edificio);
END;
GO


CREATE PROCEDURE PA_Repone
    @CodigoDecano VARCHAR(4)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @codigo_facu VARCHAR(6);

    -- Obtener el código de facultad del decano
    SELECT @codigo_facu = codigo_facu
    FROM DecanoFacultad DF
    JOIN empleados E ON DF.nombre_facultad = E.facultad
    WHERE codigo_empleado = @CodigoDecano;

    -- Obtener ausencias con fecha de reposición
    SELECT DISTINCT 
           ID_Asistencia,
           asignatura,
           Fecha AS [Fecha de Ausencia],
           (E.primer_nombre + ' ' + ISNULL(E.segundo_nombre, '') + ' ' + E.primer_apellido + ' ' + ISNULL(E.segundo_apellido, '')) AS [Docente],
           seccion,
           Fecha_Reposicion AS 'Fecha de Reposición'
    FROM Asistencia A
    JOIN Clases C ON A.cod_Asignatura = C.cod_Asignatura
    JOIN empleados E ON A.ID_Empleado = E.codigo_empleado
    WHERE C.Facultad = @codigo_facu  
      AND A.Presente = 0;
END;
GO


CREATE PROCEDURE PA_Insertar_Reposicion
    @ID_Asistencia INT,
    @Fecha_Reposicion DATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Asistencia
    SET Fecha_Reposicion = @Fecha_Reposicion
    WHERE ID_Asistencia = @ID_Asistencia;
END;
GO


CREATE PROCEDURE PA_Buscar_Repo
    @Repo VARCHAR(80),
    @Edificio CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT 
        ID_Asistencia, asignatura, Fecha AS 'Fecha de Ausencia', seccion,
        (E.primer_nombre + ' ' + ISNULL(E.segundo_nombre, '') + ' ' + E.primer_apellido + ' ' + ISNULL(E.segundo_apellido, '')) AS Docente,
        Fecha_Reposicion
    FROM Asistencia A
    JOIN empleados E ON A.ID_Empleado = E.codigo_empleado
    JOIN Clases C ON A.cod_Asignatura = C.cod_Asignatura
    WHERE A.Presente = 0
      AND ((@Repo = ''  
      OR ((E.primer_nombre + ' ' + ISNULL(E.segundo_nombre, '') + ' ' + E.primer_apellido + ' ' + ISNULL(E.segundo_apellido, '')) LIKE '%' + @Repo + '%')
      OR (asignatura LIKE '%' + @Repo + '%')
      OR (Fecha LIKE '%' + @Repo + '%')
      OR (seccion LIKE '%' + @Repo + '%'))
      AND (@Edificio = '' OR edificio = @Edificio));
END;
GO



CREATE PROCEDURE PA_Asistencia_Doc
    @CodigoDocente INT
WITH ENCRYPTION
AS
BEGIN
    SELECT DISTINCT 
        asignatura, 
        seccion,
        aula,
        edificio
    FROM Asistencia A
    JOIN Clases C ON A.cod_Asignatura = C.cod_Asignatura
    JOIN empleados E ON A.ID_Empleado = E.codigo_empleado
    WHERE E.codigo_empleado = @CodigoDocente;
END;
GO




CREATE PROCEDURE PA_Fecha_Doc
    @CodDocente INT,
    @Asigna VARCHAR(70),
    @Seccion VARCHAR(7),
    @Aula VARCHAR(25),
    @Edificio CHAR(1)
WITH ENCRYPTION
AS
BEGIN
    -- Obtener ID del empleado
    DECLARE @ID_Empleado INT;
    SELECT @ID_Empleado = codigo_empleado
    FROM empleados
    WHERE codigo_empleado = @CodDocente;

    -- Obtener cod_Asignatura
    DECLARE @cod_Asignatura VARCHAR(7);
    SELECT @cod_Asignatura = cod_Asignatura
    FROM Clases
    WHERE asignatura = @Asigna AND seccion = @Seccion AND aula = @Aula AND edificio = @Edificio;

    -- Consultar fechas de asistencia
    SELECT Fecha
    FROM Asistencia
    WHERE Presente = 1 AND cod_Asignatura = @cod_Asignatura AND ID_Empleado = @ID_Empleado;
END;
GO

CREATE TRIGGER TGR_AdminContra
ON empleados
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si hay un administrador actualizado
    IF EXISTS (SELECT 1 FROM inserted WHERE rol = 'administrador')
    BEGIN
        -- Actualizar la contraseña del administrador
        UPDATE empleados
        SET contraseña = (SELECT contraseña FROM inserted WHERE rol = 'administrador')
        WHERE rol = 'administrador';
    END;
END;
GO


CREATE TRIGGER TRG_Periodo
ON Periodo
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Eliminar cualquier registro previo en la tabla Periodo
    DELETE FROM Periodo;

    -- Insertar el nuevo período desde la tabla temporal `inserted`
    INSERT INTO Periodo (fechaInicio, fechaFin)
    SELECT fechaInicio, fechaFin
    FROM inserted;
END;
GO