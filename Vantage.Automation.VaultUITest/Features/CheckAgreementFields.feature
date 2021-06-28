Feature: CheckAgreementFields

@tc-56192
Scenario: Check Agreement Fields
	Given I am logged in to CRM
	When I open All Agreements view
		And I search for entity with name '{AgreementForUpload}'
		And I open agreements grid record with name '{AgreementForUpload}'
	Then The following fields are present on Agreement form:
	| Values                               |
	| Agreement Name                       |
	| Vault Account                        |
	| Agreement Package                    |
	| Family Tree Established              |
	| Vault Service Request                |
	| Originating Service Request          |
	| Agreement Type                       |
	| Agreement Category                   |
	| Agreement Language                   |
	| Contract Geographic scope            |
	| Governing Law                        |
	| Internal Legal Entity                |
	| Counterparty Legal Entity            |
	| Counterparty Region                  |
	| Counterparty address                 |
	| Counterparty Notice Address          |
	| Counterparty Agreement #/PO          |
	| Renewal Notice Period                |
	| Renewal Notice Unit                  |
	| Standard Payment Terms               |
	| Vault Total Opportunity Value in USD |
	| Legal Reviewed                       |
	And The following fields are present on Agreement form:
	| Values        |
	| Attested      |
	| Attested By   |
	| Signature ID  |
	| Attested Date |
	And The following fields are present on Agreement form:
	| Values                      |
	| Signature Effective Date    |
	| Original Expiration Date    |
	| Original Service Start Date |
	| Revised Expiration Date     |
	| Expiration Date Type        |
	| Extend Expiration To        |
	And The following fields are present on Agreement form:
	| Values                |
	| NDA Disclosure Period |
	| NDA Disclosure Unit   |
	| NDA Protection Period |
	| NDA Protection Unit   |
	| NDA Purpose           |
