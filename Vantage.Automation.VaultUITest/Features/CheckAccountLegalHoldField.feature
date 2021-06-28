Feature: CheckAccountLegalFlowField

@tc-56172
Scenario: Check Account Legal Hold Field Is Editable
	Given I am logged in to CRM

	When I open Active Accounts view
		And I search for entity with name '{AccountForLegalHold}'
		And I open accounts grid record with name '{AccountForLegalHold}'
	Then Legal Hold value is editable on Account Summary tab