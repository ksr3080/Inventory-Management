using MediatR;
using Microsoft.Extensions.Logging;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.Application.Users.Command;

/// <summary>
/// Assigns (or replaces) a role for a given user under the specified company.
/// If the user already has a role in UsersInRole, the old record is removed and
/// the new one is inserted (upsert pattern).
/// </summary>
public class AssignUserRoleCommand : IRequest<bool>
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public int CompanyId { get; set; }
}

public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand, bool>
{
    private readonly IUsersInRoleRepository _usersInRoleRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AssignUserRoleCommandHandler> _logger;

    public AssignUserRoleCommandHandler(
        IUsersInRoleRepository usersInRoleRepository,
        IUserRepository userRepository,
        ILogger<AssignUserRoleCommandHandler> logger)
    {
        _usersInRoleRepository = usersInRoleRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        // Verify the user exists
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            _logger.LogWarning("AssignUserRole: user {UserId} not found.", request.UserId);
            return false;
        }

        // Remove any existing role assignment for this user
        var existing = await _usersInRoleRepository.GetByUserIdTrackedAsync(request.UserId, cancellationToken);
        if (existing is not null)
        {
            _usersInRoleRepository.Remove(existing);
            await _usersInRoleRepository.SaveChangesAsync(cancellationToken);
        }

        // Insert new role assignment
        var newAssignment = UsersInRole.Create(request.UserId, request.RoleId, request.CompanyId);
        await _usersInRoleRepository.AddAsync(newAssignment, cancellationToken);
        await _usersInRoleRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "User {UserId} assigned to role {RoleId} under company {CompanyId}.",
            request.UserId, request.RoleId, request.CompanyId);

        return true;
    }
}
