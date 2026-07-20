CREATE OR ALTER PROCEDURE sp_GetUserByEmail
(
    @Email NVARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Users
    WHERE Email = @Email
      AND IsDeleted = 0;
END
GO