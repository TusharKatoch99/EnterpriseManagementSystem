CREATE OR ALTER PROCEDURE sp_CreateDesignation
(
    @DesignationCode NVARCHAR(20),
    @DesignationName NVARCHAR(100),
    @Description NVARCHAR(500)
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO Designations
    (
        DesignationCode,
        DesignationName,
        Description,
        IsActive,
        IsDeleted,
        CreatedAt
    )
    VALUES
    (
        @DesignationCode,
        @DesignationName,
        @Description,
        1,
        0,
        GETDATE()
    );

    SELECT SCOPE_IDENTITY();

END