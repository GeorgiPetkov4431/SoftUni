SELECT p.[id], p.[Name], p.[Position]
FROM [Players] AS p
JOIN [PlayersTeams] AS pt ON p.[Id] = pt.[PlayerId]
JOIN [Teams] AS t ON t.[Id] = pt.[TeamId]
WHERE t.[City] = 'London'
ORDER BY p.[Name]