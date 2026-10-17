using MediatR;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.Application.Customers.Command;

public class UpdateCustomerCommand : IRequest<string>
{
    public int Id { get; set; }
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
    public int UpdatedBy { get; set; }
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, string>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<string> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (customer == null)
            throw new Exception("Customer not found.");

        customer.UpdateDetails(request.Code, request.Email, request.GSTNo, request.Address1, request.Address2, request.State, request.StateCode, request.City, request.Pincode, request.Country,
        request.PhoneNo, request.Ship_Address1, request.Ship_Address2, request.Ship_State, request.Ship_StateCode, request.Ship_City, request.Ship_Pincode, request.Ship_Country, request.UpdatedBy);

        _customerRepository.Update(customer);

        await _customerRepository.SaveChangesAsync(cancellationToken);
        return "Update successfully";
    }
}