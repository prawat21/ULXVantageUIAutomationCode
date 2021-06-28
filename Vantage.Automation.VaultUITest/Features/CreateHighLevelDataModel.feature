Feature: CreateHighLevelDataModel

@tc-56188
@tc-56519
Scenario: Create High Level Data Model and Verify Inheritence
	Given I am logged in to CRM
	When I create a 'Gold' Account and save it as 'parent'
		And I create a 'Gold' Account with Parent 'parent' and save it as 'child'
		And I create an Agreement Package with Account 'child' and save it as 'agreementPackage'
		And I create an Agreement with Agreement Package 'agreementPackage' and Account 'child' and save it as 'agreement'
		And I create an Agreement File with Agreement 'agreement' and Agreement Package 'agreementPackage' and save it as 'agreementFile'

	When I open Active Accounts view
		And I search for entity with saved name 'child'
		And I open the grid record at index '1'
		And I fill in the following fields on Account Summary page:
		| Legal Hold |
		| true		 |
		And I click 'Save' on Command Bar
	When I open Active Agreement packages view
		And I search for entity with saved name 'agreementPackage'
		And I open the grid record at index '1'
	Then Entity field 'Legal Hold' is '1'
	#When I open Active Agreements view
	#	And I search for entity with saved name 'agreement'
	#	And I open the grid record at index '1'
	#Then Entity field 'Legal Hold' is '1'
	When I open Active Agreement files view
		And I search for entity with saved name 'agreementFile'
		And I open the grid record at index '1'
		And I open 'Overview' tab
	Then Entity field 'Legal Hold' is 'True'
