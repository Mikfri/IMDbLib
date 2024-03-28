ALTER PROCEDURE dbo.SearchPersons
@searchString NVARCHAR(400)
AS
BEGIN
    -- Søgning med wildcard og sortering i alfabetisk rækkefølge
    SELECT * FROM Persons
    WHERE PrimaryName LIKE '%' + @searchString + '%'
    ORDER BY PrimaryName ASC;
END;