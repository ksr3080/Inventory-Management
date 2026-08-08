namespace Inventory_Management.Shared.DTOs.Users;

public class AssignUserRoleDTO
{
    /// <summary>The user whose role is being assigned.</summary>
    public int UserId { get; set; }

    /// <summary>The role to assign to the user.</summary>
    public int RoleId { get; set; }

    /// <summary>The company under which the assignment is scoped.</summary>
    public int CompanyId { get; set; }
}
