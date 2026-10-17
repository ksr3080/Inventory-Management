using MediatR;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.Application.FormRulesPermission.Command;

public class SaveFormRulesPermissionCommand : IRequest<bool>
{
    public int RoleId { get; set; }
    public List<SavePermissionItem> Permissions { get; set; } = new();
}

public class SavePermissionItem
{
    public int FormRulesId { get; set; }
    public int FormId { get; set; }
    public bool IsAllowRules { get; set; }
}

public class SaveFormRulesPermissionCommandHandler : IRequestHandler<SaveFormRulesPermissionCommand, bool>
{
    private readonly IFormRulesPermissionRepository _repository;

    public SaveFormRulesPermissionCommandHandler(IFormRulesPermissionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(SaveFormRulesPermissionCommand request, CancellationToken cancellationToken)
    {
        // 1. Delete all existing permissions for this role
        await _repository.DeleteByRoleIdAsync(request.RoleId, cancellationToken);

        // 2. Insert new permissions from the request
        if (request.Permissions.Any())
        {
            var entities = request.Permissions.Select(p => new HotelManagement.Domain.Entities.FormRulesPermission
            {
                FormRulesId = p.FormRulesId,
                FormId = p.FormId,
                RoleId = request.RoleId,
                IsAllowRules = p.IsAllowRules
            }).ToList();

            await _repository.AddRangeAsync(entities, cancellationToken);
        }

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
