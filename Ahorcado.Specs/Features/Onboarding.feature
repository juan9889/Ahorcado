﻿Feature: Onboarding
	Login existing users
	Register new users
	Enable buttons when credential conditions are met
	Disable buttons if not

@Onboarding
Scenario: Enable controls if credentials are valid
	Given I entered Franco in the username field
	And I entered 1234 in the password field
	Then Buttons should be enabled
	And Close the browser
