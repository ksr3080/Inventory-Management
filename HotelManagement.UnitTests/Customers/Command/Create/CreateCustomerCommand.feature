@CreateCustomerCommand
Feature: Create Customer Command
    As an administrator
    I want to create a new customer
    So that the customer is registered in the system

    Scenario Outline: Create Customer
        Given I have a Code "<code>"
        And I have an Email "<email>"
        And I have a GSTNo "<gstNo>"
        And I have a PhoneNo "<phoneNo>"
        And I have an Address1 "<address1>"
        And I have an Address2 "<address2>"
        And I have a State "<state>"
        And I have a StateCode "<stateCode>"
        And I have a City "<city>"
        And I have a Pincode "<pincode>"
        And I have a Country "<country>"
        And I have a Ship_Address1 "<ship_address1>"
        And I have a Ship_Address2 "<ship_address2>"
        And I have a Ship_State "<ship_state>"
        And I have a Ship_StateCode "<ship_stateCode>"
        And I have a Ship_City "<ship_city>"
        And I have a Ship_Pincode "<ship_pincode>"
        And I have a Ship_Country "<ship_country>"
        And the customer repository has an existing email "<existingEmail>"
        And the customer repository has an existing gst "<existingGST>"

        When I make a request to Create Customer

        Then the response is "<response>"
        And if the response has validation issues I see the "<responseErrors>" in the response
        And if the response is successful I verify the customer was added to the repository

    Examples:
        | def                              | response   | responseErrors | existingEmail     | existingGST | code   | email              | gstNo           | phoneNo    | address1            | address2 | state   | stateCode | city      | pincode | country | ship_address1       | ship_address2 | ship_state | ship_stateCode | ship_city | ship_pincode | ship_country |
        | success: valid                   | Success    |                |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: duplicate email     | BadRequest | Email          | customer@acme.com |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: duplicate gst       | BadRequest | GSTNo          |                   | 24AAAAA0000A1Z5 | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing code        | BadRequest | Code           |                   |             |        | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing email       | BadRequest | Email          |                   |             | CUST01 |                    | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: invalid email       | BadRequest | Email          |                   |             | CUST01 | invalid-email      | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing gst         | BadRequest | GSTNo          |                   |             | CUST01 | customer@acme.com  |                 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: invalid gst length  | BadRequest | GSTNo          |                   |             | CUST01 | customer@acme.com  | 123             | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing phone       | BadRequest | PhoneNo        |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 |            | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing address1    | BadRequest | Address1       |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 |                     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing state       | BadRequest | State          |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          |         | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing state code  | BadRequest | StateCode      |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat |           | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing city        | BadRequest | City           |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        |           | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing pincode     | BadRequest | Pincode        |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad |         | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: invalid pincode     | BadRequest | Pincode        |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 123     | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing country     | BadRequest | Country        |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  |         | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing ship_address1 | BadRequest | Ship_Address1  |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   |                     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing ship_state  | BadRequest | Ship_State     |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               |            | GJ             | Ahmedabad | 380001       | India        |
        | bad request: missing ship_statecode | BadRequest | Ship_StateCode |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    |                | Ahmedabad | 380001       | India        |
        | bad request: missing ship_city   | BadRequest | Ship_City      |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             |            | 380001       | India        |
        | bad request: missing ship_pincode | BadRequest | Ship_Pincode   |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad |              | India        |
        | bad request: invalid ship_pincode | BadRequest | Ship_Pincode   |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 123          | India        |
        | bad request: missing ship_country | BadRequest | Ship_Country   |                   |             | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 123 Main Street     |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       |              |
