CREATE OR ALTER PROCEDURE sp_GetDesignationById
(
    @DesignationId INT
)
AS
BEGIN

    SET NOCOUNT ON;

    SELECT

        DesignationCode,
        DesignationId,
        DesignationName,
        Description,
        IsActive,
        CreatedAt

    FROM Designations

    WHERE DesignationId=@DesignationId
      AND IsDeleted=0;

END