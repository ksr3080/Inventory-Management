using HotelManagement.Shared.DTOs.Roles;

namespace HotelManagement.Interface;

public interface IRoleService
{
    Task<IReadOnlyList<GetRoleDTO>> GetRolesAsync();
    Task CreateRoleAsync(CreateRoleDTO roleDTO);
    Task UpdateRoleAsync(UpdateRoleDTO roleDTO);
    Task DeleteRoleAsync(int roleId);
}
