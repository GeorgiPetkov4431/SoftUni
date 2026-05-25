-- This is my comment
-- I want to tell you that SQL is really cool!
-- This is text and will not be EXECUTED!!!
-- Additional Information: DO NOT TOUCH THIS!!!
-- K. Ivanov: I made this because...
/*
	This is a multi-line comment
*/

-- Problem 01
USE [master]

CREATE DATABASE [Minions]

GO

USE [Minions]

-- Problem 02
/*
	PK Constraint:
	1. All values in the column should be UNIQUE!!! No repetitions allowed!
	2. Column can't accept NULL values!
*/
-- All columns by default ACCEPT NULL unless explicitly declared!
CREATE TABLE [Minions](
	[Id] INT PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	[Age] INT
)

CREATE TABLE [Towns](
	[Id] INT PRIMARY KEY,
	[Name] VARCHAR(80) NOT NULL
)

GO

-- Problem 03
/*
	FK Constraint:
	1. Value in FK should be an EXISTING PK of referenced table.
	2. FK allows NULL values! => Entity without related element.
	3. FK by default allows repetitions.
*/

ALTER TABLE [Minions]
ADD [TownId] INT FOREIGN KEY REFERENCES [Towns]([Id])

-- Problem 04
/*
	
*/
INSERT INTO [Towns]([Id], [Name])
VALUES
(1, 'Sofia'),
(2, 'Plovdiv'),
(3, 'Varna')

GO

INSERT INTO [Minions]([Id], [Name], [Age], [TownId])
VALUES
(1, 'Kevin', 22, 1),
(2, 'Bob', 15, 3),
(3, 'Steward', NULL, 2)

GO

SELECT * FROM [Minions];

SELECT * FROM [Towns];