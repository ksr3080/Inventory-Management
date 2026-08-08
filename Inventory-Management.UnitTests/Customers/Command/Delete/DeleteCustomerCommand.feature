@DeleteCustomerCommand
Feature: Delete Customer Command
    As an administrator
    I want to delete an existing customer
    So that obsolete customer records are removed from the system

    Scenario Outline: Delete Customer
        Given a customer exists with id "<existingId>"
        And I have an Id "<id>"

        When I make a request to Delete Customer

        Then the response is "<response>"
        And if the response has validation issues I see the "<responseErrors>" in the response
        And if the response is successful I verify the customer was removed from the repository

    Examples:
        | def                           | response   | responseErrors | existingId | id |
        | success: valid delete         | Success    |                | 1          | 1  |
        | bad request: customer missing | BadRequest | NotFound       |            | 99 |
