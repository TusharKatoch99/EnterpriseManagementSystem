CREATE OR ALTER PROCEDURE sp_RevokeRefreshTokenById
(
    @RefreshTokenId INT
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE RefreshTokens
    SET
        IsRevoked = 1,
        RevokedAt = GETUTCDATE()
    WHERE RefreshTokenId = @RefreshTokenId;

END
GO