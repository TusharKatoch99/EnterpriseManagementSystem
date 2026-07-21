CREATE OR ALTER PROCEDURE sp_RevokeRefreshToken
(
    @Token NVARCHAR(500)
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE RefreshTokens

SET

IsRevoked=1,

RevokedAt=GETUTCDATE()

WHERE Token=@Token;

END
GO