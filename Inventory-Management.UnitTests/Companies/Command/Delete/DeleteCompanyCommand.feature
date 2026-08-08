@DeleteCompanyCommand
Feature: Delete Company Command
    As an administrator
    I want to delete an existing company
    So that obsolete company records are removed from the system

    Scenario Outline: Delete Company
        Given a company exists with id "<existingId>"
        And I have an Id "<id>"

        When I make a request to Delete Company

        Then the response is "<response>"
        And if the response has validation issues I see the "<responseErrors>" in the response
        And if the response is successful I verify the company was removed from the repository

    Examples:
        | def                          | response   | responseErrors | existingId | id |
        | success: valid delete        | Success    |                 | 1          | 1  |
        | bad request: company missing | BadRequest | NotFound        |            | 99 |
