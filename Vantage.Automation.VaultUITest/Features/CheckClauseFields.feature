Feature: CheckClauseFields

@tc-58328
Scenario: Check Clause Fields For Milestone
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement        | Clause Text           | Agreement Package       | Agreement File       | Clause Type |
		| {Agreement_Gold} | TestClause{Timestamp} | {AgreementPackage_Gold} | {AgreementFile_Gold} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package       |
		| {AgreementPackage_Gold} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags |
		| Milestone	          |
	Then The following fields are present on New Clause form:
	| Values                                  |
	| AI - Review Tags                        |
	| Type of Deliverable                     |
	| Customer Approval Required              |
	| Start Date                              |
	| SLA                                     |
	| Functional Owner                        |
	| Responsible Party                       |
	| Immediate Financial Impact Upon Failure |

	
@tc-58332
Scenario: Check Clause Fields For CDL
Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement        | Clause Text           | Agreement Package       | Agreement File       | Clause Type |
		| {Agreement_Gold} | TestClause{Timestamp} | {AgreementPackage_Gold} | {AgreementFile_Gold} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package       |
		| {AgreementPackage_Gold} |
		And I fill in the following fields on New Clause form:
		| Agreement Package       |
		| {AgreementPackage_Gold} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags |
		| CDL Eligible        |
	Then The following fields are present on New Clause form:
	| Values                                  |
	| AI - Review Tags                        |
	| Type of Deliverable                     |
	| Customer Approval Required              |
	| Start Date                              |
	| SLA                                     |
	| Functional Owner                        |
	| Responsible Party                       |
	| Immediate Financial Impact Upon Failure |
	| Currency                                |
	When I fill in the following fields on New Clause form:
	| Type of Deliverable |
	| Recurring           |
	Then The following fields are present on New Clause form:
	| Values    |
	| Frequency |
	
@tc-58396
Scenario: Check Clause Fields For Obligation Service Item
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package          | Agreement File          | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     |
		| Obligation Service Item |
	Then The following fields are present on New Clause form:
	| Values                                  |
	| AI - Review Tags                        |
	| Tracking Type                           |
	| Phase		                              |
	| Customer Approval Required              |
	| Start Date                              |
	| SLA                                     |
	| Functional Owner                        |
	| Responsible Party                       |
	| Immediate Financial Impact Upon Failure |
	| Currency                                |
	When I fill in the following fields on New Clause form:
	| Tracking Type |
	| Monitored     |
	Then The following fields are present on New Clause form:
	| Values					|
	| Escalation Owner			|
	| Escalation Owner Email ID |
	