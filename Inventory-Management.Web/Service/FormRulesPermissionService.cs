using Inventory_Management.Interface;
using Inventory_Management.Shared.DTOs.FormRulesPermission;

namespace Inventory_Management.Service;

public class FormRulesPermissionService : IFormRulesPermissionService
{
    private readonly HttpClient _http;

    public FormRulesPermissionService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<GetFormRulesPermissionDTO>> GetByRoleIdAsync(int roleId)
    {
        IReadOnlyList<GetFormRulesPermissionDTO>? permissions =
            await _http.GetFromJsonAsync<IReadOnlyList<GetFormRulesPermissionDTO>>(
                $"api/FormRulesPermission/GetByRoleId?roleId={roleId}");
        return permissions ?? [];
    }

    public async Task SavePermissionsAsync(SaveFormRulesPermissionDTO dto)
    {
        var response = await _http.PostAsJsonAsync("api/FormRulesPermission/SavePermissions", dto);
        response.EnsureSuccessStatusCode();
    }
}
