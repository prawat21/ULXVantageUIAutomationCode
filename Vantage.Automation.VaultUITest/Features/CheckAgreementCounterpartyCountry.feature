Feature: CheckAgreementCounterpartyCountry

@tc-64467
Scenario: Check Agreement Counterparty Country Values
Given I am logged in to CRM
When I go to create new Agreement
	And I fill in the following fields on General Agreement page:
	| Agreement Name | Counterparty Region |
	| test-{####}    | {EntityRegion}      |
Then The following fields are present on Agreement form:
| Values               |
| Counterparty Country |
	And the Counterparty Country field contains values
When I click 'Save' on Command Bar
	And I save agreement name from Agreement form as 'agreementName'

When I go to create a New Service Request
	And I fill in the following fields on Service Request page:
	| Agreement       |
	| [agreementName] |
	And I wait for the Browser to go Idle
Then I verify field 'Parsing Error Message' contains 'Counterparty Country'
