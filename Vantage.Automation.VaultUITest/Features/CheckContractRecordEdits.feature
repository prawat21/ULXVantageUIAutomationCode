Feature: CheckContractRecordEdits

Background:
	Given I am logged in to CRM

@tc-56170
Scenario: Check Account Edits
	When I open Active Accounts view
		And I search for entity with name '{AccountForEdit}'
		And I open accounts grid record with name '{AccountForEdit}'
		And I save account info from Account Summary page as 'initial values'
		And I fill in the following fields on Account Summary page:
		| Phone         | Subscription Level |
		| {RandomPhone} | Bronze             |
		And I save account info from Account Summary page as 'new values'
		And I click 'Save' on Command Bar
		And I open Entity Audit History
	Then Account Audit History grid contains edition record from 'initial values' to 'new values'

@tc-56170
Scenario: Check Agreement Package Edits
	When I open 'Agreement Packages' from left navigation menu
		And I search for entity with name '{AgreementPackageForEdit}'
		And I open agreement package grid record with name '{AgreementPackageForEdit}'
		And I save agreement package info from General page as 'initial values'
		And I fill in the following fields on Agreement Package General page:
		| Data Retention Years |
		| {##}                 |
		And I save agreement package info from General page as 'new values'
		And I click 'Save' on Command Bar
		And I open Entity Audit History
	Then Agreement package Audit History grid contains edition record from 'initial values' to 'new values'