@UpdateCompanyCommand
Feature: Update Company Command
    As an administrator
    I want to update an existing company's details
    So that the company record stays accurate

    Scenario Outline: Update Company
        Given a company exists with id "<existingId>"
        And I have an Id "<id>"
        And I have a Name "<name>"
        And I have an Email "<email>"
        And I have a PhoneNo "<phoneNo>"
        And I have an Address1 "<address1>"
        And I have a State "<state>"
        And I have a StateCode "<stateCode>"
        And I have a City "<city>"
        And I have a Pincode "<pincode>"
        And I have a GSTNo "<gstNo>"
        And I have a CreatedBy "<createdBy>"

        When I make a request to Update Company

        Then the response is "<response>"
        And if the response has validation issues I see the "<responseErrors>" in the response
        And if the response is successful I verify the company was updated in the repository

    Examples:
        | def                          | response   | responseErrors | existingId | id | name          | email            | phoneNo    | address1       | state   | stateCode | city      | pincode | gstNo           | createdBy |
        | success: valid update        | Success    |                 | 1          | 1  | Acme Corp Ltd | contact@acme.com | 9876543210 | 456 New Street | Gujarat | GJ        | Ahmedabad | 380001  | 24AAAAA0000A1Z5 | 1         |
        | success: with new address    | Success    |                 | 2          | 2  | Acme Corp     | contact@acme.com | 9876543210 | 789 Other Road | Gujarat | GJ        | Surat     | 395001  | 24AAAAA0000A1Z5 | 1         |
        | bad request: company missing | BadRequest | NotFound        |            | 99 | Acme Corp     | contact@acme.com | 9876543210 | 456 New Street | Gujarat | GJ        | Ahmedabad | 380001  | 24AAAAA0000A1Z5 | 1         |
