using Inventory_Management.Shared.DTOs.FormRulesPermission;

namespace Inventory_Management.Interface;

public interface IFormRulesPermissionService
{
    Task<IReadOnlyList<GetFormRulesPermissionDTO>> GetByRoleIdAsync(int roleId);
    Task SavePermissionsAsync(SaveFormRulesPermissionDTO dto);
}
