@UpdateCustomerCommand
Feature: Update Customer Command
    As an administrator
    I want to update an existing customer's details
    So that the customer record stays accurate

    Scenario Outline: Update Customer
        Given a customer exists with id "<existingId>"
        And I have an Id "<id>"
        And I have a Code "<code>"
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
        And I have a Createdby "<createdby>"

        When I make a request to Update Customer

        Then the response is "<response>"
        And if the response has validation issues I see the "<responseErrors>" in the response
        And if the response is successful I verify the customer was updated in the repository

    Examples:
        | def                          | response   | responseErrors | existingId | id | code   | email              | gstNo           | phoneNo    | address1            | address2 | state   | stateCode | city      | pincode | country | ship_address1       | ship_address2 | ship_state | ship_stateCode | ship_city | ship_pincode | ship_country | createdby |
        | success: valid update        | Success    |                | 1          | 1  | CUST01 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 456 New Street      |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        | 1         |
        | success: with new address    | Success    |                | 2          | 2  | CUST02 | customer2@acme.com | 24BBBBB0000B1Z5 | 9876543211 | 789 Other Road      |          | Gujarat | GJ        | Surat     | 395001  | India   | 789 Ship Road       |               | Gujarat    | GJ             | Surat     | 395001       | India        | 2         |
        | bad request: customer missing | BadRequest | NotFound       |            | 99 | CUST99 | customer@acme.com  | 24AAAAA0000A1Z5 | 9876543210 | 456 New Street      |          | Gujarat | GJ        | Ahmedabad | 380001  | India   | 456 Ship Street     |               | Gujarat    | GJ             | Ahmedabad | 380001       | India        | 1         |
