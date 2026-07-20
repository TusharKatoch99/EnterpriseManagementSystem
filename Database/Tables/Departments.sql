
-- ===========================================
-- Departments
-- ===========================================

CREATE TABLE Departments
(
    DepartmentId INT IDENTITY(1,1) PRIMARY KEY,

    DepartmentName NVARCHAR(100) NOT NULL UNIQUE,

    Description NVARCHAR(255) NULL,

    IsActive BIT NOT NULL DEFAULT 1,

    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    UpdatedAt DATETIME2 NULL
);
GO

