Feature: ServiceRequestAssignment

# 40 Validate SR should be assigned to Legal Pro only 
# 43 Validate that a triage user should assign the new SR to legal pro/job jar
# 44 Validate that a Triage User should only be able to assign a single (i.e. primary) assignee to a service request.
# 66 The service request should move to the Assigned stage when Triage assigns the service request to a legal professional
Scenario: Verify Triage User Assignments
	Given I am logged in to CRM with Triage
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type                              | Region                 | Country                 | State/Province        | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
	When I assign the Service Request to '{LegalProUser1}'
		And I save the Service Request
		And I ignore the duplicate popup
	Then I verify that the Service Request owner is '{LegalProUser1}'
		And I verify that the Business Process Flow stage is 'Assigned'
		And I wait for the Browser to go Idle
	When I assign the Service Request to '{TriageUser1}'
		And I save the Service Request
		And I ignore the duplicate popup
	Then I verify that the Service Request owner is '{TriageUser1}'
		And I wait for the Browser to go Idle


# 46 Validate Legal Pro can reassign SR to Triage team users
# 51 Validate that a legal pro should reassign a SR assigned to him to a different legal pro on his team.
Scenario: Verify Legal Pro User Assignments
	Given I am logged in to CRM with Legal Pro
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type                              | Region                 | Country                 | State/Province        | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
	When I assign the Service Request to '{TriageUser1}'
		And I save the Service Request
		And I ignore the duplicate popup
	Then I verify that the Service Request owner is '{TriageUser1}'
		And I wait for the Browser to go Idle

	When I assign the Service Request to '{LegalProUser2}'
		And I save the Service Request
		And I ignore the duplicate popup
	Then I verify that the Service Request owner is '{LegalProUser2}'
		And I wait for the Browser to go Idle