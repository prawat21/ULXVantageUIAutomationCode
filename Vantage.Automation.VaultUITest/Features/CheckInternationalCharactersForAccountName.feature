Feature: CheckInternationalCharactersForAccountName

@tc-59434
Scenario: Check International Characters For Account Name
	Given I am logged in to CRM

	When I go to create New Account
		And I fill in the following fields on Account Summary page:
		| Account Name               | Subscription Level |
		| テストアカウント{Timestamp} | Gold               |
		And I click 'Save' on Command Bar
		And I save account name from Account Summary page as 'name'
		And I open global search
		And I fill in saved value 'name' into global search
	Then Global search results contain saved 'name' account