SELECT
	t.[DepartmentID],
	MAX(t.[Salary]) AS [ThirdHighestSalary]
FROM
(
	SELECT
		[DepartmentID],
		[Salary],
		DENSE_RANK() OVER (PARTITION BY [DepartmentID] ORDER BY [Salary] DESC) AS [Rank]
	FROM [Employees]
) AS t
WHERE t.[Rank] = 3
GROUP BY t.[DepartmentID]