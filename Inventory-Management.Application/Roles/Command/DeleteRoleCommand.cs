using MediatR;
using Microsoft.Extensions.Logging;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.Application.Roles.Command;

public class DeleteRoleCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger<DeleteRoleCommandHandler> _logger;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository, ILogger<DeleteRoleCommandHandler> logger)
    {
        _roleRepository = roleRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (role is null)
        {
            return false;
        }

        _roleRepository.Delete(role);
        await _roleRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Role {RoleId} deleted successfully.", request.Id);

        return true;
    }
}
