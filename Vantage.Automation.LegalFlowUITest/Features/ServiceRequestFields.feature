Feature: ServiceRequestFields

# 2	Validate while creating request all the fields are displayed
# 19 Validate tags functionality should be available for all the types of service requests for Legal Pro
Scenario: Verify Service Request Fields
	Given I am logged in to CRM
	When I go to create a new Service Request
	Then I verify that the following fields are present
	| Title | Description | Legal Professional Tags  |
	| true  | true        | true					 |
		And I verify that the following fields are present
		| Is this for someone else? | Related Practice Area | Client, Vendor, or Supplier | Related Matter |
		| true                      | true                  | true                        | true           |
		And I verify that the following fields are present
		| Priority | Respond By | Complete By |
		| true     | true       | true        |
		And I verify that the following fields are present
		| Request Type | Requested Resource | Privileged | Restricted? |
		| true         | true               | true       | true        |
		And I verify that the following fields are present
		| Region | Language Support |
		| true   | true             |
	When I fill out the following fields
	| Request Type |
	| Litigation   |
		And I wait for the Browser to go Idle
	Then I verify that the following fields are present
	| Sub Type |
	| true     |
	When I fill out the following fields
	| Region                 |
	| {ServiceRequestRegion} |
		And I wait for the Browser to go Idle
	Then I verify that the following fields are present
	| Country | State/Province |
	| true    | false          |
	When I fill out the following fields
	| Country                 |
	| {ServiceRequestCountry} |
		And I wait for the Browser to go Idle
	Then I verify that the following fields are present
	| State/Province |
	| true           |
	And I verify that the following fields are required
	| Title  | Description |
	| true   | true        |
		And I verify that the following fields are required
		| Is this for someone else? | Client, Vendor, or Supplier | 
		| true                      | false                       |
		And I verify that the following fields are required
		| Priority | Respond By | Complete By |
		| false    | false      | true        |
		And I verify that the following fields are required
		| Request Type | Sub Type | Requested Resource | Privileged | Restricted? |
		| true         | true     | false              | false      | false       |
		And I verify that the following fields are required
		| Region | Country | State/Province | Language Support |
		| true   | true    | true           | false            |
		And I save the Service Request
		And I verify Service Request cannot be submitted
		# fill out things
		And I fill out the following fields
		| Title                    | Description | Complete By        | Sub Type    | Country				  | State/Province        |
		| {RandomUserName}-{Lorem} | {Lorem}     | {RandomFutureDate} | Arbitration | {ServiceRequestCountry} | {ServiceRequestState} |
		And I save the Service Request
		And I ignore the duplicate popup
		And I submit the Service Request
	Then I verify that the Service Request has a Request ID

# 82 Validate that system should display the tool tips next to the SR field "Requested Resource" so that the user can hover over the icon to view field definitions.
# 83 Validate that system should display the tool tips next to the SR field "Country" so that the user can hover over the icon to view field definitions.
# 84 Validate that system should display the tool tips next to the SR field "State/Province of Support Need" so that the user can hover over the icon to view field definitions.
# 85 Validate that system should display the tool tips next to the SR field "Respond By" so that the user can hover over the icon to view field definitions.
Scenario: Verify Service Request Field Tool Tips
	Given I am logged in to CRM
	When I go to create a new Service Request
	When I fill out the following fields
	| Region			     | Country				   |
	| {ServiceRequestRegion} | {ServiceRequestCountry} |
	Then I verify the following field tooltips contain
	| Region                    | Country                 | State/Province          | Respond By                          | Complete By                  | Requested Resource           |
	| Identify the region where | Be specific about where | Be specific about where | This date defaults to your contract | Estimate when you would like | preferred Legal Professional |

Scenario: Verify Parent Service Request Required Fields And Section
	Given I am logged in to CRM
			And I find a Parent Service Catalog that has mandatory fields and section
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Request Type  |
		| {RequestType} |
		And I wait for the Browser to go Idle
	Then I verify that the Service Catalog required fields are present
	And I verify that the Service Catalog specific sections are present

Scenario: Verify Child Service Request Required Fields And Section
	Given I am logged in to CRM
			And I find a Child Service Catalog that has mandatory fields and section
	When I go to create a new Service Request
		And I fill out the following fields for Service Catalog
		| Request Type  | Sub Type  |
		| {RequestType} | {SubType} |
		And I wait for the Browser to go Idle
	Then I verify that the Service Catalog required fields are present
	And I verify that the Service Catalog specific sections are present