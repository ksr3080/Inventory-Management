using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using HotelManagement.Application.Customers.Command;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.UnitTests.Customers.Command.Create
{
    [Binding]
    [Scope(Feature = "Create Customer Command")]
    public class CreateCustomerCommandDefination
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly CreateCustomerCommandValidator _validator = new();
        private readonly CreateCustomerCommandHandler _handler;

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
        private string _existingEmail = string.Empty;
        private string _existingGST = string.Empty;

        private string _response = string.Empty;
        private List<string> _responseErrors = new();

        public CreateCustomerCommandDefination()
        {
            _handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object);
        }

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

        [Given(@"the customer repository has an existing email ""(.*)""")]
        public void GivenTheCustomerRepositoryHasAnExistingEmail(string existingEmail) => _existingEmail = existingEmail;

        [Given(@"the customer repository has an existing gst ""(.*)""")]
        public void GivenTheCustomerRepositoryHasAnExistingGst(string existingGST) => _existingGST = existingGST;

        [When(@"I make a request to Create Customer")]
        public async Task WhenIMakeARequestToCreateCustomer()
        {
            var command = new CreateCustomerCommand
            {
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
                CreatedBy = 1
            };

            // Simulate the validation pipeline behavior (MediatR ValidationBehavior) running before the handler.
            var validationResult = _validator.Validate(command);

            if (!validationResult.IsValid)
            {
                _response = "BadRequest";
                _responseErrors = validationResult.Errors.Select(e => e.PropertyName).Distinct().ToList();
                return;
            }

            _customerRepositoryMock
            .Setup(r => r.EmailExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string email, CancellationToken _) =>
                !string.IsNullOrWhiteSpace(_existingEmail)
                && email == _existingEmail.Trim().ToLowerInvariant());

            _customerRepositoryMock
            .Setup(r => r.GSTExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string gst, CancellationToken _) =>
                !string.IsNullOrWhiteSpace(_existingGST)
                && gst == _existingGST.Trim());

            try
            {
                await _handler.Handle(command, CancellationToken.None);
                _response = "Success";
                _responseErrors = new List<string>();
            }
            catch (Exception ex)
            {
                // Business-rule failure (duplicate email or gst) - treat as a BadRequest.
                _response = "BadRequest";
                if (ex.Message.Contains("Email", StringComparison.OrdinalIgnoreCase))
                    _responseErrors = new List<string> { "Email" };
                else if (ex.Message.Contains("GST", StringComparison.OrdinalIgnoreCase))
                    _responseErrors = new List<string> { "GSTNo" };
                else
                    _responseErrors = new List<string> { ex.Message };
            }
        }

        [Then(@"the response is ""(.*)""")]
        public void ThenTheResponseIs(string expectedResponse) =>
        NUnit.Framework.Assert.That(_response, Is.EqualTo(expectedResponse),
            $"Actual response was '{_response}'. Captured errors: [{string.Join(", ", _responseErrors)}]");

        [Then(@"if the response has validation issues I see the ""(.*)"" in the response")]
        public void ThenIfTheResponseHasValidationIssuesISeeTheErrorInTheResponse(string expectedError)
        {
            if (_response != "BadRequest" || string.IsNullOrWhiteSpace(expectedError))
                return;

            NUnit.Framework.Assert.That(_responseErrors, Does.Contain(expectedError));
        }

        [Then(@"if the response is successful I verify the customer was added to the repository")]
        public void ThenIfTheResponseIsSuccessfulIVerifyTheCustomerWasAddedToTheRepository()
        {
            if (_response != "Success")
                return;

            _customerRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
            _customerRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
