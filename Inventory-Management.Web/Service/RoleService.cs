using Inventory_Management.Interface;
using Inventory_Management.Shared.DTOs.Roles;

namespace Inventory_Management.Service;

public class RoleService : IRoleService
{
    private readonly HttpClient _http;

    public RoleService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<GetRoleDTO>> GetRolesAsync()
    {
        IReadOnlyList<GetRoleDTO>? roles = await _http.GetFromJsonAsync<IReadOnlyList<GetRoleDTO>>("api/Role/GetRoles");
        return roles ?? [];
    }

    public async Task CreateRoleAsync(CreateRoleDTO roleDTO)
    {
        var response = await _http.PostAsJsonAsync("api/Role/CreateRole", roleDTO);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateRoleAsync(UpdateRoleDTO roleDTO)
    {
        var response = await _http.PutAsJsonAsync($"api/Role/UpdateRole/{roleDTO.Id}", roleDTO);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteRoleAsync(int roleId)
    {
        var response = await _http.DeleteAsync($"api/Role/DeleteRole/{roleId}");
        response.EnsureSuccessStatusCode();
    }
}
