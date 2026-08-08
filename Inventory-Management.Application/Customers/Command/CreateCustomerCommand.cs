using MediatR;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.Application.Customers.Command;

public class CreateCustomerCommand : IRequest<int>
{
    public string Code { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string GSTNo { get; set; } = string.Empty;
    public string Address1 { get; set; } = string.Empty;
    public string? Address2 { get; set; }
    public string State { get; set; } = string.Empty;
    public string StateCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public string Ship_Address1 { get; set; } = string.Empty;
    public string? Ship_Address2 { get; set; }
    public string Ship_State { get; set; } = string.Empty;
    public string Ship_StateCode { get; set; } = string.Empty;
    public string Ship_City { get; set; } = string.Empty;
    public string Ship_Pincode { get; set; } = string.Empty;
    public string Ship_Country { get; set; } = string.Empty;
    public int CreatedBy { get; set; }
}
public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (await _customerRepository.EmailExistsAsync(request.Email, cancellationToken))
            throw new Exception("Email already exists.");

        if (await _customerRepository.GSTExistsAsync(request.GSTNo, cancellationToken))
            throw new Exception("GST Number already exists.");

        var customer = new Customer(
            request.Code,
            request.Email,
            request.GSTNo,
            request.Address1,
            request.Address2,
            request.State,
            request.StateCode,
            request.City,
            request.Pincode,
            request.Country,
            request.PhoneNo,
            request.Ship_Address1,
            request.Ship_Address2,
            request.Ship_State,
            request.Ship_StateCode,
            request.Ship_City,
            request.Ship_Pincode,
            request.Ship_Country,
            request.CreatedBy);

        await _customerRepository.AddAsync(customer, cancellationToken);
        await _customerRepository.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
