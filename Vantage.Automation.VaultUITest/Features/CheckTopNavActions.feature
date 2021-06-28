Feature: CheckTopNavActions

Background:
	Given I am logged in to CRM

@tc-63018
Scenario: Check Accounts Top Nav Actions
	When I open 'Accounts' from left navigation menu
	Then Command bar contains the following actions:
	| Values          |
	| New             |
	| Delete          |
	| Export to Excel |

	When I select row #'1' from Grid
	Then Command bar contains the following actions:
	| Values                  |
	| Delete                  |
	| Edit                    |
	| Merge                   |
	| Export Selected Records |

@tc-63286 @Ignore
Scenario: Check Agreements Top Nav Actions
	When I open 'Agreements' from left navigation menu
	Then Command bar contains the following actions:
	| Values          |
	| New             |
	| Delete          |
	| Export to Excel |

	When I select row #'1' from Grid
	Then Command bar contains the following actions:
	| Values |
	| Edit   |

@tc-63291
Scenario: Check Agreement Files Top Nav Actions
	When I open 'Accounts' from left navigation menu
	Then Command bar contains the following actions:
	| Values          |
	| New             |
	| Delete          |
	| Export to Excel |

	When I select row #'1' from Grid
	Then Command bar contains the following actions:
	| Values |
	| Edit   |

@tc-63295 @Ignore
Scenario: Check Clauses Top Nav Actions
	When I open 'Clauses' from left navigation menu
	Then Command bar contains the following actions:
	| Values          |
	| New             |
	| Delete          |
	| Export to Excel |

	When I select row #'1' from Grid
	Then Command bar contains the following actions:
	| Values |
	| Edit   |

@tc-63293
Scenario: Check Agreement File Details Nav Actions
	When I open 'Agreement Files' from left navigation menu
		And I open the grid record at index '1'
	Then Command bar contains only the following actions:
	| Values       |
	| Save         |
	| Save & Close |
	| Delete       |
	| Check Access |