using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Inventory_Management.Application.Companies.Command;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.UnitTests.Companies.Command.Update
{
    [Binding]
    [Scope(Feature = "Update Company Command")]
    public class UpdateCompanyCommandDefination
    {
        private readonly Mock<ICompanyRepository> _companyRepositoryMock = new();
        private readonly UpdateCompanyCommandHandler _handler;

        private string _existingId = string.Empty;
        private string _id = string.Empty;
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

        private Company? _existingCompany;
        private string _response = string.Empty;
        private string _responseError = string.Empty;

        public UpdateCompanyCommandDefination()
        {
            _handler = new UpdateCompanyCommandHandler(_companyRepositoryMock.Object);
        }

        [Given(@"a company exists with id ""(.*)""")]
        public void GivenACompanyExistsWithId(string existingId)
        {
            _existingId = existingId;

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
                "Old Name", "old@acme.com", "9000000000", "Old Address", null,
                "Gujarat", "GJ", "Ahmedabad", "380001", "24AAAAA0000A1Z5", null, 1);

            _companyRepositoryMock
                .Setup(r => r.GetByIdAsync(int.Parse(existingId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_existingCompany);
        }

        [Given(@"I have an Id ""(.*)""")]
        public void GivenIHaveAnId(string id) => _id = id;

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

        [When(@"I make a request to Update Company")]
        public async Task WhenIMakeARequestToUpdateCompany()
        {
            var command = new UpdateCompanyCommand
            {
                Id = int.Parse(_id),
                Name = _name,
                Email = _email,
                PhoneNo = _phoneNo,
                Address1 = _address1,
                State = _state,
                StateCode = _stateCode,
                City = _city,
                Pincode = _pincode,
                GSTNo = _gstNo,
                UpdatedBy = string.IsNullOrWhiteSpace(_createdBy) ? 0 : int.Parse(_createdBy)
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

        [Then(@"if the response is successful I verify the company was updated in the repository")]
        public void ThenIfTheResponseIsSuccessfulIVerifyTheCompanyWasUpdatedInTheRepository()
        {
            if (_response != "Success")
                return;

            _companyRepositoryMock.Verify(r => r.Update(It.IsAny<Company>()), Times.Once);
            _companyRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            NUnit.Framework.Assert.That(_existingCompany, Is.Not.Null);
            NUnit.Framework.Assert.That(_existingCompany!.Name, Is.EqualTo(_name));
        }
    }
}