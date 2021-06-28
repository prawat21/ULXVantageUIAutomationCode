Feature: CheckAgreementFileSizeLimit

@tc-56173
Scenario: Check Agreement Upload File Size Limit
	Given I am logged in to CRM
	When I go to create New Agreement File
		And I fill in the following fields on Agreement Package General page:
		| Name            | File Type          | Agreement          | Agreement Package		    | 
		| testfile-{####} | Ancillary Document | {Agreement_Bronze} | {AgreementPackage_Bronze} | 
		And I click 'Save' on Command Bar

	When I upload 'unacceptable' file to Agreement Files
	Then Error popup is present with text 'An error has occurred while trying to upload the attachment. The file cannot be attached because it exceeds the maximum size limit.'

	When I close Error popup
	Then New 'unacceptable' file is 'absent' on Agreement Files grid

	When I upload 'acceptable' file to Agreement Files
		And I wait '10' seconds
	Then New 'acceptable' file is 'present' on Agreement Files grid
