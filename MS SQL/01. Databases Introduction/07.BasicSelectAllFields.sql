-- Problem 16
USE [master]

CREATE DATABASE [SoftUni]

GO

USE [SoftUni]

CREATE TABLE [Towns](
	[Id] INT PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(50) NOT NULL
)

GO

CREATE TABLE [Addresses](
	[Id] INT PRIMARY KEY IDENTITY(1, 1),
	[AddressText] VARCHAR(100) NOT NULL,
	[TownId] INT FOREIGN KEY REFERENCES [Towns]([Id]) NOT NULL
)

CREATE TABLE [Departments](
	[Id] INT PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(50) NOT NULL
)

GO

CREATE TABLE [Employees](
	[Id] INT PRIMARY KEY IDENTITY(1, 1),
	[FirstName] VARCHAR(50) NOT NULL,
	[MiddleName] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,
	[JobTitle] VARCHAR(50) NOT NULL,
	[DepartmentId] INT FOREIGN KEY REFERENCES [Departments]([Id]) NOT NULL,
	[HireDate] DATE NOT NULL,
	[Salary] DECIMAL(8, 2) NOT NULL,
	[AddressId] INT FOREIGN KEY REFERENCES [Addresses]([Id]) NOT NULL
)

INSERT INTO [Towns] ([Name])
VALUES 
('Sofia'),
('Plovdiv'),
('Varna');

INSERT INTO [Departments] ([Name])
VALUES 
('Software Development'),
('Engineering'),
('Quality Assurance'),
('Sales'),
('Marketing');

GO

INSERT INTO [Addresses] ([AddressText], [TownId])
VALUES 
('123 Tsar Boris III Blvd', 1), -- Sofia
('45 Gladstone St', 2),        -- Plovdiv
('88 Knyaz Boris I Blvd', 3);  -- Varna

GO

INSERT INTO [Employees] (
	[FirstName], [MiddleName], [LastName], 
	[JobTitle], [DepartmentId], [HireDate], [Salary], [AddressId]
)
VALUES 
('Ivan', 'Ivanov', 'Ivanov', 'Software Engineer', 1, '2020-03-22', 4500, 1),
('Maria', 'Petrova', 'Georgieva', 'Sales Manager', 2, '2021-07-15', 3800, 2),
('Georgi', 'Stefanov', 'Dimitrov', 'Marketing Specialist', 3, '2023-11-01', 2900, 3);

GO

-- Problem 18
INSERT INTO [Employees] (
	[FirstName], [MiddleName], [LastName], 
	[JobTitle], [DepartmentId], [HireDate], [Salary], [AddressId]
)
VALUES 
('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 1, '2013-02-01', 3500.00, 1),
('Petar', 'Petrov', 'Petrov', 'Sales Manager', 2, '2021-07-15', 3800, 2),
('Maria', 'Petrova', 'Ivanova', 'Intern', 3, '2023-11-01', 2900, 3);

-- Problem 19
SELECT * FROM [Towns];

SELECT * FROM [Departments];

SELECT * FROM [Employees];