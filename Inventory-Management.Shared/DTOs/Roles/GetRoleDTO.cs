namespace Inventory_Management.Shared.DTOs.Roles;

public class GetRoleDTO
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public int? RoleLevel { get; set; }

    public bool IsActive { get; set; }
}
