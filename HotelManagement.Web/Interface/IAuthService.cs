using HotelManagement.Shared.DTOs.Auth;

namespace HotelManagement.Interface;

public interface IAuthService
{
    Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request, CancellationToken cancellationToken = default);
}
