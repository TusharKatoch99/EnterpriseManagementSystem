
-- ===========================================
-- RefreshTokens Table
-- ===========================================

CREATE TABLE RefreshTokens
(
    RefreshTokenId INT IDENTITY(1,1) PRIMARY KEY,

    UserId INT NOT NULL,

    Token NVARCHAR(MAX) NOT NULL,

    ExpiresAt DATETIME2 NOT NULL,

    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    RevokedAt DATETIME2 NULL,

    IsRevoked BIT NOT NULL DEFAULT 0,

    CONSTRAINT FK_RefreshTokens_Users
        FOREIGN KEY(UserId)
        REFERENCES Users(UserId)
);
GO
