Feature: EditAgreementPackageFieldFromAgreementPage

@tc-56551
Scenario: Edit Agreement Package Field From Agreement Page
	Given I am logged in to CRM

	When I open 'Agreements' from left navigation menu
		And I search for entity with name '{AgreementForEdit}'
		And I open the grid record at index '1'
		And I update Agreement Package field value by searching for 'quick ap' from Agreement page and save new value as 'new AP'
	Then Agreement Package field value on Agreement page is equal to saved 'new AP'

	When I click New Agreement Page link from Agreement Package field dropdown on Agreement page
		And I fill in the following fields on Quick Create - Agreement Package form:
		| Name                | Vault Account Value |
		| quick ap{Timestamp} | {TestAccount}       |
		And I save Name from Quick Create - Agreement Package form as 'quick ap name'
		And I click Save & Close on Quick Create form
	Then Agreement Package field value on Agreement page is equal to saved 'quick ap name'
