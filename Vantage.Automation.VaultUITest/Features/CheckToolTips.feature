Feature: CheckToolTips

@tc-60156
Scenario: Check Clause Tool Tips
	Given I am logged in to CRM
	When I go to create New Clause
		And I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package           | Agreement File           | Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable | Frequency         |
		| Obligation Service Item | Recurring           | {ClauseFrequency} |
	Then I verify the following field tooltips contain
	| Customer Approval Required          | SLA                                                      | Topic and Description			   |
	| customer approval of the obligation | whether the obligation has a corresponding service level | Topic in brief captures the heading |
	And I verify the following field tooltips contain
	| Type of Deliverable      | Phase				  | Start Date					 | Frequency				  | Responsible Party          |
	| Automated categorization | Phase of performance | The next upcoming start date | periodicity of performance | responsible for fulfilling |
	And I verify the following field tooltips contain
	| Obligation Owner                                 | Obligation Owner Email ID                        |
	| the person responsible for reporting deliverable | the person responsible for reporting deliverable |
	And I verify the following field tooltips contain
	| Escalation Owner                         | Escalation Owner Email ID                |
	| person that should receive an escalation | person that should receive an escalation |
