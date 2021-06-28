Feature: RoutingCountry

Scenario: Verify Country Rule Routes To Team
	Given I am logged in to CRM
		And I find a Country that routes to Team
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region   | Country   | State/Province | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {Region} | {Country} | {State}        | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
	When I wait for the Browser to go Idle
		And I refresh the Service Request
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
	Then I verify that the Service Request was routed to '{RouteToTeam}'

Scenario: Verify Country Rule Routes To Person
	Given I am logged in to CRM
		And I find a Country that routes to Person
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region   | Country   |  State/Province | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {Region} | {Country} | {State}         | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
	When I wait for the Browser to go Idle
		And I refresh the Service Request
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
	Then I verify that the Service Request was routed to '{RouteToPerson}'

Scenario: Verify Country Rule Routes To Team With Priority
	Given I am logged in to CRM
		And I find a Country that routes to Team with Priority
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region   | Country   | State/Province | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {Region} | {Country} | {State}        | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
	When I wait for the Browser to go Idle
		And I refresh the Service Request
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
	Then I verify that the Service Request was routed to '{RouteToTeam}'

Scenario: Verify Country Rule Routes To Person With Priority
	Given I am logged in to CRM
		And I find a Country that routes to Person with Priority
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region   | Country   |  State/Province | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {Region} | {Country} | {State}         | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
	When I wait for the Browser to go Idle
		And I refresh the Service Request
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
	Then I verify that the Service Request was routed to '{RouteToPerson}'