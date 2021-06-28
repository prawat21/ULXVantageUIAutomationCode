Feature: ServiceRequestSpecificCatalog

Scenario: Verify Data Privacy Request Fields
	Given I am logged in to CRM
	When I go to create a new Service Request
		And I fill out the following fields
		| Title                    | Description | Complete By        | Request Type              | Sub Type			 | Region				  | Country			        |  State/Province  	    |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Data Privacy and Security | Data Subject Request | {ServiceRequestRegion} | {ServiceRequestCountry} | {ServiceRequestState} |
		And I fill out the following fields
		| I am a(an)       | Data Request Type   | Affiliate | Date of Original Request  |
		| Current Employee | Data Access Request | No        | {RandomPastDate}			 |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID
		And I verify that the following fields are present
        | Identity Verified |
        | true              |