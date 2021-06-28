Feature: CheckAccessTeamMembersGrid

Background:
	Given I am logged in to CRM

@tc-63310
Scenario: Check Access Team Members Grid Is Present For Account
	When I open 'Accounts' from left navigation menu
		And I open the grid record at index '1'
	Then 'Access Team Members' tab is 'present' on entity page

	When I open 'Access Team Members' tab
	Then Access Team Members Subgrid header label is null on Entity page

@tc-63310 @tc-63500
Scenario: Check Access Team Members Grid Is Present For Agreement Package
	When I open 'Agreement Packages' from left navigation menu
		And I open the grid record at index '1'
	Then 'Access_Team_Members' Subgrid is not present on entity page
		And 'Access Team Members' tab is 'present' on entity page

	When I open 'Access Team Members' tab
	Then Access Team Members Subgrid header label is null on Entity page

@tc-63310
Scenario Outline: Check Access Team Members Grid Is Not Present
	When I open '<entity>' from left navigation menu
		And I open the grid record at index '1'
	Then 'Access Team Members' tab is 'absent' on entity page
		And 'Confidential Team Members' tab is 'absent' on entity page

	Examples:
	| entity          |
	| Agreements      |
	| Agreement Files |