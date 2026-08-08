using Inventory_Management.Domain.StoreProcedureResponse;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Shared.DTOs.FormRulesPermission;

namespace Inventory_Management.Infrastructure.Repositories.Interfaces;

public interface IFormRulesPermissionRepository
{
    Task<IReadOnlyList<GetFormRulesPermissionDTO>> GetByRoleIdAsync(int roleId, CancellationToken cancellationToken);

    Task DeleteByRoleIdAsync(int roleId, CancellationToken cancellationToken);

    Task AddRangeAsync(List<FormRulesPermission> permissions, CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
