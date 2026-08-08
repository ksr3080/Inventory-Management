@GetCustomerQuery
Feature: Get Customer Query
    As a user
    I want to retrieve customer information
    So that I can view all customers in the system

    Scenario: Get all customers when customers exist
        Given the repository contains the following customers:
            | id | code   | email              | gstNo           |
            | 1  | CUST01 | customer1@acme.com | 24AAAAA0000A1Z5 |
            | 2  | CUST02 | customer2@acme.com | 24BBBBB0000B1Z5 |
            | 3  | CUST03 | customer3@acme.com | 24CCCCC0000C1Z5 |

        When I execute the Get Customer query

        Then the response should contain 3 customers
        And the response should contain a customer with code "CUST01"
        And the response should contain a customer with code "CUST02"
        And the response should contain a customer with code "CUST03"
        And the first customer email should be "customer1@acme.com"

    Scenario: Get all customers when no customers exist
        Given no customers exist in the repository

        When I execute the Get Customer query

        Then the response should contain 0 customers
        And the response should be an empty list

    Scenario: Get all customers returns correctly mapped data
        Given the repository contains the following customers:
            | id | code   | email              | gstNo           |
            | 1  | CUST01 | customer1@acme.com | 24AAAAA0000A1Z5 |

        When I execute the Get Customer query

        Then the response should contain 1 customer
        And the customer code should be "CUST01"
        And the customer email should be "customer1@acme.com"
        And the customer gstNo should be "24AAAAA0000A1Z5"
        And the customer id should be 1

    Scenario: Get all customers with multiple records
        Given the repository contains the following customers:
            | id | code   | email              | gstNo           |
            | 1  | CUST01 | customer1@acme.com | 24AAAAA0000A1Z5 |
            | 2  | CUST02 | customer2@acme.com | 24BBBBB0000B1Z5 |
            | 3  | CUST03 | customer3@acme.com | 24CCCCC0000C1Z5 |
            | 4  | CUST04 | customer4@acme.com | 24DDDDD0000D1Z5 |
            | 5  | CUST05 | customer5@acme.com | 24EEEEE0000E1Z5 |

        When I execute the Get Customer query

        Then the response should contain 5 customers
        And customer at index 0 should have code "CUST01"
        And customer at index 4 should have code "CUST05"
        And all customers should have non-empty email addresses

