using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Inventory_Management.Application.Companies.Command;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.UnitTests.Companies.Command.Delete
{
    [Scope(Feature = "Delete Company Command")]
    [Binding]
    public class DeleteCompanyCommandDefination
    {
        private readonly Mock<ICompanyRepository> _companyRepositoryMock = new();
        private readonly DeleteCompanyCommandHandler _handler;

        private string _id = string.Empty;
        private Company? _existingCompany;
        private string _response = string.Empty;
        private string _responseError = string.Empty;

        public DeleteCompanyCommandDefination()
        {
            _handler = new DeleteCompanyCommandHandler(_companyRepositoryMock.Object);
        }

        [Given(@"a company exists with id ""(.*)""")]
        public void GivenACompanyExistsWithId(string existingId)
        {
            if (string.IsNullOrWhiteSpace(existingId))
            {
                _companyRepositoryMock
                    .Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Company?)null);
                return;
            }

            // NOTE: this assumes Company has a public constructor matching the one used in
            // CreateCompanyCommandHandler. Adjust the arguments here if your Domain constructor differs.
            _existingCompany = new Company(
                "Acme Corp", "contact@acme.com", "9876543210", "123 Main Street", null,
                "Gujarat", "GJ", "Ahmedabad", "380001", "24AAAAA0000A1Z5", null, 1);

            _companyRepositoryMock
                .Setup(r => r.GetByIdAsync(int.Parse(existingId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_existingCompany);
        }

        [Given(@"I have an Id ""(.*)""")]
        public void GivenIHaveAnId(string id) => _id = id;

        [When(@"I make a request to Delete Company")]
        public async Task WhenIMakeARequestToDeleteCompany()
        {
            try
            {
                await _handler.Handle(new DeleteCompanyCommand(int.Parse(_id)), CancellationToken.None);
                _response = "Success";
                _responseError = string.Empty;
            }
            catch (Exception ex)
            {
                _response = "BadRequest";
                _responseError = ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase)
                    ? "NotFound"
                    : ex.Message;
            }
        }

        [Then(@"the response is ""(.*)""")]
        public void ThenTheResponseIs(string expectedResponse) =>
            NUnit.Framework.Assert.That(_response, Is.EqualTo(expectedResponse),
                $"Actual response was '{_response}'. Captured error: '{_responseError}'");

        [Then(@"if the response has validation issues I see the ""(.*)"" in the response")]
        public void ThenIfTheResponseHasValidationIssuesISeeTheErrorInTheResponse(string expectedError)
        {
            if (_response != "BadRequest" || string.IsNullOrWhiteSpace(expectedError))
                return;

            NUnit.Framework.Assert.That(_responseError, Is.EqualTo(expectedError));
        }

        [Then(@"if the response is successful I verify the company was removed from the repository")]
        public void ThenIfTheResponseIsSuccessfulIVerifyTheCompanyWasRemovedFromTheRepository()
        {
            if (_response != "Success")
                return;

            _companyRepositoryMock.Verify(r => r.Delete(It.IsAny<Company>()), Times.Once);
            _companyRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
