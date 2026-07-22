CREATE OR ALTER PROCEDURE sp_UpdateDesignation
(
    @DesignationCode NVARCHAR(20),
    @DesignationId INT,
    @DesignationName NVARCHAR(100),
    @Description NVARCHAR(500),
    @IsActive BIT
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE Designations

    SET

        DesignationCode = @DesignationCode,
        DesignationName=@DesignationName,
        Description=@Description,
        IsActive=@IsActive,
        UpdatedAt=GETDATE()

    WHERE DesignationId=@DesignationId
      AND IsDeleted=0;

END