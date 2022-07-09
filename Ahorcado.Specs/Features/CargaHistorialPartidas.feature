Feature: CargaHistorialPartidas

A short summary of the feature

@CargaHistorialPartidas
Scenario: Carga de Historial de Partidas
	Given I logged in with credentials Franco and 1234
	When I click on the Game History button
	Then It should display the user game history
