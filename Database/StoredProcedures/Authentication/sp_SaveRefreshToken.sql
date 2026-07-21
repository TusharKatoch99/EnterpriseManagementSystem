CREATE OR ALTER PROCEDURE sp_SaveRefreshToken
(
    @UserId INT,
    @Token NVARCHAR(500),
    @ExpiresAt DATETIME,
    @CreatedByIp NVARCHAR(100) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO RefreshTokens
    (
        UserId,
        Token,
        ExpiresAt,
        CreatedAt,
        IsRevoked,
        CreatedByIp
    )
    VALUES
    (
        @UserId,
        @Token,
        @ExpiresAt,
        GETUTCDATE(),
        0,
        @CreatedByIp
    );
END
GO