Feature: CheckAuditHistoryIsPresentForEntities

@tc-63424
Scenario: Check Audit History Is Present For Entities
	Given I am logged in to CRM
	When I create a 'Gold' Account and save it as 'parent'
		And I create an Agreement Package with Account 'parent' and save it as 'agreementPackage'
		And I create an Agreement with Agreement Package 'agreementPackage' and Account 'parent' and save it as 'agreement'
		And I create an Agreement File with Agreement 'agreement' and Agreement Package 'agreementPackage' and save it as 'agreementFile'
		And I create a Clause with Agreement 'agreement' and Agreement Package 'agreementPackage' and Agreement File 'agreementFile' and save it as 'clause'

	When I open Active Accounts view
		And I search for entity with saved name 'parent'
		And I open the grid record at index '1'
		And I open 'Related' -> 'Audit History' subtab
	Then Audit History grid contains records

	When I open Active Agreement packages view
		And I search for entity with saved name 'agreementPackage'
		And I open the grid record at index '1'
		And I open 'Related' -> 'Audit History' subtab
	Then Audit History grid contains records

	When I open Active Agreements view
		And I search for entity with saved name 'agreement'
		And I open the grid record at index '1'
		And I open 'Related' -> 'Audit History' subtab
	Then Audit History grid contains records

	When I open Active Agreement files view
		And I search for entity with saved name 'agreementFile'
		And I open the grid record at index '1'
		And I open 'Related' -> 'Audit History' subtab
	Then Audit History grid contains records

	When I open Active Clauses view
		And I search for entity with saved name 'clause'
		And I open the grid record at index '1'
		And I open 'Related' -> 'Audit History' subtab
	Then Audit History grid contains records