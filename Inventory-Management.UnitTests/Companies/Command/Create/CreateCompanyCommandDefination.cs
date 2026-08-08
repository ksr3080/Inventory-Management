using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Inventory_Management.Application.Companies.Command;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.UnitTests.Companies.Command.Create
{
    [Binding]
    [Scope(Feature = "Create Company Command")]
    public class CreateCompanyCommandDefination
    {
        private readonly Mock<ICompanyRepository> _companyRepositoryMock = new();
        private readonly CreateCompanyCommandValidation _validator = new();
        private readonly CreateCompanyCommandHandler _handler;

        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _phoneNo = string.Empty;
        private string _address1 = string.Empty;
        private string _state = string.Empty;
        private string _stateCode = string.Empty;
        private string _city = string.Empty;
        private string _pincode = string.Empty;
        private string _gstNo = string.Empty;
        private string _createdBy = string.Empty;
        private string _existingEmail = string.Empty;

        private string _response = string.Empty;
        private List<string> _responseErrors = new();

        public CreateCompanyCommandDefination()
        {
            _handler = new CreateCompanyCommandHandler(_companyRepositoryMock.Object);
        }

        [Given(@"I have a Name ""(.*)""")]
        public void GivenIHaveAName(string name) => _name = name;

        [Given(@"I have an Email ""(.*)""")]
        public void GivenIHaveAnEmail(string email) => _email = email;

        [Given(@"I have a PhoneNo ""(.*)""")]
        public void GivenIHaveAPhoneNo(string phoneNo) => _phoneNo = phoneNo;

        [Given(@"I have an Address1 ""(.*)""")]
        public void GivenIHaveAnAddress1(string address1) => _address1 = address1;

        [Given(@"I have a State ""(.*)""")]
        public void GivenIHaveAState(string state) => _state = state;

        [Given(@"I have a StateCode ""(.*)""")]
        public void GivenIHaveAStateCode(string stateCode) => _stateCode = stateCode;

        [Given(@"I have a City ""(.*)""")]
        public void GivenIHaveACity(string city) => _city = city;

        [Given(@"I have a Pincode ""(.*)""")]
        public void GivenIHaveAPincode(string pincode) => _pincode = pincode;

        [Given(@"I have a GSTNo ""(.*)""")]
        public void GivenIHaveAGstNo(string gstNo) => _gstNo = gstNo;

        [Given(@"I have a CreatedBy ""(.*)""")]
        public void GivenIHaveACreatedBy(string createdBy) => _createdBy = createdBy;

        [Given(@"the company repository has an existing email ""(.*)""")]
        public void GivenTheCompanyRepositoryHasAnExistingEmail(string existingEmail) => _existingEmail = existingEmail;

        [When(@"I make a request to Create Company")]
        public async Task WhenIMakeARequestToCreateCompany()
        {
            var command = new CreateCompanyCommand
            {
                Name = _name,
                Email = _email,
                PhoneNo = _phoneNo,
                Address1 = _address1,
                State = _state,
                StateCode = _stateCode,
                City = _city,
                Pincode = _pincode,
                GSTNo = _gstNo,
                CreatedBy = string.IsNullOrWhiteSpace(_createdBy) ? 0 : int.Parse(_createdBy)
            };

            // Simulate the validation pipeline behavior (MediatR ValidationBehavior) running before the handler.
            var validationResult = _validator.Validate(command);

            if (!validationResult.IsValid)
            {
                _response = "BadRequest";
                _responseErrors = validationResult.Errors.Select(e => e.PropertyName).Distinct().ToList();
                return;
            }

            _companyRepositoryMock
            .Setup(r => r.EmailExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string email, CancellationToken _) =>
                !string.IsNullOrWhiteSpace(_existingEmail)
                && email == _existingEmail.Trim().ToLowerInvariant());

            try
            {
                await _handler.Handle(command, CancellationToken.None);
                _response = "Success";
                _responseErrors = new List<string>();
            }
            catch (InvalidOperationException)
            {
                // Business-rule failure (duplicate email) - treat as a BadRequest on the Email field.
                _response = "BadRequest";
                _responseErrors = new List<string> { "Email" };
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

        [Then(@"if the response is successful I verify the company was added to the repository")]
        public void ThenIfTheResponseIsSuccessfulIVerifyTheCompanyWasAddedToTheRepository()
        {
            if (_response != "Success")
                return;

            _companyRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Company>(), It.IsAny<CancellationToken>()), Times.Once);
            _companyRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}