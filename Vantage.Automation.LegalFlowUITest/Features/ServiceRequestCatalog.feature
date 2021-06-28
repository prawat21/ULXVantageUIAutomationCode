Feature: ServiceRequestCatalog

# 53 Validate updating of service catalog should update PRIVILEGED status of the SR
Scenario: Verify Updating Service Catalog Updates Privileged Status
	Given I am logged in to CRM
		And I find a Service Catalog that is not Privileged
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region                 | Country                 | State/Province        | Opportunity                 |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
	Then I verify that a warning notification containing text 'Privileged' is not present
		And I verify the following field values are
		| Privileged |
		| False      |
	When I find a Service Catalog that is Privileged
		And I fill out the following fields for Service Catalog
		| Request Type  | Sub Type  | Related Opportunity         |
		| {RequestType} | {SubType} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
	Then I verify that a warning notification containing text 'Privileged' is present
		And I verify the following field values are
			| Privileged |
			| True       |

# 96 Validate System calculates the number of documents attached to a SR record and the count is captured in a field called Document Count.
Scenario: Verify System Calculates Number Documents Attached
	Given I am logged in to CRM
		And I find a Service Catalog that requires Document
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region                 | Country                 | State/Province        | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I attach a private document to the Service Request with name 'test.pdf' and size 1024
		And I attach a public document to the Service Request with name 'test.jpg' and size 1024
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
		And I go to All Service Requests
		And I search the Grid for the created Service Request
	Then I verify that the Grid has at least 1 records
	When I set the Browser zoom level to 30
	Then I verify that the Grid record at index 0 contains the following properties
	| ulx_documentcount |
	| 2                 |

# 67 Validate that Legal Pro should have the ability to denote his SR with available Sub-statuses when the SR is in an Active Stage
Scenario: Verify Service Catalog With A Sub Status
	Given I am logged in to CRM
		And I find a Service Catalog with a Sub Status
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
		And I select Next Stage from 'Assigned'
		And I select Stage 'Active'
		And I fill out the following BFP fields for Service Catalog
		| Sub-Status  |
		| {SubStatus} |
		And I select Next Stage from 'Active'
		And I ignore the duplicate popup
		And I wait for the Browser to go Idle
		And I select Finish from 'Completed'
	Then I verify that the Service Request has been completed

# 108 Validate the SR is automatically assigned to the Director of Investigations if Administration/Investigation field = Investigation ….
Scenario: Verify Service Catalog With Director of Investigation
	Given I am logged in to CRM
		And I find a Service Catalog with Director of Investigation
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Title                    | Description | Complete By        | Request Type  | Sub Type  | Region                 | Country                 | State/Province        | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | {RequestType} | {SubType} | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I fill out the following fields for Service Catalog
		| File/Complaint Type   | File/Complaint Sub-Type | 
		| Conflicts of interest | External employment     |
		And I save the Service Request
		And I ignore the duplicate popup
		And I attach a private document to the Service Request with name 'test.pdf' and size 1024
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
	And I select Next Stage from 'Submitted'
		And I select Next Stage from 'Triage'
		And I wait for the Browser to go Idle
		And I fill out the following fields for Service Catalog
		| Adminstrative/Investigation |
		| Investigation               |
		And I save the Service Request
		And I ignore the duplicate popup
		And I select Next Stage from 'Assigned'		
	When I wait for the Browser to go Idle
	Then I verify that the Service Request was routed to '{RouteToPerson}'