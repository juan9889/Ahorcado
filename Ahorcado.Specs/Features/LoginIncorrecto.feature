Feature: LoginIncorrecto


@LoginIncorrecto
Scenario: Login Incorrecto
	Given I entered a GUID in the username field
	And I entered another GUID in the password field
	When I click on the Login button
	Then I should get an error message saying user does not exist
