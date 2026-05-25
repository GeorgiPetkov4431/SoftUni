-- Problem 09
-- Constraint Name needs to be the precise name of the Key you intend to drop
ALTER TABLE [Users]
DROP CONSTRAINT [PK__Users__3214EC0751D71623]

GO

-- Composite PK: Combination of several columns forming PK
ALTER TABLE [Users]
ADD CONSTRAINT [PK_Users_Id_Username] PRIMARY KEY([Id], [Username])

-- Problem 10
ALTER TABLE [Users]
ADD CONSTRAINT [CK_Password_Min_Length_5] CHECK(LEN([Password]) >= 5)

GO

-- Problem 11
ALTER TABLE [Users]
ADD CONSTRAINT [Last_Login_Time_Now] DEFAULT(GETDATE()) FOR [LastLoginTime]

GO

INSERT INTO [Users]([Username], [Password], [ProfilePicture], [IsDeleted])
VALUES
('stamat', '123456', NULL, 0)

-- Problem 12
ALTER TABLE [Users]
DROP CONSTRAINT [PK_Users_Id_Username]

GO

ALTER TABLE [Users]
ADD CONSTRAINT [PK_Users_Id] PRIMARY KEY([Id])

GO

ALTER TABLE [Users]
ADD CONSTRAINT [UQ_Users_Username] UNIQUE([Username])

GO

ALTER TABLE [Users]
ADD CONSTRAINT [CK_Username_Min_Length_3] CHECK(Len([Username]) >= 3)

-- Problem 13
USE [master]

GO

CREATE DATABASE [Movies]

GO

USE [Movies]

GO

CREATE TABLE [Directors](
	[Id] INT PRIMARY KEY IDENTITY,
	[DirectorName] VARCHAR(50) NOT NULL,
	[Notes] NVARCHAR(500)
)

CREATE TABLE [Genres](
	[Id] INT PRIMARY KEY IDENTITY,
	[GenreName] VARCHAR(50) NOT NULL,
	[Notes] NVARCHAR(500)
)

CREATE TABLE [Categories](
	[Id] INT PRIMARY KEY IDENTITY,
	[CategoryName] VARCHAR(50) NOT NULL,
	[Notes] NVARCHAR(500)
)

GO

CREATE TABLE [Movies](
	[Id] INT PRIMARY KEY IDENTITY,
	[Title] VARCHAR(50) NOT NULL,
	[DirectorId] INT FOREIGN KEY REFERENCES [Directors]([Id]) NOT NULL,
	[CopyrightYear] INT NOT NULL,
	[Length] TIME NOT NULL,
	[GenreId] INT FOREIGN KEY REFERENCES [Genres]([Id]) NOT NULL,
	[CategoryId] INT FOREIGN KEY REFERENCES [Categories]([Id]) NOT NULL,
	[Rating] DECIMAL(2, 1) NOT NULL,
	[Notes] NVARCHAR(500)
)

GO

INSERT INTO [Directors] VALUES
	('Stanley Kubrick', NULL),
	('Alfred Hitchcock', NULL),
	('Quentin Tarantino', NULL),
	('Steven Spielberg', NULL),
	('Martin Scorsese', NULL)

INSERT INTO [Genres] VALUES
	('Action', NULL),
	('Comedy', NULL),
	('Drama', NULL),
	('Fantasy', NULL),
	('Horror', NULL)

INSERT INTO [Categories] VALUES
	('Short', NULL),
	('Long', NULL),
	('Biography', NULL),
	('Documentary', NULL),
	('TV', NULL)

GO

INSERT INTO [Movies] VALUES
	('The Shawshank Redemption', 1, 1994, '02:22:00', 2, 3, 9.4, NULL),
	('The Godfather', 2, 1972, '02:55:00', 3, 4, 9.2, NULL),
	('Schindler`s List', 3, 1993, '03:15:00', 4, 5, 9.0, NULL),
	('Pulp Fiction', 4, 1994, '02:34:00', 5, 1, 8.9, NULL),
	('Fight Club', 5, 1999, '02:19:00', 1, 2, 8.8, NULL)