namespace HotelManagement.Domain.Entities;

public class UsersInRole
{
    private UsersInRole() { }

    public int Id { get; private set; }

    public int RoleId { get; private set; }

    public int UserId { get; private set; }

    public int CompanyId { get; private set; }

    public static UsersInRole Create(int userId, int roleId, int companyId)
    {
        return new UsersInRole
        {
            UserId = userId,
            RoleId = roleId,
            CompanyId = companyId
        };
    }
}
