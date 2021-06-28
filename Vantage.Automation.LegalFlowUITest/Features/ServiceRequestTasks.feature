Feature: ServiceRequestTasks

# 52 Validate that a legal pro should create sub tasks and assign them to different legal pro's within organization only. 
# 56 Validate Sub task should create by click on +New Task button.
# 57 Validate that multiple sub task should be assign to multiple people
Scenario: Add Tasks To Service Request
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type | Sub Type                              | Region                 | Country                 | State/Province        | Related Opportunity         |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {ServiceRequestOpportunity} |
		And I save the Service Request
		And I ignore the duplicate popup
	When I select the Service Request Tasks tab
		And I add a task to the Service Request with the following properties
		| Title   | Task Details | Start Date       | Due Date		     | People          |
		| {Lorem} | {Lorem}      | {RandomPastDate} | {RandomFutureDate} | {LegalProUser1} |
		And I add a task to the Service Request with the following properties
		| Title   | Task Details | Start Date		| Due Date           | People        |
		| {Lorem} | {Lorem}      | {RandomPastDate} | {RandomFutureDate} | {TriageUser1} |
		And I wait for the Browser to go Idle
	Then I verify that the SubGrid 'subgrid_tasks' contains records with the following properties
	| People          |
	| {LegalProUser1} |
	| {TriageUser1}   |
