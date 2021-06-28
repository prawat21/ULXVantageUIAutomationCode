Feature: CheckSourceSystemAccountValueForAgreementPackage

@tc-56168
Scenario: Check Source System Account Value For Agreement Package
	Given I am logged in to CRM

	When I open 'Agreement Packages' from left navigation menu
		And I search for entity with name '{AgreementPackageForClauses}'
		And I open agreement package grid record with name '{AgreementPackageForClauses}'
	Then Source System Account value is populated on Agreement package general tab
		And Source System Account value is 'readonly' on Agreement package general tab
		And Vault Account value is equal to Source System Account value on Agreement package general tab
		And Vault Account value is 'editable' on Agreement package general tab