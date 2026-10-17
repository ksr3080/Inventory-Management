namespace HotelManagement.Shared.DTOs.Auth;

public class LoginRequestDTO
{
    public string UserNameOrEmail { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
