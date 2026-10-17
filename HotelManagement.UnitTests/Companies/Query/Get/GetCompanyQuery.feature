@GetCompanyQuery
Feature: Get Company Query
	As a user
	I want to retrieve company information
	So that I can view all companies in the system

	Scenario: Get all companies when companies exist
		Given the repository contains the following companies:
			| id | name           | email              | phoneNo    | gstNo           |
			| 1  | Acme Corp      | contact@acme.com   | 9876543210 | 24AAAAA0000A1Z5 |
			| 2  | Tech Solutions | tech@solutions.com | 9876543211 | 24BBBBB0000B1Z5 |
			| 3  | Global Trades  | global@trades.com  | 9876543212 | 24CCCCC0000C1Z5 |

		When I execute the Get Company query

		Then the response should contain 3 companies
		And the response should contain a company with name "Acme Corp"
		And the response should contain a company with name "Tech Solutions"
		And the response should contain a company with name "Global Trades"
		And the first company email should be "contact@acme.com"

	Scenario: Get all companies when no companies exist
		Given no companies exist in the repository

		When I execute the Get Company query

		Then the response should contain 0 companies
		And the response should be an empty list

	Scenario: Get all companies returns correctly mapped data
		Given the repository contains the following companies:
			| id | name      | email            | phoneNo    | gstNo           |
			| 1  | Acme Corp | contact@acme.com | 9876543210 | 24AAAAA0000A1Z5 |

		When I execute the Get Company query

		Then the response should contain 1 company
		And the company name should be "Acme Corp"
		And the company email should be "contact@acme.com"
		And the company gstNo should be "24AAAAA0000A1Z5"
		And the company phoneNo should be "9876543210"
		And the company id should be 1

	Scenario: Get all companies with multiple records
		Given the repository contains the following companies:
			| id | name            | email                | phoneNo    | gstNo           |
			| 1  | Acme Corp       | contact@acme.com     | 9876543210 | 24AAAAA0000A1Z5 |
			| 2  | Tech Solutions  | tech@solutions.com   | 9876543211 | 24BBBBB0000B1Z5 |
			| 3  | Global Trades   | global@trades.com    | 9876543212 | 24CCCCC0000C1Z5 |
			| 4  | Supply Inc      | supply@inc.com       | 9876543213 | 24DDDDD0000D1Z5 |
			| 5  | Export Experts  | export@experts.com   | 9876543214 | 24EEEEE0000E1Z5 |

		When I execute the Get Company query

		Then the response should contain 5 companies
		And the response should contain a company with name "Supply Inc"
		And the response should contain a company with name "Export Experts"
