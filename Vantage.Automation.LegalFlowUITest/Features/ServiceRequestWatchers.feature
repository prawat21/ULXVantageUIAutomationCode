Feature: ServiceRequestWatchers

# 7	Validate that a Legal Professional should have ability to add "watchers" so that they can track the service requests.
# 8	Validate that Legal professional should have the ability to upgrade a 'watcher' to a 'collaborator'.
Scenario: Add Watcher To Service Request And Update To Collaborator
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type                              | Region                 | Country                 | State/Province        | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup

	When I add watcher '{LegalProUser1}' to the Service Request
		And I wait for the Browser to go Idle
	Then I verify there is a watcher with Name '{LegalProUser1}' and Collaborating 'No'

	When I open watcher '{LegalProUser1}'
		And I wait for the Browser to go Idle and wait 5 seconds
		And I fill out the following fields
		| Collaborating |
		| Yes           |
		And I Save And Close the Entity
		And I wait for the Browser to go Idle
	Then I verify there is a watcher with Name '{LegalProUser1}' and Collaborating 'Yes'

	When I open watcher '{LegalProUser1}'
		And I wait for the Browser to go Idle and wait 5 seconds
		And I Delete the Entity
		And I wait for the Browser to go Idle

