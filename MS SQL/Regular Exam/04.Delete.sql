DELETE ps
FROM [PlayerStats] AS ps
JOIN [Players] AS p ON ps.[PlayerId] = p.[Id]
JOIN [PlayersTeams] AS pt ON p.[id] = pt.[PlayerId]
JOIN [Teams] AS t ON t.[Id] = pt.[TeamId]
JOIN [Leagues] AS l ON t.[LeagueId] = l.[Id]
WHERE p.[Name] IN ('Luuk de Jong', 'Josip Sutalo') AND l.[Name] = 'Eredivisie'

GO

DELETE pt
FROM [PlayersTeams] AS pt
JOIN [Players] AS p ON pt.[PlayerId] = p.[Id]
JOIN [Teams] AS t ON t.[Id] = pt.[TeamId]
JOIN [Leagues] AS l ON t.[LeagueId] = l.[Id]
WHERE p.[Name] IN ('Luuk de Jong', 'Josip Sutalo') AND l.[Name] = 'Eredivisie'

GO

DELETE p
FROM [Players] AS p
JOIN [PlayersTeams] AS pt ON p.[Id] = pt.[PlayerId]
JOIN [Teams] AS t ON t.[Id] = pt.[TeamId]
JOIN [Leagues] AS l ON l.[Id] = t.[LeagueId]
WHERE p.[Name] IN ('Luuk de Jong', 'Josip Sutalo') AND l.[Name] = 'Eredivisie'