Feature: CheckEntityHierarchy

@tc-56846
Scenario: Check Account Hierarchy
	Given I am logged in to CRM
	When I open Active Accounts view
		And I search for entity with name '{Account_Gold}'
		And I open the grid record at index '1'
		And I open 'Related' -> 'Agreement Packages' subtab
	Then I verify that the Grid contains records with the following properties
	| ulx_name				  | ulx_vaultaccountvalue |
	| {AgreementPackage_Gold} | {Account_Gold}		  |

	When I open Active Agreements view
		And I search for entity with name '{Agreement_Gold}'
		And I open the grid record at index '1'
		And I open 'Agreement Files' tab
	Then I verify that the SubGrid 'AgreementFiles' contains records with the following properties
	| Agreement File Name  | Agreement        |
	| {AgreementFile_Gold} | {Agreement_Gold} |


Scenario: Check Agreement Hierarchy
	Given I am logged in to CRM

	When I open 'Agreements' from left navigation menu
		And I search for entity with name '{AgreementForClauses}'
		And I open agreements grid record with name '{AgreementForClauses}'
	When I open 'Clauses' tab
	Then I verify that the SubGrid 'Clausesagr' has at least 1 records
	When I open 'Agreement Files' tab
	Then I verify that the SubGrid 'AgreementFiles' has at least 1 records
	
@tc-57140
Scenario: Check Agreement Package Hierarchy
	Given I am logged in to CRM

	When I open 'Agreement Packages' from left navigation menu
		And I search for entity with name '{AgreementPackageForEdit}'
		And I open agreements grid record with name '{AgreementPackageForEdit}'
	When I open 'Clauses' tab
	Then I verify that the SubGrid 'Clauses' has at least 1 records
	When I open 'Agreements' tab
	Then I verify that the SubGrid 'Agreement' has at least 1 records
	When I open 'Agreement Files' tab
	Then I verify that the SubGrid 'AgreementsFiles' has at least 1 records
