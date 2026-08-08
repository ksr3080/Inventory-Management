using MediatR;
using Inventory_Management.Infrastructure.Repositories.Interfaces;
using Inventory_Management.Shared.DTOs.Roles;

namespace Inventory_Management.Application.Roles.Query;

public class GetRoleQuery : IRequest<IReadOnlyList<GetRoleDTO>>
{
}

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, IReadOnlyList<GetRoleDTO>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IReadOnlyList<GetRoleDTO>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        return await _roleRepository.GetAllAsync(cancellationToken);
    }
}
