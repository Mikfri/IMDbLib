CREATE PROCEDURE DeleteMovie
    @Tconst nvarchar(50)
AS
BEGIN
    -- Delete the movie
    DELETE FROM MovieBases WHERE Tconst = @Tconst;
END