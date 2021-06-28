Feature: CheckAccountAliases

@tc-56191
Scenario: Check Account Aliases
	Given I am logged in to CRM

	When I open Active Accounts view
		And I search for entity with name '{AccountForAliases}'
		And I open the grid record at index '1'
		And I save account name from Account Summary page as 'original name'
		And I fill in the following fields on Account Summary page:
		| Account Name                        |
		| Test Account For Aliases{Timestamp} |
		And I click 'Save' on Command Bar		
		And I open 'Aliases' tab
	Then Account Aliases grid contains new record with account name from saved 'original name'

	When I save all account aliases from grid as 'aliases'
		And I open global search
	Then Global Search results are present for all saved 'aliases'