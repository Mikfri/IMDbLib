CREATE PROCEDURE [dbo].[GenerateNextMovieId]
AS
BEGIN
    DECLARE @highestId NVARCHAR(255);
    DECLARE @numericPart INT;
    DECLARE @newId NVARCHAR(255);

    -- Find the highest existing ID
    SELECT @highestId = MAX(Tconst) FROM MovieBases;

    -- Extract the numeric part of the ID and increment it
    SET @numericPart = CAST(SUBSTRING(@highestId, 3, LEN(@highestId) - 2) AS INT);
    SET @numericPart = @numericPart + 1;

    -- Construct the new ID
    SET @newId = 'tt' + RIGHT('0000000' + CAST(@numericPart AS NVARCHAR(7)), 7);

    -- Return the new ID
    SELECT @newId;
END

--EXEC SearchMovies @title = 'star wars';

--EXEC GetMovieDetails @tconst = 'tt0086190';
