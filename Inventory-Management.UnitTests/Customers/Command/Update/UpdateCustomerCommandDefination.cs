using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Inventory_Management.Application.Customers.Command;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.UnitTests.Customers.Command.Update
{
    [Binding]
    [Scope(Feature = "Update Customer Command")]
    public class UpdateCustomerCommandDefination
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly UpdateCustomerCommandHandler _handler;

        private string _existingId = string.Empty;
        private string _id = string.Empty;
        private string _code = string.Empty;
        private string _email = string.Empty;
        private string _gstNo = string.Empty;
        private string _phoneNo = string.Empty;
        private string _address1 = string.Empty;
        private string? _address2 = null;
        private string _state = string.Empty;
        private string _stateCode = string.Empty;
        private string _city = string.Empty;
        private string _pincode = string.Empty;
        private string _country = string.Empty;
        private string _ship_address1 = string.Empty;
        private string? _ship_address2 = null;
        private string _ship_state = string.Empty;
        private string _ship_stateCode = string.Empty;
        private string _ship_city = string.Empty;
        private string _ship_pincode = string.Empty;
        private string _ship_country = string.Empty;
        private string _createdby = string.Empty;

        private Customer? _existingCustomer;
        private string _response = string.Empty;
        private string _responseError = string.Empty;

        public UpdateCustomerCommandDefination()
        {
            _handler = new UpdateCustomerCommandHandler(_customerRepositoryMock.Object);
        }

        [Given(@"a customer exists with id ""(.*)""")]
        public void GivenACustomerExistsWithId(string existingId)
        {
            _existingId = existingId;

            if (string.IsNullOrWhiteSpace(existingId))
            {
                _customerRepositoryMock
                    .Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Customer?)null);
                return;
            }

            _existingCustomer = new Customer(
                "CUST001", "old@acme.com", "24AAAAA0000A1Z5", "Old Address", null,
                "Gujarat", "GJ", "Ahmedabad", "380001", "India", "9000000000",
                "Old Ship Address", null, "Gujarat", "GJ", "Ahmedabad", "380001", "India");

            _customerRepositoryMock
                .Setup(r => r.GetByIdAsync(int.Parse(existingId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_existingCustomer);
        }

        [Given(@"I have an Id ""(.*)""")]
        public void GivenIHaveAnId(string id) => _id = id;

        [Given(@"I have a Code ""(.*)""")]
        public void GivenIHaveACode(string code) => _code = code;

        [Given(@"I have an Email ""(.*)""")]
        public void GivenIHaveAnEmail(string email) => _email = email;

        [Given(@"I have a GSTNo ""(.*)""")]
        public void GivenIHaveAGSTNo(string gstNo) => _gstNo = gstNo;

        [Given(@"I have a PhoneNo ""(.*)""")]
        public void GivenIHaveAPhoneNo(string phoneNo) => _phoneNo = phoneNo;

        [Given(@"I have an Address1 ""(.*)""")]
        public void GivenIHaveAnAddress1(string address1) => _address1 = address1;

        [Given(@"I have an Address2 ""(.*)""")]
        public void GivenIHaveAnAddress2(string address2) => _address2 = string.IsNullOrEmpty(address2) ? null : address2;

        [Given(@"I have a State ""(.*)""")]
        public void GivenIHaveAState(string state) => _state = state;

        [Given(@"I have a StateCode ""(.*)""")]
        public void GivenIHaveAStateCode(string stateCode) => _stateCode = stateCode;

        [Given(@"I have a City ""(.*)""")]
        public void GivenIHaveACity(string city) => _city = city;

        [Given(@"I have a Pincode ""(.*)""")]
        public void GivenIHaveAPincode(string pincode) => _pincode = pincode;

        [Given(@"I have a Country ""(.*)""")]
        public void GivenIHaveACountry(string country) => _country = country;

        [Given(@"I have a Ship_Address1 ""(.*)""")]
        public void GivenIHaveAShipAddress1(string ship_address1) => _ship_address1 = ship_address1;

        [Given(@"I have a Ship_Address2 ""(.*)""")]
        public void GivenIHaveAShipAddress2(string ship_address2) => _ship_address2 = string.IsNullOrEmpty(ship_address2) ? null : ship_address2;

        [Given(@"I have a Ship_State ""(.*)""")]
        public void GivenIHaveAShipState(string ship_state) => _ship_state = ship_state;

        [Given(@"I have a Ship_StateCode ""(.*)""")]
        public void GivenIHaveAShipStateCode(string ship_stateCode) => _ship_stateCode = ship_stateCode;

        [Given(@"I have a Ship_City ""(.*)""")]
        public void GivenIHaveAShipCity(string ship_city) => _ship_city = ship_city;

        [Given(@"I have a Ship_Pincode ""(.*)""")]
        public void GivenIHaveAShipPincode(string ship_pincode) => _ship_pincode = ship_pincode;

        [Given(@"I have a Ship_Country ""(.*)""")]
        public void GivenIHaveAShipCountry(string ship_country) => _ship_country = ship_country;

        [Given(@"I have a Createdby ""(.*)""")]
        public void GivenIHaveACreatedby(string createdby) => _createdby = createdby;

        [When(@"I make a request to Update Customer")]
        public async Task WhenIMakeARequestToUpdateCustomer()
        {
            var command = new UpdateCustomerCommand
            {
                Id = int.Parse(_id),
                Code = _code,
                Email = _email,
                GSTNo = _gstNo,
                PhoneNo = _phoneNo,
                Address1 = _address1,
                Address2 = _address2,
                State = _state,
                StateCode = _stateCode,
                City = _city,
                Pincode = _pincode,
                Country = _country,
                Ship_Address1 = _ship_address1,
                Ship_Address2 = _ship_address2,
                Ship_State = _ship_state,
                Ship_StateCode = _ship_stateCode,
                Ship_City = _ship_city,
                Ship_Pincode = _ship_pincode,
                Ship_Country = _ship_country,
                UpdatedBy = string.IsNullOrWhiteSpace(_createdby) ? 0 : int.Parse(_createdby)
            };

            try
            {
                await _handler.Handle(command, CancellationToken.None);
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

        [Then(@"if the response is successful I verify the customer was updated in the repository")]
        public void ThenIfTheResponseIsSuccessfulIVerifyTheCustomerWasUpdatedInTheRepository()
        {
            if (_response != "Success")
                return;

            _customerRepositoryMock.Verify(r => r.Update(It.IsAny<Customer>()), Times.Once);
            _customerRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
