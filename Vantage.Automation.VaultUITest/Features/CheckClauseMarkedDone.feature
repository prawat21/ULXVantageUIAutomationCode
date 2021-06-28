Feature: CheckClauseMarkedDone

@tc-64785
Scenario: 01 Bronze Obligation Service Item One Time
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Bronze} | TestClause{Timestamp} | {AgreementPackage_Bronze} | {AgreementFile_Bronze} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Bronze} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner        | Responsible Party		 | Tracking Type		|
		| {ClausePhase} | true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | {ClauseTrackingType} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 02 Bronze Obligation Service Item Recurring
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Bronze} | TestClause{Timestamp} | {AgreementPackage_Bronze} | {AgreementFile_Bronze} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Bronze} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | Recurring           |
		And I fill in the following fields on New Clause form:
		| Phase  | Customer Approval Required | SLA  | Functional Owner | Responsible Party | Tracking Type | Frequency |
		| Steady | true                       | true | Legal            | Vendor Ownership  | Alerted       | Weekly    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 03 Bronze CDL Eligible One Time
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Bronze} | TestClause{Timestamp} | {AgreementPackage_Bronze} | {AgreementFile_Bronze} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Bronze} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | One-Time            |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner | Responsible Party |
		| true                       | true | Legal            | Vendor Ownership  |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 04 Bronze CDL Eligible Recurring
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Bronze} | TestClause{Timestamp} | {AgreementPackage_Bronze} | {AgreementFile_Bronze} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Bronze} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable | Frequency |
		| CDL Eligible        | Recurring           | Weekly    |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner | Responsible Party |
		| true                       | true | Legal            | Vendor Ownership  |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 05 Bronze Milestone
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Bronze} | TestClause{Timestamp} | {AgreementPackage_Bronze} | {AgreementFile_Bronze} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Bronze} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| Milestone           | One-Time            |
		And I fill in the following fields on New Clause form:
		| Functional Owner | Responsible Party | Customer Approval Required | SLA  |
		| Legal            | Vendor Ownership  | true                       | true | 
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 06 Silver Obligation Service Item
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Silver} | TestClause{Timestamp} | {AgreementPackage_Silver} | {AgreementFile_Silver} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Silver} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner        | Responsible Party        | Immediate Financial Impact Upon Failure | Tracking Type        | Financial Impact        |
		| {ClausePhase} | true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | Low                                     | {ClauseTrackingType} | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 06 Gold Obligation Service Item
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement        | Clause Text           | Agreement Package		 | Agreement File	    | Clause Type |
		| {Agreement_Gold} | TestClause{Timestamp} | {AgreementPackage_Gold} | {AgreementFile_Gold} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package       |
		| {AgreementPackage_Gold} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type        |	Financial Impact        |
		| {ClausePhase} | true					     | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | Low                                     | {ClauseTrackingType} | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 07 Silver Obligation Service Item None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Silver} | TestClause{Timestamp} | {AgreementPackage_Silver} | {AgreementFile_Silver} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Silver} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Phase                     | Customer Approval Required | SLA  | Functional Owner | Responsible Party        | Immediate Financial Impact Upon Failure | Tracking Type			   |
		| {ClausePhase} | true                       | true | {ClauseFunctionalOwner}      | {ClauseResponsibleParty} | None									| {ClauseTrackingType}     |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 07 Gold Obligation Service Item None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement        | Clause Text           | Agreement Package	     | Agreement File	    | Clause Type |
		| {Agreement_Gold} | TestClause{Timestamp} | {AgreementPackage_Gold} | {AgreementFile_Gold} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package       |
		| {AgreementPackage_Gold} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party        | Immediate Financial Impact Upon Failure | Tracking Type		  |
		| {ClausePhase} | true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    | {ClauseTrackingType} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 08 Silver Obligation Service Item Low
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Silver} | TestClause{Timestamp} | {AgreementPackage_Silver} | {AgreementFile_Silver} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Silver} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | Recurring           |
		And I fill in the following fields on New Clause form:
		| Phase         | Frequency | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type	      | Financial Impact        |
		| {ClausePhase} | Weekly    | true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | Low                                     | {ClauseTrackingType} | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 08 Gold Obligation Service Item Low
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement        | Clause Text           | Agreement Package       | Agreement File       | Clause Type |
		| {Agreement_Gold} | TestClause{Timestamp} | {AgreementPackage_Gold} | {AgreementFile_Gold} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package        |
		| {AgreementPackage_Gold} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | Recurring           |
		And I fill in the following fields on New Clause form:
		| Phase         | Frequency | Customer Approval Required | SLA  | Functional Owner        | Responsible Party        | Immediate Financial Impact Upon Failure | Tracking Type        | Financial Impact        |
		| {ClausePhase} | Weekly    | true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | Low                                     | {ClauseTrackingType} | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 09 Silver Obligation Service Item None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Silver} | TestClause{Timestamp} | {AgreementPackage_Silver} | {AgreementFile_Silver} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Silver} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | Recurring           |
		And I fill in the following fields on New Clause form:
		| Phase         | Frequency | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type		  |
		| {ClausePhase} | Weekly    | true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    | {ClauseTrackingType} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 09 Gold Obligation Service Item None
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
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | Recurring           |
		And I fill in the following fields on New Clause form:
		| Phase         | Frequency | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type		  |
		| {ClausePhase} | Weekly    | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    | {ClauseTrackingType} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 10 Silver CDL Eligible High
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Silver} | TestClause{Timestamp} | {AgreementPackage_Silver} | {AgreementFile_Silver} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Silver} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | One-Time            |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner        | Responsible Party        | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 10 Gold CDL Eligible High
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
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | One-Time            |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 11 Silver CDL Eligible None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Silver} | TestClause{Timestamp} | {AgreementPackage_Silver} | {AgreementFile_Silver} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Silver} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | One-Time            |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 11 Gold CDL Eligible None
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
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | One-Time            |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 12 Silver CDL Eligible High
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Silver} | TestClause{Timestamp} | {AgreementPackage_Silver} | {AgreementFile_Silver} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Silver} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable | Frequency |
		| CDL Eligible        | Recurring           | Weekly    |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 12 Gold CDL Eligible High
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
		| Human - Review Tags | Type of Deliverable | Frequency |
		| CDL Eligible        | Recurring           | Weekly    |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner        | Responsible Party        | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 13 Silver CDL Eligible None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Silver} | TestClause{Timestamp} | {AgreementPackage_Silver} | {AgreementFile_Silver} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Silver} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable | Frequency |
		| CDL Eligible        | Recurring           | Weekly    |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 13 Gold CDL Eligible None
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
		| Human - Review Tags | Type of Deliverable | Frequency |
		| CDL Eligible        | Recurring           | Weekly    |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 14 Silver Milestone High
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Silver} | TestClause{Timestamp} | {AgreementPackage_Silver} | {AgreementFile_Silver} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Silver} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| Milestone           | One-Time            |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 14 Gold Milestone High
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
		| Human - Review Tags | Type of Deliverable |
		| Milestone           | One-Time            |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 15 Silver Milestone None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement          | Clause Text           | Agreement Package         | Agreement File         | Clause Type |
		| {Agreement_Silver} | TestClause{Timestamp} | {AgreementPackage_Silver} | {AgreementFile_Silver} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package         |
		| {AgreementPackage_Silver} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| Milestone           | One-Time            |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 15 Gold Milestone None
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
		| Human - Review Tags | Type of Deliverable |
		| Milestone           | One-Time            |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 16 Platinum Obligation Service Item High
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      | Escalation Owner        | Escalation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} | {ClauseEscalationOwner} | {ClauseEscalationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type		  | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseTrackingType} | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 16 Diamond Obligation Service Item High
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      | Escalation Owner        | Escalation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} | {ClauseEscalationOwner} | {ClauseEscalationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type		  | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseTrackingType} | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 17 Platinum Obligation Service Item High Alerted
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | Alerted	   | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 17 Diamond Obligation Service Item High Alerted
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | Alerted	   | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 18 Platinum Obligation Service Item None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      | Escalation Owner        | Escalation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} | {ClauseEscalationOwner} | {ClauseEscalationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type		  | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    | {ClauseTrackingType} | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 18 Diamond Obligation Service Item None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      | Escalation Owner        | Escalation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} | {ClauseEscalationOwner} | {ClauseEscalationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type		  | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    | {ClauseTrackingType} | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 19 Platinum Obligation Service Item None Alerted
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    | Alerted       | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 19 Diamond Obligation Service Item None Alerted
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable |
		| Obligation Service Item | One-Time            |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    | Alerted       | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 20 Platinum Obligation Service Item Recurring High
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable | Frequency         |
		| Obligation Service Item | Recurring           | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      | Escalation Owner        | Escalation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} | {ClauseEscalationOwner} | {ClauseEscalationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type		  | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseTrackingType} | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 20 Diamond Obligation Service Item Recurring High
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable | Frequency         |
		| Obligation Service Item | Recurring           | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      | Escalation Owner        | Escalation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} | {ClauseEscalationOwner} | {ClauseEscalationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type		  | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseTrackingType} | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 21 Platinum Obligation Service Item Recurring High Alerted
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable | Frequency         |
		| Obligation Service Item | Recurring           | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | Alerted       | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 21 Diamond Obligation Service Item Recurring High Alerted
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable | Frequency         |
		| Obligation Service Item | Recurring           | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type | Financial Impact        |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | Alerted       | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 22 Platinum Obligation Service Item Recurring None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable | Frequency         |
		| Obligation Service Item | Recurring           | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      | Escalation Owner        | Escalation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} | {ClauseEscalationOwner} | {ClauseEscalationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type		  |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    | {ClauseTrackingType} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 22 Diamond Obligation Service Item Recurring None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable | Frequency         |
		| Obligation Service Item | Recurring           | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      | Escalation Owner        | Escalation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} | {ClauseEscalationOwner} | {ClauseEscalationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type		  |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    | {ClauseTrackingType} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 23 Platinum Obligation Service Item Recurring None Alerted
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable | Frequency         |
		| Obligation Service Item | Recurring           | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    | Alerted	   |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 23 Diamond Obligation Service Item Recurring None Alerted
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags     | Type of Deliverable | Frequency         |
		| Obligation Service Item | Recurring           | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Topic and Description       | Start Date         | Obligation Owner        | Obligation Owner Email ID      |
		| {ClauseTopicAndDescription} | {RandomFutureDate} | {ClauseObligationOwner} | {ClauseObligationOwnerEmailId} |
		And I fill in the following fields on New Clause form:
		| Phase         | Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Tracking Type |
		| {ClausePhase} | true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    | Alerted	   |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 24 Platinum CDL Eligible High
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | One-Time            |
		And I fill in the following fields on New Clause form:
		| Start Date         |
		| {RandomFutureDate} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 24 Diamond CDL Eligible High
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | One-Time            |
		And I fill in the following fields on New Clause form:
		| Start Date         |
		| {RandomFutureDate} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 25 Platinum CDL Eligible None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | One-Time            |
		And I fill in the following fields on New Clause form:
		| Start Date         |
		| {RandomFutureDate} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure |
		| true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 25 Diamond CDL Eligible None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | One-Time            |
		And I fill in the following fields on New Clause form:
		| Start Date         |
		| {RandomFutureDate} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure |
		| true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 26 Platinum CDL Eligible High Recurring
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | Recurring           |
		And I fill in the following fields on New Clause form:
		| Start Date         | Frequency         |
		| {RandomFutureDate} | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 26 Diamond CDL Eligible High Recurring
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | Recurring           |
		And I fill in the following fields on New Clause form:
		| Start Date         | Frequency         |
		| {RandomFutureDate} | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 27 Platinum CDL Eligible None Recurring
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | Recurring           |
		And I fill in the following fields on New Clause form:
		| Start Date         | Frequency         |
		| {RandomFutureDate} | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | 
		| true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 27 Diamond CDL Eligible None Recurring
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| CDL Eligible        | Recurring           |
		And I fill in the following fields on New Clause form:
		| Start Date         | Frequency         |
		| {RandomFutureDate} | {ClauseFrequency} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner		  | Responsible Party		 | Immediate Financial Impact Upon Failure | 
		| true						 | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 28 Platinum Milestone High
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| Milestone	          | One-Time            |
		And I fill in the following fields on New Clause form:
		| Start Date         | Impact/Consequences        |
		| {RandomFutureDate} | {ClauseImpactConsequences} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner        | Responsible Party        | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 28 Diamond Milestone High
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| Milestone           | One-Time            |
		And I fill in the following fields on New Clause form:
		| Start Date         | Impact/Consequences        |
		| {RandomFutureDate} | {ClauseImpactConsequences} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner        | Responsible Party        | Immediate Financial Impact Upon Failure | Financial Impact        |
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | High                                    | {ClauseFinancialImpact} |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 29 Platinum Milestone None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement            | Clause Text           | Agreement Package			 | Agreement File			| Clause Type |
		| {Agreement_Platinum} | TestClause{Timestamp} | {AgreementPackage_Platinum} | {AgreementFile_Platinum} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package           |
		| {AgreementPackage_Platinum} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| Milestone           | One-Time            |
		And I fill in the following fields on New Clause form:
		| Start Date         | Impact/Consequences        |
		| {RandomFutureDate} | {ClauseImpactConsequences} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner        | Responsible Party        | Immediate Financial Impact Upon Failure | 
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete

@tc-64785
Scenario: 29 Diamond Milestone None
	Given I am logged in to CRM
	When I go to create New Clause
		When I fill in the following fields on New Clause form:
		| Agreement           | Clause Text           | Agreement Package		   | Agreement File		     | Clause Type |
		| {Agreement_Diamond} | TestClause{Timestamp} | {AgreementPackage_Diamond} | {AgreementFile_Diamond} | Deliverable |
		And I click 'Save' on Command Bar
		And I fill in the following fields on New Clause form:
		| Agreement Package          |
		| {AgreementPackage_Diamond} |
		And I fill in the following fields on New Clause form:
		| Human - Review Tags | Type of Deliverable |
		| Milestone           | One-Time            |
		And I fill in the following fields on New Clause form:
		| Start Date         | Impact/Consequences        |
		| {RandomFutureDate} | {ClauseImpactConsequences} |
		And I fill in the following fields on New Clause form:
		| Customer Approval Required | SLA  | Functional Owner        | Responsible Party        | Immediate Financial Impact Upon Failure | 
		| true                       | true | {ClauseFunctionalOwner} | {ClauseResponsibleParty} | None                                    |
		And I click 'Save' on Command Bar
	Then Clause is marked as Complete