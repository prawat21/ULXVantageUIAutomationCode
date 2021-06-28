Feature: ServiceRequestTriage

# 64 Portal: Validate there should be three tabs of Service Request page: All, Active and Closed
Scenario: Verify Active and Closed and All Service Requests
	Given I am logged in to CRM with Triage
	When I go to Active Service Requests
	When I go to Closed Service Requests
	When I go to All Service Requests
