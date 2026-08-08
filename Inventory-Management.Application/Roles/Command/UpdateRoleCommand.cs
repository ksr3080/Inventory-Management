using MediatR;
using Microsoft.Extensions.Logging;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.Application.Roles.Command;

public class UpdateRoleCommand : IRequest<bool>
{
    public int Id { get; set; }

    public string RoleName { get; set; } = string.Empty;

    public int RoleLevel { get; set; }

    public bool IsActive { get; set; }
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
{
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger<UpdateRoleCommandHandler> _logger;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository, ILogger<UpdateRoleCommandHandler> logger)
    {
        _roleRepository = roleRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (role is null)
        {
            return false;
        }

        role.Update(request.RoleName, request.RoleLevel, request.IsActive);

        _roleRepository.Update(role);
        await _roleRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Role {RoleId} updated successfully.", role.Id);

        return true;
    }
}
