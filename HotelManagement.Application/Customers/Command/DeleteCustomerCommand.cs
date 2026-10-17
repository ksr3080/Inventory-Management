using MediatR;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.Application.Customers.Command;

public record DeleteCustomerCommand(int Id) : IRequest;
public class DeleteCustomerCommandHandler
    : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (customer == null)
            throw new Exception("Customer not found.");

        _customerRepository.Delete(customer);

        await _customerRepository.SaveChangesAsync(cancellationToken);
    }
}