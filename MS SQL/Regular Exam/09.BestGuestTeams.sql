SELECT t.[Id], t.[Name], SUM(m.[AwayTeamGoals]) AS [TotalAwayGoals]
FROM [Teams] AS t
JOIN [Matches] AS m ON t.[Id] = m.[AwayTeamId]
GROUP BY t.[Id], t.[Name]
HAVING SUM(m.[AwayTeamGoals]) >= 6
ORDER BY [TotalAwayGoals] DESC, t.[Name]