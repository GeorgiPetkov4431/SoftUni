CREATE PROCEDURE usp_GetTownsStartingWith
(
	@StartingVal VARCHAR(20)
)
AS
(
	SELECT [Name]
	FROM [Towns]
	WHERE [Name] LIKE @StartingVal + '%'
)