-- ===========================================
-- Users Table
-- ===========================================

USE EMS_DB;
GO

CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,

    FirstName NVARCHAR(100) NOT NULL,

    LastName NVARCHAR(100) NOT NULL,

    UserName NVARCHAR(100) NOT NULL UNIQUE,

    Email NVARCHAR(255) NOT NULL UNIQUE,

    PasswordHash NVARCHAR(MAX) NOT NULL,

    PhoneNumber NVARCHAR(20) NULL,

    IsActive BIT NOT NULL DEFAULT 1,

    IsDeleted BIT NOT NULL DEFAULT 0,

    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    UpdatedAt DATETIME2 NULL
);
GO


