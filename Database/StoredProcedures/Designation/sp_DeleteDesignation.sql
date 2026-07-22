CREATE OR ALTER PROCEDURE sp_DeleteDesignation
(
    @DesignationId INT
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE Designations

    SET

        IsDeleted=1,
        UpdatedAt=GETDATE()

    WHERE DesignationId=@DesignationId;

END