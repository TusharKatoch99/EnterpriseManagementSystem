-- ===========================================
-- Designations
-- ===========================================

CREATE TABLE Designations
(
    DesignationId INT IDENTITY(1,1) PRIMARY KEY,

    DesignationName NVARCHAR(100) NOT NULL UNIQUE,

    Description NVARCHAR(255) NULL,

    IsActive BIT NOT NULL DEFAULT 1,

    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    UpdatedAt DATETIME2 NULL
);
GO

