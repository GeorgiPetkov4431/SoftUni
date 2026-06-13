CREATE OR ALTER PROCEDURE usp_UpdatePlayerStats
(
	@PlayerId INT,
	@GoalsDelta INT = NULL,
	@AssistsDelta INT = NULL 
)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM [PlayerStats] WHERE [PlayerId] = @PlayerId)
	BEGIN
		INSERT INTO [PlayerStats]
		VALUES (@PlayerId, 0, 0)
	END

	IF @GoalsDelta IS NOT NUll
	BEGIN
		UPDATE [PlayerStats]
		SET [Goals] += @GoalsDelta
		WHERE [PlayerId] = @PlayerId
	END

	IF @AssistsDelta IS NOT NUll
	BEGIN
		UPDATE [PlayerStats]
		SET [Assists] += @AssistsDelta
		WHERE [PlayerId] = @PlayerId
	END

	/*
	UPDATE [PlayerStats]
	SET
	[Goals] = CASE
		WHEN @GoalsDelta IS NULL THEN [Goals]
		ELSE [Goals] + @GoalsDelta
	END,

	[Assists] = CASE
		WHEN @AssistsDelta IS NULL THEN [Assists]
		ELSE [Assists] + @AssistsDelta
	END
	WHERE [PlayerId] = @PlayerId
	*/

END