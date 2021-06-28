Feature: ServiceRequest

# 1	Validate the Unique Service Request ID upon submission of request
# 3	Validate after filling all the mandatory information the service request should be generated successfully
# 4	Validate while clicking on submit button confirmation screen message should be displayed.
# 6	Validate that legal professional should populates service request fields.
# 10 Validate that while legal pro should create a service request system should auto-populates "Priority" field with "Standard"
# 75 Validate Legal Pro can accept SR
Scenario: Submit and Accept A Service Request
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type                              | Region                 | Country                 | State/Province        | Related Opportunity		  |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I verify the following field values contain
		| Priority |
		| Standard |

	When I wait for the Browser to go Idle
		And I refresh the Service Request
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
		And I accept the Service Request
		And I refresh the Service Request
	Then I verify that the Service Request has been accepted

# 73 Validate that user can reject SR
# 74 Validate that user can select Reject SR with Reject reason from the list 
# 77 Validate Rejected SR should be routed to Person/Team
Scenario: Reject A Service Request And Verify It Is Rejected
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type								 | Region				  | Country			        |  State/Province  	    | Related Opportunity		  |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I verify the following field values contain
		| Priority |
		| Standard |
	When I wait for the Browser to go Idle
		And I refresh the Service Request
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
		And I reject the Service Request
		And I refresh the Service Request
	Then I verify that the Service Request has been rejected

# 71 Validate that user can mark SR as a duplicate via rejection and choose rejection reason of 'merged'
Scenario: Reject A Service Request With Duplicate Reason
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type								 | Region				  | Country			        |  State/Province  	    | Related Opportunity		  |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
	When I wait for the Browser to go Idle
		And I refresh the Service Request
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
		And I reject the Service Request with Other Reason 'Duplicate'
		And I refresh the Service Request
	Then I verify that the Service Request has been rejected

# 65 Validate Legal Pro should be able to manually update stage to Completed upon completing the work.
# 70 Validate Date & Time and User who changes Stage from "Assigned" to "Active will be captured in fields "Accepted By" and "Accepted On" (not on form)
# 89 Validate SR should go from Completed to Finished stage after Requestor manually closes SR on Portal
# 91 Validate that once Legal pro should marks service request as completed, SR moves from Active phase to Completed phase.
Scenario: Complete A Service Request And Verify It Is Completed
	Given I am logged in to CRM
		And I find a Service Catalog without a Sub Status
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region                 | Country				 | State/Province	     | Related Opportunity		   |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I select Next Stage from 'Assigned'
		And I select Next Stage from 'Active'
		And I ignore the duplicate popup
		And I wait for the Browser to go Idle
	Then I verify that the Service Request has been completed

	When I Confirm Completion of the Service Request
		And I wait for the Browser to go Idle and wait 30 seconds
		And I refresh the Service Request
	Then I verify that Service Request status contains 'Completed'
	When I select the Service Request Activities tab
		And I switch the Grid view to 'All Activity Associated View'
		And I wait for the Browser to go Idle and wait 5 seconds
	Then I verify that the Grid contains records with the following properties
	| subject						   | activitytypecode			  | statecode |
	| Feedback to the Legal Department | Customer Voice survey invite | Open      |

# 9 Validate that after populating all the fields and clicks on "save and close" button, legal pro should clicks on "submit" button
# 72 Validate that a legal pro should have the ability to Mark Service Request as "Privileged".
Scenario: Save And Close And Submit A Service Request
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type								 | Region                 | Country                 | State/Province        | Privileged | Related Opportunity		   |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | true       | {ServiceRequestOpportunity} |
		And I save and close the Service Request
		And I ignore the duplicate popup
		And I wait for the Browser to go Idle
		And I click the Browser forward button
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I verify that Business Process Flow exists
		And I verify the following field values contain
		| Privileged |
		| True       |

# 60 Validate a triage should validate SR by review all the information provided by Requestor.
# 87 Validate the service request should move to Triage stage when a user with the role of Triage first clicks into the service request
Scenario: Triage User Can View Service Request
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type								 | Region                 | Country                 | State/Province        | Related Opportunity	      |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I save the Browser Url
		And I close the browser
	Given I am logged in to CRM with Triage
	When I navigate to the saved Browser Url
		And I wait for the Browser to go Idle
	Then I verify the following field values contain
	| Title   | Description   | Request Type   | Sub Type   | Region   | Country   | State/Province   | Related Opportunity   |
	| [Title] | [Description] | [Request Type] | [Sub Type] | [Region] | [Country] | [State/Province] | [Related Opportunity] |
		And I verify that Service Request status contains 'Active'
		And I verify that the Business Process Flow stage is 'Triage'
	
Scenario: Clone A Service Request
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type								 | Region                 | Country				    | State/Province	    | Related Opportunity		  |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I select Next Stage from 'Assigned'
		And I wait for the Browser to go Idle
	When I clone the Service Request
		And I wait for the Browser to go Idle
		And I go to All Service Requests
		And I search the Grid for the cloned Service Request
	Then I verify that the Grid has at least 1 records
	When I open the Grid record at index 0
	Then I verify the following field values contain	
	| Description   | Request Type   | Sub Type   | Region   | Country   | State/Province   | Related Opportunity   |
	| [Description] | [Request Type] | [Sub Type] | [Region] | [Country] | [State/Province] | [Related Opportunity] |