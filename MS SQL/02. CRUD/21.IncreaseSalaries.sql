UPDATE [Employees]
SET [Salary] *= 1.12
WHERE [DepartmentID] IN (1, 2, 4, 11)

GO

SELECT [Salary] FROM [Employees]