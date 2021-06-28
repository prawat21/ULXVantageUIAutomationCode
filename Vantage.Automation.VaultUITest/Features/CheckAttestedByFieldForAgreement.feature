Feature: CheckAttestedByFieldForAgreement

Background:
	Given I am logged in to CRM

@tc-56752
Scenario: Check Attested By Field For New Agreement
	When I go to create new Agreement
		And I fill in the following fields on General Agreement page:
		| Agreement Name      | Attested |
		| TestAgreement{####} | Yes      |
		And I save Agreement Name from General page as 'name'
		And I click 'Save' on Command Bar
	Then I search Advanced Find for an Agreement with name '[name]' and value 'Attested'='Yes'

@tc-57137
Scenario: Check Attested By Field For Edited Agreement
	When I open 'Agreements' from left navigation menu
		And I search for entity with name '{AgreementForEdit}'
		And I open the grid record at index '1'
		And I save Agreement Name from General page as 'name'
		And I toggle Attested field value on General page
		And I save Attested Yes or No field value from General page as 'attested'
		And I click 'Save' on Command Bar
	Then I search Advanced Find for an Agreement with name '[name]' and value 'Attested'='[attested]'