Feature: Contacts

# 48 Validate user can generate report
# 49 Validate user can  search results
Scenario: Verify Export Contact Search Results And Running Report
	Given I am logged in to CRM
	When I go to view Active Contacts
	Then I verify CommandBar button 'Export to Excel' is not present
		And I verify that I can run Products By Contact Report