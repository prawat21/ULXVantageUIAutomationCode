Feature: CheckAccountFields

@tc-56540
Scenario: Check Account Fields
	Given I am logged in to CRM
	When I open 'Accounts' from left navigation menu
		And I search for entity with name '{AccountForEdit}'
		And I open accounts grid record with name '{AccountForEdit}'
	Then The following fields are present on Account Summary page:
	| Values                  |
	| Account Name            |
	| Account Number          |
	| Account Category        |
	| Salesforce Account Name |
	| Country                 |
	| Region                  |
