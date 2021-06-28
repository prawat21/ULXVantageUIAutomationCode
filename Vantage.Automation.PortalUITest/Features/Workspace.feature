Feature: Workspace

# 63 Validate there's ability to switch from Tile view to List view and back
Scenario: Verify Tile View And List View
	Given I am logged in to Portal
	When I go to Service Requests

	When I click the Table View Button
	Then I verify that at least 1 Table Rows are present

	When I click the Tiles View Button
	Then I verify that at least 1 Tiles are present

	When I click the Table View Button
	Then I verify that at least 1 Table Rows are present
