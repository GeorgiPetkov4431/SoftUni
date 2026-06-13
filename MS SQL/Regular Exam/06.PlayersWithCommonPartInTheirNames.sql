SELECT p.[Name], t.[City]
FROM [Players] AS p
JOIN [PlayersTeams] AS pt ON p.[Id] = pt.[PlayerId]
JOIN [Teams] AS t ON t.[Id] = pt.[TeamId]
WHERE p.[Name] LIKE '%Aaron%'
ORDER BY p.[Name]