SELECT TOP(1) AVG([Salary]) AS [MinAvgSalary]
FROM [Employees]
GROUP BY [DepartmentID]
ORDER BY [MinAvgSalary]