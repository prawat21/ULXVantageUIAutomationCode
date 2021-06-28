Feature: CheckEntityHeaders

@tc-59735
Scenario: Check Agreement Headers
	Given I am logged in to CRM
	When I open 'Agreements' from left navigation menu
		And I search for entity with name '{AgreementForClauses}'
		And I open agreements grid record with name '{AgreementForClauses}'
	Then a header with name 'Vault Account' is present
		And a header with name 'Agreement Package' is present
		And a header with name 'Parent Agreement' is present

@tc-59735
Scenario: Check Agreement File Headers
	Given I am logged in to CRM
	When I open 'Agreement Files' from left navigation menu
		And I search for entity with name '{TestAgreementFile}'
	When I open the grid record at index '1'
	Then a header with name 'Account' is present
		And a header with name 'Agreement Package' is present
		And a header with name 'Agreement' is present

@tc-59735
Scenario: Check Agreement Package Headers
	Given I am logged in to CRM
	When I open 'Agreement Packages' from left navigation menu
		And I search for entity with name '{AgreementPackageForClauses}'
		And I open agreements grid record with name '{AgreementPackageForClauses}'
	Then a header with name 'Service Request' is present
		And a header with name 'Vault Account' is present
		And a header with name 'Legal Hold' is present

@tc-59735
Scenario: Check Clause Headers
	Given I am logged in to CRM
	When I open 'Clauses' from left navigation menu
		And I search for entity with name '{TestClause}'
		And I open agreements grid record with name '{TestClause}'
	Then a header with name 'Agreement Package' is present
		And a header with name 'Agreement' is present
		And a header with name 'Agreement File' is present
