
-- ===========================================
-- UserRoles Table
-- ===========================================

CREATE TABLE UserRoles
(
    UserRoleId INT IDENTITY(1,1) PRIMARY KEY,

    UserId INT NOT NULL,

    RoleId INT NOT NULL,

    AssignedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_UserRoles_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(UserId),

    CONSTRAINT FK_UserRoles_Roles
        FOREIGN KEY (RoleId)
        REFERENCES Roles(RoleId),

    CONSTRAINT UQ_UserRole UNIQUE(UserId, RoleId)
);
GO

