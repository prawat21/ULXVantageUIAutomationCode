Feature: CheckRelatedServiceRequests

@tc-56156
Scenario: Check Related Service Requests
	Given I am logged in to CRM

	When I open Active Accounts view
		And I search for entity with name '{TestAccount}'
		And I open accounts grid record with name '{TestAccount}'
		And I open 'Service Requests' tab
	Then Service requests are present and ordered by descending date