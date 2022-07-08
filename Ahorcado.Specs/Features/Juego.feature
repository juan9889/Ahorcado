Feature: Juego

A short summary of the feature

@tag1
Scenario: Won Game
	Given I logged in with credentials Franco and 1234
	And I start a new game on Easy difficulty
	When I type all the letters in the correct word
	Then It should display a victory message
