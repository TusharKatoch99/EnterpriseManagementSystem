CREATE OR ALTER PROCEDURE sp_RegisterUser
(
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @UserName NVARCHAR(100),
    @Email NVARCHAR(255),
    @PhoneNumber NVARCHAR(20),
    @PasswordHash NVARCHAR(500)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Users
    (
        FirstName,
        LastName,
        UserName,
        Email,
        PhoneNumber,
        PasswordHash,
        IsActive,
        IsDeleted,
        CreatedAt
    )
    VALUES
    (
        @FirstName,
        @LastName,
        @UserName,
        @Email,
        @PhoneNumber,
        @PasswordHash,
        1,
        0,
        GETDATE()
    );

    SELECT SCOPE_IDENTITY() AS UserId;
END
GO