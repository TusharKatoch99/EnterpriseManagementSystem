CREATE OR ALTER PROCEDURE sp_SaveRefreshToken
(
    @UserId INT,
    @Token NVARCHAR(500),
    @ExpiresAt DATETIME
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
        IsRevoked
    )
    VALUES
    (
        @UserId,
        @Token,
        @ExpiresAt,
        GETDATE(),
        0
    );
END
GO