using AutoMapper;
using MediatR;
using HotelManagement.Infrastructure.Repositories.Interfaces;
using HotelManagement.Shared.DTOs.Customer;

namespace HotelManagement.Application.Customers.Query;

public class GetCustomerQuery : IRequest<IReadOnlyList<GetCustomerDTO>>
{
}

public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, IReadOnlyList<GetCustomerDTO>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<GetCustomerDTO>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<IReadOnlyList<GetCustomerDTO>>(customers);
    }
}
