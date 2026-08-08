namespace Inventory_Management.Shared.DTOs.Roles;

public class CreateRoleDTO
{
    public string RoleName { get; set; }

    public int RoleLevel { get; set; }

    public bool IsActive { get; set; } = true;
}
