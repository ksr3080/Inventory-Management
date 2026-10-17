using MediatR;
using HotelManagement.Infrastructure.Repositories.Interfaces;
using HotelManagement.Shared.DTOs.Users;

namespace HotelManagement.Application.Users.Query;

public class GetUserQuery : IRequest<IReadOnlyList<GetUserDTO>>
{
    public int CompanyId { get; init; }

    public GetUserQuery(int companyId)
    {
        CompanyId = companyId;
    }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IReadOnlyList<GetUserDTO>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IReadOnlyList<GetUserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetAllByCompanyAsync(request.CompanyId, cancellationToken);
    }
}