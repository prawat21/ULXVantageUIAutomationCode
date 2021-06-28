Feature: ServiceCatalog

Scenario: Verify Catalog Rule Routes To Team
	Given I am logged in to CRM
		And I find a Service Catalog that routes to Team
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region                 | Country                 | State/Province        | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
	When I refresh the Service Request
	Then I verify that the Service Request was routed to '{RouteToTeam}'

Scenario: Verify Catalog Rule Routes To Person
	Given I am logged in to CRM
		And I find a Service Catalog that routes to Person
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region				   | Country			     |  State/Province  	 | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
	When I refresh the Service Request
	Then I verify that the Service Request was routed to '{RouteToPerson}'

Scenario: Verify Catalog Rule Routes To Team With Priority
	Given I am logged in to CRM
		And I find a Service Catalog that routes to Team with Priority
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region                 | Country                 | State/Province        | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
	When I refresh the Service Request
	Then I verify that the Service Request was routed to '{RouteToTeam}'

Scenario: Verify Catalog Rule Routes To Person With Priority
	Given I am logged in to CRM
		And I find a Service Catalog that routes to Person with Priority
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region				   | Country			     |  State/Province  	 | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
	When I refresh the Service Request
	Then I verify that the Service Request was routed to '{RouteToPerson}'


Scenario: Verify Catalog Rule Routes Rejection To Team
	Given I am logged in to CRM
		And I find a Service Catalog that routes rejection to Team
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region                 | Country                 | State/Province        | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
		And I reject the Service Request
		And I wait for the Browser to go Idle
	Then I verify that the Service Request was routed to '{RouteToTeam}'

Scenario: Verify Catalog Rule Routes Rejection To Person
	Given I am logged in to CRM
		And I find a Service Catalog that routes rejection to Person
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region                 | Country                 | State/Province        | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
		And I reject the Service Request
		And I wait for the Browser to go Idle
	Then I verify that the Service Request was routed to '{RouteToPerson}'

# Validate that when SR should submitted without applying routing rule, it should be land in "Submitted Stage" of business process workflow.
Scenario: Verify Catalog Rule Without Routing
	Given I am logged in to CRM
		And I find a Service Catalog that has no routing
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region                 | Country                 | State/Province        | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I verify that the Business Process Flow stage is 'Submitted'
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
		And I verify that the Service Request owner is '{LegalProUser1}'		