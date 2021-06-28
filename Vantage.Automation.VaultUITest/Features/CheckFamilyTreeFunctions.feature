Feature: CheckFamilyTreeFunctions

@tc-59522
Scenario: Create Family Tree And Download Files
	Given I am logged in to CRM
	When I create a 'Gold' Account and save it as 'parent'
		And I create an Agreement Package with Account 'parent' and save it as 'agreementPackage'
		And I create an Agreement with Agreement Package 'agreementPackage' and Account 'parent' and save it as 'agreement'
		And I create an Agreement File with 1 Files and Agreement 'agreement' and Agreement Package 'agreementPackage' and save it as 'agreementFile1'
		And I create an Agreement File with 1 Files and Agreement 'agreement' and Agreement Package 'agreementPackage' and save it as 'agreementFile2'
	When I open Active Agreement packages view
		And I search for entity with saved name 'agreementPackage'
		And I open the grid record at index '1'
		And I open 'Family Tree' tab
	Then the Family Tree contains record with name '[agreementPackage]' at level 0	

	When I expand the Family Tree record with name '[agreementPackage]'
		And I wait '2' seconds
		And I expand the Family Tree record with name '[agreement]'
		And I wait '2' seconds
		And I expand the Family Tree record with name '[agreementFile1]'
		And I wait '2' seconds
		And I expand the Family Tree record with name '[agreementFile2]'
	Then I select record with name '[agreementFile1]' and Download Files
		And I select record with name '[agreement]' and Download Files

@tc-59632 
@tc-60098 
Scenario: Create Family Tree And Adjust Relationships
	Given I am logged in to CRM
	When I create a 'Gold' Account and save it as 'parent'
		And I create an Agreement Package with Account 'parent' and save it as 'agreementPackage'
		And I create an Agreement with Agreement Package 'agreementPackage' and Account 'parent' and save it as 'agreement1'
		And I create an Agreement with Agreement Package 'agreementPackage' and Account 'parent' and save it as 'agreement2'

	When I open Active Agreement packages view
		And I search for entity with saved name 'agreementPackage'
		And I open the grid record at index '1'
		And I open 'Family Tree' tab
	Then the Family Tree contains record with name '[agreementPackage]' at level 0
	When I expand the Family Tree record with name '[agreementPackage]'
		And I wait '5' seconds
	Then the Family Tree contains record with name '[agreement1]' at level 1
		And the Family Tree contains record with name '[agreement2]' at level 1

	When I drag the Family Tree record with name '[agreement2]' to the record with name '[agreement1]'
		And I wait '5' seconds
		And I expand the Family Tree record with name '[agreement1]'
		And I wait '5' seconds
	Then the Family Tree contains record with name '[agreement1]' at level 1
		And the Family Tree contains record with name '[agreement2]' at level 2
