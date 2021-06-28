Feature: CheckSignatureIdIsGeneratedForAgreement

@tc-61523
Scenario: Check Signature Id Is Generated When Agreement Is Attested To
	Given I am logged in to CRM

	When I go to create new Agreement
	Then 'Signature ID' field is empty on Agreement page

	When I fill in the following fields on General Agreement page:
	| Agreement Name      | Attested |
	| TestAgreement{####} | Yes      |
		And I click 'Save' on Command Bar
	Then 'Signature ID' field is populated on Agreement page
		And 'Attested Date' field is populated on Agreement page