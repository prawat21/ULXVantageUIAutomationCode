Feature: CheckSubscriptionLevels

Background:
	Given I am logged in to CRM

@tc-61436
Scenario: Check Subscription Levels For Account
	When I open 'Accounts' from left navigation menu
		And I search for entity with name '{AccountForEdit}'
		And I open accounts grid record with name '{AccountForEdit}'
	Then Subscription level lookup contains the following values on Account page:
	| Values   |
	| Bronze   |
	| Silver   |
	| Gold     |
	| Platinum |
	| Diamond  |

@tc-61436
Scenario: Check Subscription Levels For Agreement Package
	When I open 'Agreement Packages' from left navigation menu
		And I search for entity with name '{AgreementPackageForEdit}'
		And I open agreement package grid record with name '{AgreementPackageForEdit}'
	Then The following fields are absent on Agreement Package form:
	| Values             |
	| Subscription Level |