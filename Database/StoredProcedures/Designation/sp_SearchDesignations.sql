CREATE OR ALTER PROCEDURE sp_SearchDesignations
(
    @Search NVARCHAR(100)=NULL,
    @PageNumber INT=1,
    @PageSize INT=10
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

    WHERE

        IsDeleted=0

        AND

        (
            @Search IS NULL
            OR DesignationCode LIKE '%' + @Search + '%'
            OR DesignationName LIKE '%' + @Search + '%'
        )

    ORDER BY DesignationName

    OFFSET (@PageNumber-1)*@PageSize ROWS

    FETCH NEXT @PageSize ROWS ONLY;

END