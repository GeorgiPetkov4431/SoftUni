CREATE OR ALTER FUNCTION udf_LeagueTopScorer 
(
	@leagueName NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
	SELECT
		p.[Name] AS [PlayerName],
		ps.[Goals] AS [TotalGoals]
	FROM [Players] AS p
	JOIN [PlayerStats] AS ps ON p.[Id] = ps.[PlayerId]
	JOIN [PlayersTeams] AS pt ON p.[Id] = pt.[PlayerId]
	JOIN [Teams] AS t ON t.[Id] = pt.[TeamId]
	JOIN [Leagues] AS l ON l.[Id] = t.[LeagueId]
	WHERE l.[Name] = @leagueName
	AND ps.[Goals] = 
	(
		SELECT MAX(ps.[Goals])
		FROM [PlayerStats] AS ps
		JOIN [Players] AS p ON ps.[PlayerId] = p.[Id]
		JOIN [PlayersTeams] AS pt ON p.[Id] = pt.[PlayerId]
		JOIN [Teams] AS t ON pt.[TeamId] = t.[Id]
		JOIN [Leagues] AS l ON l.[Id] = t.[LeagueId]
		WHERE l.[Name] = @leagueName
	)
)