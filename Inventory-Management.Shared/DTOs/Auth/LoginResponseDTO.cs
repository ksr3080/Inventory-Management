namespace Inventory_Management.Shared.DTOs.Auth;

public class LoginResponseDTO
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string EmailId { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? UserType { get; set; }

    public int? UserTypeId { get; set; }

    public int RoleId { get; set; }

    public int CompanyId { get; set; }
}
