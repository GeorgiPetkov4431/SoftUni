SELECT TOP(10)
	th.[Name] AS [HomeTeamName],
	ta.[Name] AS [AwayTeamName],
	l.[Name] AS [LeagueName],
	FORMAT(m.[MatchDate], 'yyyy-MM-dd') AS [MatchDate]
FROM [Matches] AS m
JOIN [Teams] AS th ON m.[HomeTeamId] = th.[Id]
JOIN [Teams] AS ta ON m.[AwayTeamId] = ta.[Id]
JOIN [Leagues] AS l ON m.[LeagueId] = l.[Id]
WHERE m.[MatchDate] > '09/01/2024'
AND m.[MatchDate] < '09/15/2024'
AND l.[Id] % 2 = 0
ORDER BY m.[MatchDate], [HomeTeamName]