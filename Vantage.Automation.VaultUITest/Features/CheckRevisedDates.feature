Feature: CheckRevisedDates

Background:
	Given I am logged in to CRM

	When I open 'Agreements' from left navigation menu
		And I search for entity with name '{AgreementForRevised}'
		And I open agreements grid record with name '{AgreementForRevised}'

@tc-57117
Scenario: Check Revised Dates For Agreement - Upstream	
	Then The following date fields are present on Agreement form:
	| Values                     |
	| Revised Expiration Date    |
	| Revised Effective Date     |
	| Revised Service Start Date |

	When I fill in the following fields on General Agreement page:
	| Revised Expiration Date | Revised Effective Date | Revised Service Start Date | Cascade Fields |
	| {RandomFutureDate}      | {RandomFutureDate}     | {RandomFutureDate}         | Upstream       |
		And I save Revised Dates from General Agreement page as 'revised dates'
		And I click 'Save & Close' on Command Bar
		And I open Active Agreement packages view
		And I search for entity with name '{AgreementPackageForRevised}'
		And I open agreement package grid record with name '{AgreementPackageForRevised}'
	Then Revised Dates on General Agreement Package page are equal to saved 'revised dates'

@tc-57117
Scenario: Check Revised Dates For Agreement - Downstream
	When I fill in the following fields on General Agreement page:
	| Revised Expiration Date | Revised Effective Date | Revised Service Start Date | Cascade Fields |
	| {RandomFutureDate}      | {RandomFutureDate}     | {RandomFutureDate}         | Downstream     |
		And I save Revised Dates from General Agreement page as 'revised dates'
		And I click 'Save' on Command Bar
		And I open 'Child Agreements' tab
		And I open the grid record at index '1'
	Then Revised Dates on General Agreement page are equal to saved 'revised dates'

@tc-57117
Scenario: Check Revised Dates For Agreement - Both
	When I fill in the following fields on General Agreement page:
	| Revised Expiration Date | Revised Effective Date | Revised Service Start Date | Cascade Fields |
	| {RandomFutureDate}      | {RandomFutureDate}     | {RandomFutureDate}         | Both           |
		And I save Revised Dates from General Agreement page as 'revised dates'
		And I click 'Save' on Command Bar
		And I open 'Child Agreements' tab
		And I open the grid record at index '1'
	Then Revised Dates on General Agreement page are equal to saved 'revised dates'

	When I open Active Agreement packages view
		And I search for entity with name '{AgreementPackageForRevised}'
		And I open agreement package grid record with name '{AgreementPackageForRevised}'
	Then Revised Dates on General Agreement Package page are equal to saved 'revised dates'

@tc-57117
Scenario: Check Revised Dates For Agreement - None
	When I fill in the following fields on General Agreement page:
	| Revised Expiration Date | Revised Effective Date | Revised Service Start Date | Cascade Fields |
	| {RandomFutureDate}      | {RandomFutureDate}     | {RandomFutureDate}         | None           |
		And I save Revised Dates from General Agreement page as 'revised dates'
		And I click 'Save' on Command Bar
		And I open 'Child Agreements' tab
		And I open the grid record at index '1'
	Then Revised Dates on General Agreement page are not equal to saved 'revised dates'

	When I open Active Agreement packages view
		And I search for entity with name '{AgreementPackageForRevised}'
		And I open agreement package grid record with name '{AgreementPackageForRevised}'
	Then Revised Dates on General Agreement Package page are not equal to saved 'revised dates'