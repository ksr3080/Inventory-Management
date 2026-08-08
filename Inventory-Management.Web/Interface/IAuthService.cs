using Inventory_Management.Shared.DTOs.Auth;

namespace Inventory_Management.Interface;

public interface IAuthService
{
    Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request, CancellationToken cancellationToken = default);
}
