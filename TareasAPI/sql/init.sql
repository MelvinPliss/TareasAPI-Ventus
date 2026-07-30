-- Crear la base de datos
CREATE DATABASE DBSistemaTareas;
GO

-- Usar la base de datos
USE DBSistemaTareas;
GO

-- Crear tabla de usuarios responsables
CREATE TABLE Usuarios (
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(150) UNIQUE NOT NULL
);
GO

-- Crear tabla de tareas
CREATE TABLE Tareas (
    TareaId INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL,
    Prioridad NVARCHAR(10) NOT NULL CHECK (Prioridad IN ('Alta','Media','Baja')),
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaInicio DATETIME NULL,
    FechaFinalizacion DATETIME NULL,
    FechaLimite DATETIME NULL,
    Estatus NVARCHAR(15) NOT NULL CHECK (Estatus IN ('Pendiente','En progreso','Terminada')),
    UsuarioId INT NOT NULL,
    CONSTRAINT FK_Tareas_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId)
);
GO

-- Crear índices recomendados
CREATE NONCLUSTERED INDEX IX_Tareas_UsuarioId
ON Tareas (UsuarioId);

CREATE NONCLUSTERED INDEX IX_Tareas_Estatus
ON Tareas (Estatus);

CREATE NONCLUSTERED INDEX IX_Tareas_Prioridad
ON Tareas (Prioridad);

CREATE NONCLUSTERED INDEX IX_Tareas_UsuarioId_Titulo
ON Tareas (UsuarioId, Titulo);

GO

-- Ejemplo de inserción de usuarios
INSERT INTO Usuarios (Nombre, Correo)
VALUES ('Juan Pérez', 'juan.perez@empresa.com'),
       ('María López', 'maria.lopez@empresa.com'),
       ('Carlos Ramírez', 'carlos.ramirez@empresa.com'),
       ('Ana Torres', 'ana.torres@empresa.com'),
       ('Luis Hernández', 'luis.hernandez@empresa.com'),
       ('Sofía Martínez', 'sofia.martinez@empresa.com'),
       ('Pedro González', 'pedro.gonzalez@empresa.com'),
       ('Laura Fernández', 'laura.fernandez@empresa.com'),
       ('Miguel Castro', 'miguel.castro@empresa.com'),
       ('Valeria Díaz', 'valeria.diaz@empresa.com');
GO

-- Ejemplo de inserción de tareas

INSERT INTO Tareas (Titulo, Descripcion, Prioridad, FechaLimite, Estatus, UsuarioId, FechaInicio, FechaFinalizacion)
VALUES 
('Organizar reunión de equipo', 'Coordinar agenda y enviar invitaciones', 'Alta', '2026-08-15', 'Pendiente', 3, '2026-08-01', '2026-08-10'),
('Revisar presupuesto trimestral', 'Analizar gastos y proponer ajustes', 'Alta', '2026-08-20', 'Pendiente', 4, '2026-08-02', '2026-08-14'),
('Capacitación en nuevas herramientas', 'Preparar sesión de entrenamiento para el personal', 'Media', '2026-08-25', 'Pendiente', 5, '2026-08-03', '2026-08-18'),
('Diseñar campaña de marketing', 'Crear materiales para la campaña digital', 'Alta', '2026-08-30', 'Pendiente', 6, '2026-08-04', '2026-08-22'),
('Evaluar proveedores', 'Comparar propuestas y seleccionar al mejor proveedor', 'Media', '2026-09-05', 'Pendiente', 7, '2026-08-05', '2026-08-28'),
('Actualizar sitio web', 'Modificar contenido y mejorar diseño', 'Alta', '2026-09-10', 'Pendiente', 8, '2026-08-06', '2026-09-02'),
('Planificar evento corporativo', 'Definir logística y coordinar con organizadores', 'Alta', '2026-09-15', 'Pendiente', 9, '2026-08-07', '2026-09-07'),
('Auditoría interna', 'Revisar procesos y cumplimiento de normas', 'Alta', '2026-09-20', 'Pendiente', 10, '2026-08-08', '2026-09-12'),
('Redactar boletín informativo', 'Preparar contenido para clientes y socios', 'Media', '2026-09-25', 'Pendiente', 1, '2026-08-09', '2026-09-17'),
('Optimizar procesos de ventas', 'Analizar flujo de trabajo y proponer mejoras', 'Alta', '2026-09-30', 'Pendiente', 2, '2026-08-10', '2026-09-22');
GO



CREATE PROCEDURE SP_GetPendingTasks
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.UsuarioId AS Id,
        u.Nombre AS Usuario,
        COUNT(CASE WHEN t.Estatus = 'Pendiente' THEN 1 END) AS TotalPendientes,
        COUNT(CASE WHEN t.Estatus = 'Pendiente' AND t.FechaLimite < GETDATE() THEN 1 END) AS TotalVencidas
    FROM Tareas t
    INNER JOIN Usuarios u ON t.UsuarioId = u.UsuarioId
    GROUP BY u.UsuarioId, u.Nombre;
END;
GO
