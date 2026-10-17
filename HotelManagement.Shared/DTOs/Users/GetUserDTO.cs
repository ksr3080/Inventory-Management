namespace HotelManagement.Shared.DTOs.Users;

public class GetUserDTO
{

    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string EmailId { get; set; }

    public string? ContactNo { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsLoginAllowed { get; set; }

    public string? UserType { get; set; }

    /// <summary>Current role assigned via UsersInRole (0 if not yet assigned).</summary>
    public int RoleId { get; set; }
}
