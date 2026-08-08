namespace Inventory_Management.Shared.DTOs.Roles;

public class UpdateRoleDTO
{
    public int Id { get; set; }

    public string RoleName { get; set; }

    public int RoleLevel { get; set; }

    public bool IsActive { get; set; }
}
