-- ===========================================
-- Employees
-- ===========================================

CREATE TABLE Employees
(
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,

    UserId INT NULL,

    EmployeeCode NVARCHAR(20) NOT NULL UNIQUE,

    FirstName NVARCHAR(100) NOT NULL,

    LastName NVARCHAR(100) NOT NULL,

    Gender NVARCHAR(20) NULL,

    DateOfBirth DATE NULL,

    Email NVARCHAR(255) NOT NULL UNIQUE,

    PhoneNumber NVARCHAR(20) NULL,

    DepartmentId INT NOT NULL,

    DesignationId INT NOT NULL,

    ReportingManagerId INT NULL,

    DateOfJoining DATE NOT NULL,

    Salary DECIMAL(18,2) NULL,

    Address NVARCHAR(500) NULL,

    IsActive BIT NOT NULL DEFAULT 1,

    IsDeleted BIT NOT NULL DEFAULT 0,

    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Employees_Users
        FOREIGN KEY(UserId)
        REFERENCES Users(UserId),

    CONSTRAINT FK_Employees_Departments
        FOREIGN KEY(DepartmentId)
        REFERENCES Departments(DepartmentId),

    CONSTRAINT FK_Employees_Designations
        FOREIGN KEY(DesignationId)
        REFERENCES Designations(DesignationId),

    CONSTRAINT FK_Employees_Manager
        FOREIGN KEY(ReportingManagerId)
        REFERENCES Employees(EmployeeId)
);
GO