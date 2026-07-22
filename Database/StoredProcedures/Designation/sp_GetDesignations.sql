CREATE OR ALTER PROCEDURE sp_GetDesignations
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

    WHERE IsDeleted = 0

    ORDER BY DesignationName;

END