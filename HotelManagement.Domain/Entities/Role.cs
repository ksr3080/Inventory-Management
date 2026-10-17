namespace HotelManagement.Domain.Entities;

public class Role
{
    private Role()
    {
    }

    public Role(string roleName, int roleLevel, bool isActive = true)
    {
        SetRoleName(roleName);
        SetRoleLevel(roleLevel);
        SetIsActive(isActive);
    }

    public int Id { get; private set; }

    public string? RoleName { get; private set; }

    public int? RoleLevel { get; private set; }

    public bool IsActive { get; private set; }

    private void SetRoleName(string roleName)
    {
        RoleName = roleName?.Trim();
    }

    private void SetRoleLevel(int roleLevel)
    {
        RoleLevel = roleLevel;
    }

    private void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }

    public void Update(string roleName, int roleLevel, bool isActive)
    {
        SetRoleName(roleName);
        SetRoleLevel(roleLevel);
        SetIsActive(isActive);
    }
}