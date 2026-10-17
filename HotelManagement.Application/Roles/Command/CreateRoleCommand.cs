using MediatR;
using Microsoft.Extensions.Logging;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.Application.Roles.Command;

public class CreateRoleCommand : IRequest<int>
{
    public string RoleName { get; set; } = string.Empty;

    public int RoleLevel { get; set; }

    public bool IsActive { get; set; } = true;
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
{
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger<CreateRoleCommandHandler> _logger;

    public CreateRoleCommandHandler(IRoleRepository roleRepository, ILogger<CreateRoleCommandHandler> logger)
    {
        _roleRepository = roleRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role(request.RoleName, request.RoleLevel, request.IsActive);

        await _roleRepository.AddAsync(role, cancellationToken);
        await _roleRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Role {RoleId} created successfully.", role.Id);

        return role.Id;
    }
}
