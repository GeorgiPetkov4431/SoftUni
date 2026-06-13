UPDATE ps
SET GOALS += 1
FROM [PlayerStats] AS ps
JOIN [Players] AS p ON p.[Id] = ps.[PlayerId]
JOIN [PlayersTeams] AS pt ON p.[id] = pt.[PlayerId]
JOIN [Teams] AS t ON t.[Id] = pt.[TeamId]
JOIN [Leagues] AS l ON t.[LeagueId] = l.[Id]
WHERE l.[Name] = 'La Liga' AND p.[Position] = 'Forward'