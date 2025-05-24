-- Script DDL para SQL Server
-- Versión: 2.0
-- Autor: bscl
-- Fecha: 24/05/2025
-- Descripción: Script actualizado para crear todas las tablas del sistema Encarte1817

-- Desactivar la verificación de claves foráneas durante la creación de tablas
SET NOCOUNT ON;
GO

-- Crear eliminar tablas existentes (en orden inverso de dependencias)
-- Tablas de relaciones
IF OBJECT_ID('dbo.RolFormPermissions', 'U') IS NOT NULL DROP TABLE dbo.RolFormPermissions;
IF OBJECT_ID('dbo.MenuPermissions', 'U') IS NOT NULL DROP TABLE dbo.MenuPermissions;
IF OBJECT_ID('dbo.ModulePermissions', 'U') IS NOT NULL DROP TABLE dbo.ModulePermissions;
IF OBJECT_ID('dbo.FormModules', 'U') IS NOT NULL DROP TABLE dbo.FormModules;
IF OBJECT_ID('dbo.RolUsers', 'U') IS NOT NULL DROP TABLE dbo.RolUsers;

-- Tablas de detalles
IF OBJECT_ID('dbo.Districts', 'U') IS NOT NULL DROP TABLE dbo.Districts;
IF OBJECT_ID('dbo.CodePostals', 'U') IS NOT NULL DROP TABLE dbo.CodePostals;

-- Tablas de datos personales
IF OBJECT_ID('dbo.Clients', 'U') IS NOT NULL DROP TABLE dbo.Clients;
IF OBJECT_ID('dbo.Employees', 'U') IS NOT NULL DROP TABLE dbo.Employees;
IF OBJECT_ID('dbo.Providers', 'U') IS NOT NULL DROP TABLE dbo.Providers;

-- Tablas geográficas
IF OBJECT_ID('dbo.Cities', 'U') IS NOT NULL DROP TABLE dbo.Cities;
IF OBJECT_ID('dbo.Departments', 'U') IS NOT NULL DROP TABLE dbo.Departments;
IF OBJECT_ID('dbo.Countries', 'U') IS NOT NULL DROP TABLE dbo.Countries;

-- Tablas de seguridad
IF OBJECT_ID('dbo.ChangeLogs', 'U') IS NOT NULL DROP TABLE dbo.ChangeLogs;
IF OBJECT_ID('dbo.Permissions', 'U') IS NOT NULL DROP TABLE dbo.Permissions;
IF OBJECT_ID('dbo.Forms', 'U') IS NOT NULL DROP TABLE dbo.Forms;
IF OBJECT_ID('dbo.Modules', 'U') IS NOT NULL DROP TABLE dbo.Modules;
IF OBJECT_ID('dbo.Menus', 'U') IS NOT NULL DROP TABLE dbo.Menus;
IF OBJECT_ID('dbo.Persons', 'U') IS NOT NULL DROP TABLE dbo.Persons;
IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL DROP TABLE dbo.Roles;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
GO

-- Crear tabla de Usuarios
CREATE TABLE dbo.Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL
);
GO

-- Crear índice para optimizar búsquedas por Email
CREATE UNIQUE INDEX IX_Users_Email ON dbo.Users(Email) WHERE Status = 1;
GO

-- Crear tabla de Personas
CREATE TABLE dbo.Persons (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    IdentificationType TINYINT NOT NULL,
    IdentificationNumber NVARCHAR(20) NOT NULL,
    BirthDate DATE NULL,
    Gender TINYINT NULL,
    PhoneNumber NVARCHAR(20) NULL,
    MobileNumber NVARCHAR(20) NULL,
    Email NVARCHAR(255) NULL,
    Address NVARCHAR(255) NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL
);
GO

-- Crear tabla de Roles
CREATE TABLE dbo.Roles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL
);
GO

-- Crear índice para optimizar búsquedas por Nombre de Rol
CREATE UNIQUE INDEX IX_Roles_Name ON dbo.Roles(Name) WHERE Status = 1;
GO

-- Crear tabla de relación entre Roles y Usuarios
CREATE TABLE dbo.RolUsers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RolId INT NOT NULL,
    UserId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_RolUsers_Roles FOREIGN KEY (RolId) REFERENCES dbo.Roles(Id),
    CONSTRAINT FK_RolUsers_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(Id)
);
GO

-- Crear tabla de Menús
CREATE TABLE dbo.Menus (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Icon NVARCHAR(50) NULL,
    Url NVARCHAR(255) NULL,
    Order INT NOT NULL DEFAULT 0,
    ParentMenuId INT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_Menus_ParentMenu FOREIGN KEY (ParentMenuId) REFERENCES dbo.Menus(Id)
);
GO

-- Crear tabla de Módulos
CREATE TABLE dbo.Modules (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    Icon NVARCHAR(50) NULL,
    Url NVARCHAR(255) NULL,
    Order INT NOT NULL DEFAULT 0,
    ParentModuleId INT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_Modules_ParentModule FOREIGN KEY (ParentModuleId) REFERENCES dbo.Modules(Id)
);
GO

-- Crear tabla de Formularios
CREATE TABLE dbo.Forms (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    Url NVARCHAR(255) NOT NULL,
    ModuleId INT NOT NULL,
    Order INT NOT NULL DEFAULT 0,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_Forms_Modules FOREIGN KEY (ModuleId) REFERENCES dbo.Modules(Id)
);
GO

-- Crear tabla de relación entre Formularios y Módulos
CREATE TABLE dbo.FormModules (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FormId INT NOT NULL,
    ModuleId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_FormModules_Forms FOREIGN KEY (FormId) REFERENCES dbo.Forms(Id),
    CONSTRAINT FK_FormModules_Modules FOREIGN KEY (ModuleId) REFERENCES dbo.Modules(Id),
    CONSTRAINT UQ_FormModules UNIQUE (FormId, ModuleId)
);
GO

-- Crear tabla de Permisos
CREATE TABLE dbo.Permissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    PermissionCode NVARCHAR(50) NOT NULL,
    Type TINYINT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL
);
GO

-- Crear índice único para PermissionCode
CREATE UNIQUE INDEX IX_Permissions_Code ON dbo.Permissions(PermissionCode) WHERE Status = 1;
GO

-- Crear tabla de relación entre Módulos y Permisos
CREATE TABLE dbo.ModulePermissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ModuleId INT NOT NULL,
    PermissionId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_ModulePermissions_Modules FOREIGN KEY (ModuleId) REFERENCES dbo.Modules(Id),
    CONSTRAINT FK_ModulePermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES dbo.Permissions(Id),
    CONSTRAINT UQ_ModulePermissions UNIQUE (ModuleId, PermissionId)
);
GO

-- Crear tabla de relación entre Menús y Permisos
CREATE TABLE dbo.MenuPermissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MenuId INT NOT NULL,
    PermissionId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_MenuPermissions_Menus FOREIGN KEY (MenuId) REFERENCES dbo.Menus(Id),
    CONSTRAINT FK_MenuPermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES dbo.Permissions(Id),
    CONSTRAINT UQ_MenuPermissions UNIQUE (MenuId, PermissionId)
);
GO

-- Crear tabla de relación entre Rol, Formulario y Permiso (relación ternaria)
CREATE TABLE dbo.RolFormPermissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RolId INT NOT NULL,
    FormId INT NOT NULL,
    PermissionId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_RolFormPermissions_Roles FOREIGN KEY (RolId) REFERENCES dbo.Roles(Id),
    CONSTRAINT FK_RolFormPermissions_Forms FOREIGN KEY (FormId) REFERENCES dbo.Forms(Id),
    CONSTRAINT FK_RolFormPermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES dbo.Permissions(Id),
    CONSTRAINT UQ_RolFormPermissions UNIQUE (RolId, FormId, PermissionId) 
);
GO

-- Crear tabla de Log de Cambios
CREATE TABLE dbo.ChangeLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EntityName NVARCHAR(100) NOT NULL,
    EntityId INT NOT NULL,
    Action NVARCHAR(50) NOT NULL,
    OldValues NVARCHAR(MAX) NULL,
    NewValues NVARCHAR(MAX) NULL,
    UserId INT NOT NULL,
    Timestamp DATETIME NOT NULL DEFAULT GETUTCDATE(),
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_ChangeLogs_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(Id)
);
GO

-- TABLAS GEOGRÁFICAS --

-- Crear tabla de Países
CREATE TABLE dbo.Countries (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CountryName NVARCHAR(100) NOT NULL,
    CountryCode NVARCHAR(10) NOT NULL,
    Currency NVARCHAR(50) NULL,
    PhonePrefix NVARCHAR(10) NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL
);
GO

-- Crear tabla de Departamentos/Estados
CREATE TABLE dbo.Departments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL,
    DepartmentCode NVARCHAR(10) NOT NULL,
    CountryId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_Departments_Countries FOREIGN KEY (CountryId) REFERENCES dbo.Countries(Id)
);
GO

-- Crear tabla de Ciudades
CREATE TABLE dbo.Cities (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CityName NVARCHAR(100) NOT NULL,
    CityCode NVARCHAR(10) NOT NULL,
    DepartmentId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_Cities_Departments FOREIGN KEY (DepartmentId) REFERENCES dbo.Departments(Id)
);
GO

-- Crear tabla de Distritos/Barrios
CREATE TABLE dbo.Districts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DistrictName NVARCHAR(100) NOT NULL,
    StreetType TINYINT NOT NULL,
    StreetNumber NVARCHAR(20) NULL,
    StreetLetter TINYINT NULL,
    SecondaryNumber NVARCHAR(20) NULL,
    SecondaryLetter TINYINT NULL,
    TertiaryNumber NVARCHAR(20) NULL,
    AdditionalNumber NVARCHAR(20) NULL,
    AdditionalLetter TINYINT NULL,
    CityId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_Districts_Cities FOREIGN KEY (CityId) REFERENCES dbo.Cities(Id)
);
GO

-- Crear tabla de Códigos Postales
CREATE TABLE dbo.CodePostals (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PostalCode NVARCHAR(20) NOT NULL,
    Area NVARCHAR(100) NULL,
    CityId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_CodePostals_Cities FOREIGN KEY (CityId) REFERENCES dbo.Cities(Id)
);
GO

-- TABLAS DE DATOS PERSONALES ESPECÍFICOS --

-- Crear tabla de Clientes
CREATE TABLE dbo.Clients (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClientCode NVARCHAR(50) NULL,
    CreditLimit DECIMAL(18, 2) NULL,
    PersonId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_Clients_Persons FOREIGN KEY (PersonId) REFERENCES dbo.Persons(Id)
);
GO

-- Crear tabla de Empleados
CREATE TABLE dbo.Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Position NVARCHAR(100) NULL,
    Department NVARCHAR(100) NULL,
    HiringDate DATE NOT NULL,
    Salary DECIMAL(18, 2) NOT NULL,
    ContractType TINYINT NOT NULL,
    EmployeeCode NVARCHAR(50) NULL,
    WorkEmail NVARCHAR(255) NULL,
    PersonId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_Employees_Persons FOREIGN KEY (PersonId) REFERENCES dbo.Persons(Id)
);
GO

-- Crear tabla de Proveedores
CREATE TABLE dbo.Providers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CompanyName NVARCHAR(100) NOT NULL,
    ContactName NVARCHAR(100) NULL,
    ContactTitle NVARCHAR(100) NULL,
    TaxID NVARCHAR(50) NULL,
    Website NVARCHAR(255) NULL,
    PaymentTerms NVARCHAR(255) NULL,
    PersonId INT NOT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_Providers_Persons FOREIGN KEY (PersonId) REFERENCES dbo.Persons(Id)
);
GO

-- Insertar datos iniciales

-- Insertar países básicos
INSERT INTO dbo.Countries (CountryName, CountryCode, Currency, PhonePrefix)
VALUES 
    ('Colombia', 'CO', 'COP', '+57'),
    ('Estados Unidos', 'US', 'USD', '+1'),
    ('México', 'MX', 'MXN', '+52');
GO

-- Insertar departamentos para Colombia
INSERT INTO dbo.Departments (DepartmentName, DepartmentCode, CountryId)
VALUES 
    ('Antioquia', 'ANT', 1),
    ('Cundinamarca', 'CUN', 1),
    ('Valle del Cauca', 'VAL', 1);
GO

-- Insertar ciudades
INSERT INTO dbo.Cities (CityName, CityCode, DepartmentId)
VALUES 
    ('Medellín', 'MDE', 1),
    ('Bogotá', 'BOG', 2),
    ('Cali', 'CAL', 3);
GO

-- Insertar roles básicos
INSERT INTO dbo.Roles (Name, Description)
VALUES 
    ('Admin', 'Administrador del sistema con acceso completo'),
    ('Usuario', 'Usuario regular con acceso limitado'),
    ('Supervisor', 'Usuario con capacidad de supervisión');
GO

-- Insertar permisos básicos
INSERT INTO dbo.Permissions (Name, Description, PermissionCode, Type)
VALUES 
    ('Crear', 'Permite crear nuevos registros', 'CREATE', 1),
    ('Leer', 'Permite leer registros existentes', 'READ', 2),
    ('Actualizar', 'Permite actualizar registros existentes', 'UPDATE', 3),
    ('Eliminar', 'Permite eliminar registros existentes', 'DELETE', 4);
GO

-- Insertar módulos básicos
INSERT INTO dbo.Modules (Name, Description, Icon, Url, Order)
VALUES 
    ('Seguridad', 'Administración de seguridad', 'security', '/seguridad', 1),
    ('Administración', 'Administración del sistema', 'admin', '/admin', 2),
    ('Reportes', 'Reportes del sistema', 'report', '/reportes', 3);
GO

-- Insertar formularios básicos
INSERT INTO dbo.Forms (Name, Description, Url, ModuleId, Order)
VALUES 
    ('Usuarios', 'Gestión de usuarios', '/seguridad/usuarios', 1, 1),
    ('Roles', 'Gestión de roles', '/seguridad/roles', 1, 2),
    ('Permisos', 'Gestión de permisos', '/seguridad/permisos', 1, 3);
GO

-- Insertar un usuario administrador por defecto (contraseña debe estar cifrada en producción)
INSERT INTO dbo.Users (Email, Password)
VALUES ('admin@encarte1817.com', 'Admin123!');
GO

-- Asignar rol de administrador al usuario admin
INSERT INTO dbo.RolUsers (RolId, UserId)
VALUES (1, 1);
GO

-- Crear persona para el administrador
INSERT INTO dbo.Persons (FirstName, LastName, IdentificationType, IdentificationNumber)
VALUES ('Administrador', 'Sistema', 1, '00000000');
GO

-- Crear vistas para simplificar consultas comunes

-- Vista para obtener usuarios con sus roles
CREATE OR ALTER VIEW dbo.vw_UsersWithRoles
AS
SELECT 
    u.Id AS UserId,
    u.Email,
    u.Status AS UserStatus,
    r.Id AS RolId,
    r.Name AS RolName,
    r.Description AS RolDescription,
    ru.Status AS AssignmentStatus
FROM 
    dbo.Users u
    LEFT JOIN dbo.RolUsers ru ON u.Id = ru.UserId AND ru.Status = 1
    LEFT JOIN dbo.Roles r ON ru.RolId = r.Id AND r.Status = 1
WHERE 
    u.Status = 1;
GO

-- Vista para obtener roles con sus permisos
CREATE OR ALTER VIEW dbo.vw_RolesWithPermissions
AS
SELECT 
    r.Id AS RolId,
    r.Name AS RolName,
    f.Id AS FormId,
    f.Name AS FormName,
    p.Id AS PermissionId,
    p.Name AS PermissionName,
    p.PermissionCode
FROM 
    dbo.Roles r
    JOIN dbo.RolFormPermissions rfp ON r.Id = rfp.RolId AND rfp.Status = 1
    JOIN dbo.Forms f ON rfp.FormId = f.Id AND f.Status = 1
    JOIN dbo.Permissions p ON rfp.PermissionId = p.Id AND p.Status = 1
WHERE 
    r.Status = 1;
GO

PRINT 'Script DDL ejecutado correctamente.';
GO