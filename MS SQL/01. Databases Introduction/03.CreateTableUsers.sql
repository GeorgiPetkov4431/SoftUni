-- Problem 08
CREATE TABLE [Users](
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[Username] VARCHAR(50) UNIQUE NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	[ProfilePicture] VARBINARY(MAX),
	[LastLoginTime] DATETIME2,
	[IsDeleted] BIT NOT NULL
)

GO

INSERT INTO [Users]([Username], [Password], [ProfilePicture], [LastLoginTime], [IsDeleted])
VALUES
('Grisha', 'grisha@PW', NULL, '2026-05-10 08:23:11', 0),
('Pesho', 'pesho@PW', NULL, '2026-05-10 07:23:11', 0),
('Misho', 'misho@PW', NULL, '2026-05-10 06:23:11', 0),
('Tosho', 'tosho@PW', NULL, '2026-05-10 05:23:11', 1),
('Chefo', 'chefo@PW', NULL, '2026-05-10 04:23:11', 0)