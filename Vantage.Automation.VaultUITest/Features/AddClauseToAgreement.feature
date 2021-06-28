Feature: AddClauseToAgreement

@tc-56940
Scenario: Add Clause To Agreement
	Given I am logged in to CRM

	When I open 'Agreements' from left navigation menu
		And I search for entity with name '{AgreementForClauses}'
		And I open agreements grid record with name '{AgreementForClauses}'
		And I open 'Clauses' tab
		And I click 'New Clause' button on Clause subgrid
	Then Agreement value is '{AgreementForClauses}' on New Clause form

	When I fill in the following fields on New Clause form:
	| Clause Text               | Agreement Package            | Agreement File		 |
	| AutoTestClause{Timestamp} | {AgreementPackageForClauses} | {TestAgreementFile} |
		And I save clause text from New Clause form as 'clause text'
		And I click 'Save & Close' on Command Bar
	Then Clause with saved text 'clause text' is present on Clauses subgrid