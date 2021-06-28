Feature: CheckRevertAudit

@tc-56956
Scenario: Check Revert Audit Of Account
	Given I am logged in to CRM as an Admin
	When I go to create New Account
		And I fill in the following fields on Account Summary page:
		| Account Name           | Subscription Level |
		| TestAccount{Timestamp} | Bronze             |
		And I click 'Save' on Command Bar
		And I fill in the following fields on Account Summary page:
		| Phone          |
		| 1-234-567-8901 |
		And I click 'Save' on Command Bar
		And I save account name from Account Summary page as 'accountName'

	When I open the Revert Audit area
		And I search Revert Audit for user '{AdminDisplayedUserName}'
	Then I verify that the Revert Audit table has at least 2 records
		And I verify that the Revert Audit table contains 1 records with the following values:
		| Values                   |
		| Update                   |
		| Account                  |
		| {AdminDisplayedUserName} |
		| [accountName]			   |
		And I verify that the Revert Audit table contains 1 records with the following values:
		| Values                   |
		| Create                   |
		| Account                  |
		| {AdminDisplayedUserName} |
		| [accountName]			   |

	When I attempt to Undo Audit of record 1 of the Revert Audit table
		And I search Revert Audit for user '{AdminDisplayedUserName}'
		And I wait '5' seconds
	Then I verify that the Revert Audit table has at least 3 records
		And I verify that the Revert Audit table contains 2 records with the following values:
		| Values                   |
		| Update                   |
		| Account                  |
		| {AdminDisplayedUserName} |
		| [accountName]			   |
