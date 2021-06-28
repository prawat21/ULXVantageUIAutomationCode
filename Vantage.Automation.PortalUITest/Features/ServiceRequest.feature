Feature: ServiceRequest

Scenario: Create A Commerical Facilities Service Request
	Given I am logged in to Portal
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Request Type | Sub Type                              | Region                 | Country                 | State                 | Complete By               |
		| {RandomUserName}-{Lorem} | {Lorem}     | Commercial   | Facilities and Real Estate Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {RandomFutureDateFormat2} |
		And I submit the Service Request
	Then I verify that the Service Request was submitted

# 38 Validate that as a part of sales process all the related Individual Agreements/Work Items should be submit at a time.
# 61 Validate that drop-down use caret instead of filled arrows
Scenario: Create A Commerical Sales Agreement Service Request
	Given I am logged in to Portal
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Request Type | Sub Type         | Region                 | Country                 | State                 | Complete By               |
		| {RandomUserName}-{Lorem} | {Lorem}     | Commercial   | Sales Agreements | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {RandomFutureDateFormat2} |
		And I attach a document to the Service Request
		And I fill out the following fields
		| Account Name | Deal Value | Expected Close Date | Opportunity Number | Product/Service | Sector               | Customer Type    | Document Request Type |
		| Test QA      | 123        | {RandomFutureDate}  | 23456              | test            | Government All Other | Other Collateral | Other                 |
		And I submit the Service Request
	Then I verify that the Service Request was submitted
		And I verify the following field values are
		| Account Name | Deal Value | Opportunity Number | Product/Service | Sector             | Customer Type    | Document Request Type |
		| Test QA      | $123       | 23456              | test            | GovernmentAllOther | Other Collateral | Other                 |

# 12 Validate that as a part of sales process all the related Individual Agreements/Work Items should be submit at a time
Scenario: Create An Intellectual Property Service Request
	Given I am logged in to Portal
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Request Type           | Sub Type                        | Region                 | Country                 | State                 | Complete By               |
		| {RandomUserName}-{Lorem} | {Lorem}     | Intellectual Property  | Copyrights Portfolio Management | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {RandomFutureDateFormat2} |
		And I fill out the following fields
		| Technology Area | Committee |
		| Other           | Video     |
		And I submit the Service Request
	Then I verify that the Service Request was submitted
		And I verify the following field values are
		| Technology Area | Committee |
		| Other           | Video     |

# 39 Validate that the SR additional data fields should be updated on CRM with the generic fields
# 18 Verify that a requestor should need to involve legal and specifically compliance during data breach/incident response activities.
Scenario: Create An Ethics Service Request
	Given I am logged in to Portal
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Request Type | Sub Type                 | Region                 | Country                 | State                 | Complete By               |
		| {RandomUserName}-{Lorem} | {Lorem}     | Ethics       | File / Request Complaint | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {RandomFutureDateFormat2} |
		And I fill out the following fields
		| File/Complaint Type | File/Complaint Sub-Type |
		| Trade control       | Trade                   |
		And I attach a document to the Service Request
		And I verify that the Service Request is privileged
		And I submit the Service Request
	Then I verify that the Service Request was submitted
		And I verify the following field values are
		| File/Complaint Type | File/Complaint Sub-Type |
		| Trade control       | Trade                   |

Scenario: Create A Data Privacy And Security Request
	Given I am logged in to Portal
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Request Type				 | Sub Type             | Region                 | Country                 | State                 | Complete By               |
		| {RandomUserName}-{Lorem} | {Lorem}     | Data Privacy and Security | Data Subject Request | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} | {RandomFutureDateFormat2} |
		And I fill out the following fields
		| I am a(an)       | Request Type        | Date of Original Request |
		| Current Employee | Data Access Request | {RandomFutureDate}       |
		And I submit the Service Request
	Then I verify that the Service Request was submitted
		And I verify the following field values are
		| I am a(an)       | Request Type        |
		| Current Employee | Data Access Request |