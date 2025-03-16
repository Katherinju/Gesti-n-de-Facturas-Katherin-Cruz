-- Verifica si la base de datos ya existe antes de crearla
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'GestionFacturasDB')
BEGIN
    CREATE DATABASE GestionFacturasDB;
    PRINT 'Base de datos GestionFacturasDB creada exitosamente.';
END
ELSE
    PRINT 'La base de datos GestionFacturasDB ya existe.';

-- Usar la base de datos antes de crear tablas
USE GestionFacturasDB;

-- Verificar si la tabla ya existe antes de crearla
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Facturas')
BEGIN
    CREATE TABLE Facturas (
        Id INT PRIMARY KEY IDENTITY(1,1),
        NumeroFactura NVARCHAR(50) NOT NULL UNIQUE, -- Asegura que no haya facturas duplicadas
        FechaVencimiento DATE NOT NULL CHECK (FechaVencimiento >= GETDATE()) -- Evita fechas pasadas
    );
    PRINT 'Tabla Facturas creada exitosamente.';
END
ELSE
    PRINT 'La tabla Facturas ya existe.';
