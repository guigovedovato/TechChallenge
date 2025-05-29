Feature: Create Game

This feature is about creating a new game.

There are 1 validation:
* Game must be unique

Rule: The game must be unique.

@success @game @create
Scenario: Game is created successfully
	Given the user started the creation of a new game
	And add the values to the game
		| name          | Description     | ImageUrl          | Genre | Publisher | ReleaseDate | Price |
		| The Witcher 3 | Geralt of Rivia | urloftheimagehere | 2     | CD Projekt | 2023-10-01  | 59.99 |
	When the user sends the request
	Then the game is created and the id is returned

@error @game @create @name
Scenario: Game already exists
	Given the user started the creation of a new game
	And add the values to the game
		| name          | Description     | ImageUrl          | Genre | Publisher | ReleaseDate | Price |
		| The Witcher 3 | Geralt of Rivia | urloftheimagehere | 2     | CD Projekt | 2023-10-01  | 59.99 |
	And the game already exists in the system
	When the user sends the request
	Then the game is not created with the message "Game with name The Witcher 3 already exists"