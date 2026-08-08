using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Inventory_Management.Application.Customers.Query;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Repositories.Interfaces;
using Inventory_Management.Shared.DTOs.Customer;
using AutoMapper;

namespace Inventory_Management.UnitTests.Customers.Query.Get
{
    [Binding]
    [Scope(Feature = "Get Customer Query")]
    public class GetCustomerQueryDefination
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly GetCustomerHandler _handler;

        private List<Customer> _existingCustomers = new();
        private List<GetCustomerDTO> _mappedCustomers = new();
        private IReadOnlyList<GetCustomerDTO> _response = new List<GetCustomerDTO>();

        public GetCustomerQueryDefination()
        {
            _handler = new GetCustomerHandler(_customerRepositoryMock.Object, _mapperMock.Object);
        }

        [Given(@"the repository contains the following customers:")]
        public void GivenTheRepositoryContainsTheFollowingCustomers(Table table)
        {
            _existingCustomers = new List<Customer>();
            _mappedCustomers = new List<GetCustomerDTO>();

            foreach (var row in table.Rows)
            {
                int id = int.Parse(row["id"]);
                string code = row["code"];
                string email = row["email"];
                string gstNo = row["gstNo"];

                // Create domain entity
                var customer = new Customer(
                    code,
                    email,
                    gstNo,
                    "123 Main Street",
                    null,
                    "Gujarat",
                    "GJ",
                    "Ahmedabad",
                    "380001",
                    "India",
                    "9876543210",
                    "456 Ship Street",
                    null,
                    "Gujarat",
                    "GJ",
                    "Ahmedabad",
                    "380001",
                    "India");

                _existingCustomers.Add(customer);

                // Create DTO
                var dto = new GetCustomerDTO
                {
                    Id = id,
                    Code = code,
                    Email = email,
                    GSTNo = gstNo,
                    Address1 = "123 Main Street",
                    Address2 = null,
                    State = "Gujarat",
                    StateCode = "GJ",
                    City = "Ahmedabad",
                    Pincode = "380001",
                    Country = "India",
                    PhoneNo = "9876543210",
                    Ship_Address1 = "456 Ship Street",
                    Ship_Address2 = null,
                    Ship_State = "Gujarat",
                    Ship_StateCode = "GJ",
                    Ship_City = "Ahmedabad",
                    Ship_Pincode = "380001",
                    Ship_Country = "India",
                    IsActive = true
                };

                _mappedCustomers.Add(dto);
            }

            _customerRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(_existingCustomers);

            _mapperMock
                .Setup(m => m.Map<IReadOnlyList<GetCustomerDTO>>(It.IsAny<IEnumerable<Customer>>()))
                .Returns(_mappedCustomers.AsReadOnly());
        }

        [Given(@"no customers exist in the repository")]
        public void GivenNoCustomersExistInTheRepository()
        {
            _customerRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Customer>());

            _mapperMock
                .Setup(m => m.Map<IReadOnlyList<GetCustomerDTO>>(It.IsAny<IEnumerable<Customer>>()))
                .Returns(new List<GetCustomerDTO>().AsReadOnly());
        }

        [When(@"I execute the Get Customer query")]
        public async Task WhenIExecuteTheGetCustomerQuery()
        {
            var query = new GetCustomerQuery();
            _response = await _handler.Handle(query, CancellationToken.None);
        }

        [Then(@"the response should contain (\d+) customers?")]
        public void ThenTheResponseShouldContainCustomers(int expectedCount)
        {
            NUnit.Framework.Assert.That(_response.Count, Is.EqualTo(expectedCount),
                $"Expected {expectedCount} customers but got {_response.Count}");
        }

        [Then(@"the response should contain a customer with code ""(.*)""")]
        public void ThenTheResponseShouldContainACustomerWithCode(string expectedCode)
        {
            var customer = _response.FirstOrDefault(c => c.Code == expectedCode);
            NUnit.Framework.Assert.That(customer, Is.Not.Null,
                $"Customer with code '{expectedCode}' not found in response");
        }

        [Then(@"the response should be an empty list")]
        public void ThenTheResponseShouldBeAnEmptyList()
        {
            NUnit.Framework.Assert.That(_response, Is.Empty,
                $"Expected empty list but got {_response.Count} customers");
        }

        [Then(@"the first customer email should be ""(.*)""")]
        public void ThenTheFirstCustomerEmailShouldBe(string expectedEmail)
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty, "Response is empty");
            var firstCustomer = _response.First();
            NUnit.Framework.Assert.That(firstCustomer.Email, Is.EqualTo(expectedEmail),
                $"Expected first customer email '{expectedEmail}' but got '{firstCustomer.Email}'");
        }

        [Then(@"the customer code should be ""(.*)""")]
        public void ThenTheCustomerCodeShouldBe(string expectedCode)
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty);
            var customer = _response.First();
            NUnit.Framework.Assert.That(customer.Code, Is.EqualTo(expectedCode));
        }

        [Then(@"the customer email should be ""(.*)""")]
        public void ThenTheCustomerEmailShouldBe(string expectedEmail)
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty);
            var customer = _response.First();
            NUnit.Framework.Assert.That(customer.Email, Is.EqualTo(expectedEmail));
        }

        [Then(@"the customer gstNo should be ""(.*)""")]
        public void ThenTheCustomerGstNoShouldBe(string expectedGstNo)
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty);
            var customer = _response.First();
            NUnit.Framework.Assert.That(customer.GSTNo, Is.EqualTo(expectedGstNo));
        }

        [Then(@"the customer id should be (\d+)")]
        public void ThenTheCustomerIdShouldBe(int expectedId)
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty);
            var customer = _response.First();
            NUnit.Framework.Assert.That(customer.Id, Is.EqualTo(expectedId));
        }

        [Then(@"customer at index (\d+) should have code ""(.*)""")]
        public void ThenCustomerAtIndexShouldHaveCode(int index, string expectedCode)
        {
            NUnit.Framework.Assert.That(_response, Has.Count.GreaterThan(index),
                $"Response does not have customer at index {index}");
            var customer = _response.ElementAt(index);
            NUnit.Framework.Assert.That(customer.Code, Is.EqualTo(expectedCode),
                $"Expected customer at index {index} to have code '{expectedCode}' but got '{customer.Code}'");
        }

        [Then(@"all customers should have non-empty email addresses")]
        public void ThenAllCustomersShouldHaveNonEmptyEmailAddresses()
        {
            NUnit.Framework.Assert.That(_response, Is.Not.Empty, "Response is empty");

            foreach (var customer in _response)
            {
                NUnit.Framework.Assert.That(customer.Email, Is.Not.Null.And.Not.Empty,
                    $"Customer with code '{customer.Code}' has empty email");
            }
        }
    }
}
