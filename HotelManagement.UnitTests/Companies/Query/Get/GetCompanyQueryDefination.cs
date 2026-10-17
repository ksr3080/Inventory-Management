using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using HotelManagement.Application.Companies.Query;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Repositories.Interfaces;
using HotelManagement.Shared.DTOs.Company;
using AutoMapper;

namespace HotelManagement.UnitTests.Companies.Query.Get
{
    [Binding]
    [Scope(Feature = "Get Company Query")]
    public class GetCompanyQueryDefination
    {
        private readonly Mock<ICompanyRepository> _companyRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly GetCompanyHandler _handler;

        private List<Company> _existingCompanies = new();
        private List<GetCompanyDTO> _mappedCompanies = new();
        private IReadOnlyList<GetCompanyDTO> _response = new List<GetCompanyDTO>();

        public GetCompanyQueryDefination()
        {
            _handler = new GetCompanyHandler(_companyRepositoryMock.Object, _mapperMock.Object);
        }

        [Given(@"the repository contains the following companies:")]
        public void GivenTheRepositoryContainsTheFollowingCompanies(Table table)
        {
            _existingCompanies = new List<Company>();
            _mappedCompanies = new List<GetCompanyDTO>();

            foreach (var row in table.Rows)
            {
                int id = int.Parse(row["id"]);
                string name = row["name"];
                string email = row["email"];
                string phoneNo = row["phoneNo"];
                string gstNo = row["gstNo"];

                // Create domain entity
                var company = new Company(
                    name,
                    email,
                    phoneNo,
                    "123 Business Street",
                    null,
                    "Gujarat",
                    "GJ",
                    "Ahmedabad",
                    "380001",
                    gstNo,
                    null,
                    1);

                _existingCompanies.Add(company);

                // Create DTO
                var dto = new GetCompanyDTO
                {
                    Id = id,
                    Name = name,
                    Email = email,
                    PhoneNo = phoneNo,
                    GSTNo = gstNo,
                    Address1 = "123 Business Street",
                    Address2 = null,
                    State = "Gujarat",
                    StateCode = "GJ",
                    City = "Ahmedabad",
                    Pincode = "380001",
                    Logo = null
                };

                _mappedCompanies.Add(dto);
            }

            _companyRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(_existingCompanies);

            _mapperMock
                .Setup(m => m.Map<IReadOnlyList<GetCompanyDTO>>(It.IsAny<IEnumerable<Company>>()))
                .Returns(_mappedCompanies.AsReadOnly());
        }

        [Given(@"no companies exist in the repository")]
        public void GivenNoCompaniesExistInTheRepository()
        {
            _existingCompanies = new List<Company>();
            _mappedCompanies = new List<GetCompanyDTO>();

            _companyRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(_existingCompanies);

            _mapperMock
                .Setup(m => m.Map<IReadOnlyList<GetCompanyDTO>>(It.IsAny<IEnumerable<Company>>()))
                .Returns(_mappedCompanies.AsReadOnly());
        }

        [When(@"I execute the Get Company query")]
        public async Task WhenIExecuteTheGetCompanyQuery()
        {
            var query = new GetCompanyQuery();
            _response = await _handler.Handle(query, CancellationToken.None);
        }

        [Then(@"the response should contain (\d+) compan(?:y|ies)")]
        public void ThenTheResponseShouldContainCompanies(int expectedCount)
        {
            NUnit.Framework.Assert.That(_response.Count, Is.EqualTo(expectedCount),
                $"Expected {expectedCount} companies but got {_response.Count}");
        }

        [Then(@"the response should be an empty list")]
        public void ThenTheResponseShouldBeAnEmptyList()
        {
            NUnit.Framework.Assert.That(_response, Is.Empty,
                $"Expected empty list but got {_response.Count} companies");
        }

        [Then(@"the response should contain a company with name ""(.*)""")]
        public void ThenTheResponseShouldContainACompanyWithName(string companyName)
        {
            var company = _response.FirstOrDefault(c => c.Name == companyName);
            NUnit.Framework.Assert.That(company, Is.Not.Null,
                $"Company with name '{companyName}' not found in response");
        }

        [Then(@"the first company email should be ""(.*)""")]
        public void ThenTheFirstCompanyEmailShouldBe(string expectedEmail)
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty, "Response is empty");
            var firstCompany = _response.First();
            NUnit.Framework.Assert.That(firstCompany.Email, Is.EqualTo(expectedEmail),
                $"Expected first company email '{expectedEmail}' but got '{firstCompany.Email}'");
        }

        [Then(@"the company name should be ""(.*)""")]
        public void ThenTheCompanyNameShouldBe(string expectedName)
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty, "Response is empty");
            var firstCompany = _response.First();
            NUnit.Framework.Assert.That(firstCompany.Name, Is.EqualTo(expectedName),
                $"Expected company name '{expectedName}' but got '{firstCompany.Name}'");
        }

        [Then(@"the company email should be ""(.*)""")]
        public void ThenTheCompanyEmailShouldBe(string expectedEmail)
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty, "Response is empty");
            var firstCompany = _response.First();
            NUnit.Framework.Assert.That(firstCompany.Email, Is.EqualTo(expectedEmail),
                $"Expected company email '{expectedEmail}' but got '{firstCompany.Email}'");
        }

        [Then(@"the company gstNo should be ""(.*)""")]
        public void ThenTheCompanyGstNoShouldBe(string expectedGstNo)
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty, "Response is empty");
            var firstCompany = _response.First();
            NUnit.Framework.Assert.That(firstCompany.GSTNo, Is.EqualTo(expectedGstNo),
                $"Expected company GSTNo '{expectedGstNo}' but got '{firstCompany.GSTNo}'");
        }

        [Then(@"the company phoneNo should be ""(.*)""")]
        public void ThenTheCompanyPhoneNoShouldBe(string expectedPhoneNo)
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty, "Response is empty");
            var firstCompany = _response.First();
            NUnit.Framework.Assert.That(firstCompany.PhoneNo, Is.EqualTo(expectedPhoneNo),
                $"Expected company phoneNo '{expectedPhoneNo}' but got '{firstCompany.PhoneNo}'");
        }

        [Then(@"the company id should be (\d+)")]
        public void ThenTheCompanyIdShouldBe(int expectedId)
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty, "Response is empty");
            var firstCompany = _response.First();
            NUnit.Framework.Assert.That(firstCompany.Id, Is.EqualTo(expectedId),
                $"Expected company id '{expectedId}' but got '{firstCompany.Id}'");
        }
    }
}
