using Inventory_Management.Shared.DTOs.Roles;

namespace Inventory_Management.Interface;

public interface IRoleService
{
    Task<IReadOnlyList<GetRoleDTO>> GetRolesAsync();
    Task CreateRoleAsync(CreateRoleDTO roleDTO);
    Task UpdateRoleAsync(UpdateRoleDTO roleDTO);
    Task DeleteRoleAsync(int roleId);
}
