@CreateCompanyCommand
Feature: Create Company Command
    As an administrator
    I want to create a new company
    So that the company is registered in the system

    Scenario Outline: Create Company
        Given I have a Name "<name>"
        And I have an Email "<email>"
        And I have a PhoneNo "<phoneNo>"
        And I have an Address1 "<address1>"
        And I have a State "<state>"
        And I have a StateCode "<stateCode>"
        And I have a City "<city>"
        And I have a Pincode "<pincode>"
        And I have a GSTNo "<gstNo>"
        And I have a CreatedBy "<createdBy>"
        And the company repository has an existing email "<existingEmail>"

        When I make a request to Create Company

        Then the response is "<response>"
        And if the response has validation issues I see the "<responseErrors>" in the response
        And if the response is successful I verify the company was added to the repository

    Examples:
        | def                                | response   | responseErrors | existingEmail    | name      | email            | phoneNo    | address1        | state   | stateCode | city      | pincode | gstNo           | createdBy |
        | success: valid                     | Success    |                 |                  | Acme Corp | contact@acme.com | 9876543210 | 123 Main Street | Gujarat | GJ        | Ahmedabad | 380001  | 24AAAAA0000A1Z5 | 1         |
        | bad request: duplicate email       | BadRequest | Email           | contact@acme.com | Acme Corp | contact@acme.com | 9876543210 | 123 Main Street | Gujarat | GJ        | Ahmedabad | 380001  | 24AAAAA0000A1Z5 | 1         |
        | bad request: missing name          | BadRequest | Name            |                  |           | contact@acme.com | 9876543210 | 123 Main Street | Gujarat | GJ        | Ahmedabad | 380001  | 24AAAAA0000A1Z5 | 1         |
        | bad request: missing email         | BadRequest | Email           |                  | Acme Corp |                  | 9876543210 | 123 Main Street | Gujarat | GJ        | Ahmedabad | 380001  | 24AAAAA0000A1Z5 | 1         |
        | bad request: invalid email format  | BadRequest | Email           |                  | Acme Corp | not-an-email     | 9876543210 | 123 Main Street | Gujarat | GJ        | Ahmedabad | 380001  | 24AAAAA0000A1Z5 | 1         |
        | bad request: missing phone         | BadRequest | PhoneNo         |                  | Acme Corp | contact@acme.com |            | 123 Main Street | Gujarat | GJ        | Ahmedabad | 380001  | 24AAAAA0000A1Z5 | 1         |
        | bad request: missing address       | BadRequest | Address1        |                  | Acme Corp | contact@acme.com | 9876543210 |                 | Gujarat | GJ        | Ahmedabad | 380001  | 24AAAAA0000A1Z5 | 1         |
        | bad request: missing state         | BadRequest | State           |                  | Acme Corp | contact@acme.com | 9876543210 | 123 Main Street |         | GJ        | Ahmedabad | 380001  | 24AAAAA0000A1Z5 | 1         |
        | bad request: missing state code    | BadRequest | StateCode       |                  | Acme Corp | contact@acme.com | 9876543210 | 123 Main Street | Gujarat |           | Ahmedabad | 380001  | 24AAAAA0000A1Z5 | 1         |
        | bad request: missing city          | BadRequest | City            |                  | Acme Corp | contact@acme.com | 9876543210 | 123 Main Street | Gujarat | GJ        |           | 380001  | 24AAAAA0000A1Z5 | 1         |
        | bad request: missing pincode       | BadRequest | Pincode         |                  | Acme Corp | contact@acme.com | 9876543210 | 123 Main Street | Gujarat | GJ        | Ahmedabad |         | 24AAAAA0000A1Z5 | 1         |
        | bad request: missing gst no        | BadRequest | GSTNo           |                  | Acme Corp | contact@acme.com | 9876543210 | 123 Main Street | Gujarat | GJ        | Ahmedabad | 380001  |                 | 1         |
