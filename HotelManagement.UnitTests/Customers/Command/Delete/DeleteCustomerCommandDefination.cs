using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using HotelManagement.Application.Customers.Command;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.UnitTests.Customers.Command.Delete
{
    [Scope(Feature = "Delete Customer Command")]
    [Binding]
    public class DeleteCustomerCommandDefination
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly DeleteCustomerCommandHandler _handler;

        private string _id = string.Empty;
        private Customer? _existingCustomer;
        private string _response = string.Empty;
        private string _responseError = string.Empty;

        public DeleteCustomerCommandDefination()
        {
            _handler = new DeleteCustomerCommandHandler(_customerRepositoryMock.Object);
        }

        [Given(@"a customer exists with id ""(.*)""")]
        public void GivenACustomerExistsWithId(string existingId)
        {
            if (string.IsNullOrWhiteSpace(existingId))
            {
                _customerRepositoryMock
                    .Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Customer?)null);
                return;
            }

            _existingCustomer = new Customer(
                "CUST001", "customer@acme.com", "24AAAAA0000A1Z5", "123 Main Street", null,
                "Gujarat", "GJ", "Ahmedabad", "380001", "India", "9876543210",
                "456 Ship Street", null, "Gujarat", "GJ", "Ahmedabad", "380001", "India");

            _customerRepositoryMock
                .Setup(r => r.GetByIdAsync(int.Parse(existingId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_existingCustomer);
        }

        [Given(@"I have an Id ""(.*)""")]
        public void GivenIHaveAnId(string id) => _id = id;

        [When(@"I make a request to Delete Customer")]
        public async Task WhenIMakeARequestToDeleteCustomer()
        {
            try
            {
                await _handler.Handle(new DeleteCustomerCommand(int.Parse(_id)), CancellationToken.None);
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

        [Then(@"if the response is successful I verify the customer was removed from the repository")]
        public void ThenIfTheResponseIsSuccessfulIVerifyTheCustomerWasRemovedFromTheRepository()
        {
            if (_response != "Success")
                return;

            _customerRepositoryMock.Verify(r => r.Delete(It.IsAny<Customer>()), Times.Once);
            _customerRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
