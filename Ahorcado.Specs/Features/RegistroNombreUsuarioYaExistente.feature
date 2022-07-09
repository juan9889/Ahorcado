Feature: RegistroNombreUsuarioYaExistente

A short summary of the feature

@RegistroNombreUsuarioYaExistente
Scenario: Registro Nombre Usuario Ya Existente
	Given I entered Franco in the username field
	And I entered 1234 in the password field
	When I click the Register button
	Then I should get an error message saying username is already registered
