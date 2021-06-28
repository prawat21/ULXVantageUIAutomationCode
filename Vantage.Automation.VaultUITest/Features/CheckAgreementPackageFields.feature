Feature: CheckAgreementPackageFields

@tc-56190
Scenario: Check Agreement Package Fields
	Given I am logged in to CRM

	When I open 'Agreement Packages' from left navigation menu
		And I search for entity with name '{AgreementPackageForClauses}'
		And I open agreement package grid record with name '{AgreementPackageForClauses}'
	Then The following fields are present on Agreement Package form:
	| Values                     |
	| Name                       |
	| Strategic Alliance Partner |
	| Interpretation Title       |
	| Interpretation Comments    |
	| Counterparty Legal Entity  |
	| Customer Location Country  |
		And 'Agreements' tab is 'present' on entity page
