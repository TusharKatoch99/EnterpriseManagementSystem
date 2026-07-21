CREATE OR ALTER PROCEDURE sp_GetRefreshToken
(
    @Token NVARCHAR(500)
)
AS
BEGIN

SET NOCOUNT ON;

SELECT
    RefreshTokenId,
    UserId,
    Token,
    ExpiresAt,
    CreatedAt,
    RevokedAt,
    IsRevoked,
    CreatedByIp
FROM RefreshTokens
WHERE Token=@Token;

END
GO