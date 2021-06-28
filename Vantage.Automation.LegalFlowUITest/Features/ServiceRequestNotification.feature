Feature: ServiceRequestNotification

# 97 Validate Email is an available Activity type and can be created.
# 98 Validate The state of the email is closed when it's sent
Scenario: Create And Send Email
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type								 | Region				  | Country			        |  State/Province  	    | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
	When I select the Service Request Activities tab
		And I wait for the Browser to go Idle
		And I add an Email to the Service Request with the following properties
		| Subject |
		| test    |
		And I set the email body to 'foobar'
		And I send the Email
		And I wait for the Browser to go Idle
	When I switch the Grid view to 'All Activities'
		And I wait for the Browser to go Idle and wait 10 seconds
	Then I verify that the Grid contains records with the following properties
	| subject | activitytypecode | statecode |
	| test    | Email            | Completed |

# 100 Validate user push a specific document to signature tool so that he can retrieve and manage signatures electronically
Scenario: Push Document To Signature Tool
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type								 | Region				  | Country			        |  State/Province  	    | Related Opportunity		  |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I attach a private document to the Service Request with name 'test.pdf' and size 1024
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
	When I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I select Next Stage from 'Assigned'
		And I wait for the Browser to go Idle
		And I click Request Signature
		And I select Signature Document 'test.pdf'
		And I Save the Entity
		And I wait for the Browser to go Idle
		And I add a Signee to the Signature Request with the following properties
		| Signee Email  | Signing Order | Signing Role |
		| test@test.com | 1             | Primary      |
		And I submit the Signature Request

# 101 Validate ~Initiate new BriefBox button exists only for Commercial SRs
# 103 Validate Related BriefBox field is located in Commercial section ('SR Catalog Specific section name' for Commercial Service Catalogs)
Scenario: Validate BriefBox Field And Button 
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type								 | Region				  | Country			        |  State/Province  	    | Related Opportunity		  |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
	When I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I select Next Stage from 'Assigned'
		And I wait for the Browser to go Idle
	Then I verify that the following fields are present
	| BriefBox ID |
	| true        |
		And I verify that I can click Initiate BriefBox
		And I ignore the duplicate popup
		And I wait for the Browser to go Idle
