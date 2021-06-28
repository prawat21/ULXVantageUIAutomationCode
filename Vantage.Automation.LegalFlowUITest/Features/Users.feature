Feature: Users

# 22 Validate a primary practice area should be captured on the legal professional (Admin-user) profile
# 41 Practice Area is shown on the page of Legal Pro 
Scenario: Verify a Legal Professional User
	Given I am logged in to CRM
	When I go to view Active Users
		And I open User '{LegalProUser1}'

	When I add a Qualification to the User with the following properties
	| Name | Entity       |
	| test | Dynamics 365 |
	And I add a Language to the User with the following properties
	| Language |
	| English  |
	And I add an Education to the User with the following properties
	| Year | Degree Type | University | University Location |
	| 2000 | MBA         | Harvard    | Massachusetts       |
	#Then I verify that the user has a Practice Area
		Then I verify that the user has at least 1 Language Skills
		And I verify that the user has at least 1 Education Skills
		And I verify that the user has at least 1 Qualification Skills

# 48 Validate user can generate report
# 49 Validate user can export search results
Scenario: Verify Export User Search Results And Running Report
	Given I am logged in to CRM
	When I go to view Active Users
	Then I verify CommandBar button 'Export to Excel' is not present
		And I verify that I can run User Activities Report

# 42 Validate that the triage user should browse the inventory of legal professionals to make service requests assignments.
Scenario: Verify Triage User Can Browse Legal Professionals
	Given I am logged in to CRM with Triage
	When I go to view Active Users
