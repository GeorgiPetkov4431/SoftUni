SELECT TOP(10) e.[FirstName], e.[LastName], e.[DepartmentID]
FROM [Employees] AS e
JOIN
(
	SELECT [DepartmentID], AVG([Salary]) AS [AvgSalary]
	FROM [Employees]
	GROUP BY [DepartmentID]
) AS a ON e.[DepartmentID] = a.[DepartmentID]
WHERE e.[Salary] > a.[AvgSalary]