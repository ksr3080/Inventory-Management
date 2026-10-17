using HotelManagement.Domain.StoreProcedureResponse;
using HotelManagement.Domain.Entities;
using HotelManagement.Shared.DTOs.FormRulesPermission;

namespace HotelManagement.Infrastructure.Repositories.Interfaces;

public interface IFormRulesPermissionRepository
{
    Task<IReadOnlyList<GetFormRulesPermissionDTO>> GetByRoleIdAsync(int roleId, CancellationToken cancellationToken);

    Task DeleteByRoleIdAsync(int roleId, CancellationToken cancellationToken);

    Task AddRangeAsync(List<FormRulesPermission> permissions, CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
