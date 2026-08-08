using System.Net.Http.Json;
using Inventory_Management.Interface;
using Inventory_Management.Shared.DTOs.Auth;

namespace Inventory_Management.Service;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request, CancellationToken cancellationToken = default)
    {
        var response = await _http.PostAsJsonAsync("api/Auth/login", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<LoginResponseDTO>(cancellationToken: cancellationToken);
    }
}
