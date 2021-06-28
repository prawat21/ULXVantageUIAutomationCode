Feature: Articles

# 92 Validate User has ability to create new article and submit for publication	
Scenario: Create And Submit Article
	Given I am logged in to CRM
	When I go to create a new Article
	And I fill out the following fields
		| Title        | Description | Keywords  |
		| Test-{Lorem} | {Lorem}     | test      | 
	And I set the article body to 'foobar'
	And I Save the Entity
	And I fill out Article Subject and Review
	And I select Next Stage from 'Author'
	And I wait for the Browser to go Idle
	And I Approve the Article
	And I select Next Stage from 'Review'
	And I mark the Article as Completed
	And I select Finish from 'Publish'
	And I wait for the Browser to go Idle
	Then I verify that Business Process Flow status contains 'Completed'