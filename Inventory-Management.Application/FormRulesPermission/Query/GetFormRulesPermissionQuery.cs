using MediatR;
using Inventory_Management.Infrastructure.Repositories.Interfaces;
using Inventory_Management.Shared.DTOs.FormRulesPermission;

namespace Inventory_Management.Application.FormRulesPermission.Query;

public class GetFormRulesPermissionQuery : IRequest<IReadOnlyList<GetFormRulesPermissionDTO>>
{
    public int RoleId { get; init; }

    public GetFormRulesPermissionQuery(int roleId)
    {
        RoleId = roleId;
    }
}

public class GetFormRulesPermissionQueryHandler : IRequestHandler<GetFormRulesPermissionQuery, IReadOnlyList<GetFormRulesPermissionDTO>>
{
    private readonly IFormRulesPermissionRepository _repository;

    public GetFormRulesPermissionQueryHandler(IFormRulesPermissionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<GetFormRulesPermissionDTO>> Handle(GetFormRulesPermissionQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByRoleIdAsync(request.RoleId, cancellationToken);
    }
}
