using HotelManagement.Shared.DTOs.FormRulesPermission;

namespace HotelManagement.Interface;

public interface IFormRulesPermissionService
{
    Task<IReadOnlyList<GetFormRulesPermissionDTO>> GetByRoleIdAsync(int roleId);
    Task SavePermissionsAsync(SaveFormRulesPermissionDTO dto);
}
