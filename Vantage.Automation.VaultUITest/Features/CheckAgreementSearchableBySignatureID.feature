Feature: CheckAgreementSearchableBySignatureID

@tc-60771
Scenario: Agreement Searchable By Signature ID
	Given I am logged in to CRM
	When I go to create new Agreement
		And I fill in the following fields on General Agreement page:
		| Agreement Name | Attested |
		| test-{####}    | true     |
		And I click 'Save' on Command Bar
		And I save agreement name from Agreement form as 'agreementName'
		And I save Signature ID from Agreement form as 'signatureID'		

	When I open All Agreements view
		And I search for saved 'signatureID' agreement
	Then I verify that the Grid has at least 1 records

	When I open global search
		And I fill in saved value 'signatureID' into global search
	Then Global search results contain saved 'agreementName' agreement