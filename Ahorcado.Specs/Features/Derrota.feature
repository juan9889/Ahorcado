Feature: Derrota

A short summary of the feature

@Derrota
Scenario: Derrota
	Given I logged in with credentials Franco and 1234
	And I start a new game on Easy difficulty
	When I type the same incorrect letter six times in a row
	Then It should display a defeat message