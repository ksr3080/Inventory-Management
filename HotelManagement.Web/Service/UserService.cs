using HotelManagement.Interface;
using HotelManagement.Shared.DTOs.Users;

namespace HotelManagement.Service;

public class UserService:IUserService
{
    private readonly HttpClient _http; 
    public UserService(HttpClient http)
    {
        _http = http;
    }
    public async Task CreateUserAsync(CreateUserDTO user)
    {
        var response = await _http.PostAsJsonAsync("api/User/CreateUser", user);
        response.EnsureSuccessStatusCode();
    }

    public async Task<int> CreateUserAndGetIdAsync(CreateUserDTO user)
    {
        var response = await _http.PostAsJsonAsync("api/User/CreateUser", user);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<CreateUserResponse>();
        return body?.Id ?? 0;
    }
    public async Task<IReadOnlyList<GetUserDTO>> GetUsersAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/User/GetUsers");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("401 Unauthorized - User is not authenticated.");
                return [];
            }

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(
                    $"API Error: {response.StatusCode} - {response.ReasonPhrase}");

                return [];
            }

            var users =
                await response.Content.ReadFromJsonAsync<IReadOnlyList<GetUserDTO>>();

            return users ?? [];
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Request Error: {ex.Message}");
            return [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return [];
        }
    }
    public async Task UpdateUserAsync(UpdateUserDTO user)
    {
        HttpResponseMessage response = await _http.PutAsJsonAsync($"api/User/UpdateUser/{user.Id}", user);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteUserAsync(int userId)
    {
        HttpResponseMessage response = await _http.DeleteAsync($"api/User/DeleteUser/{userId}");

        response.EnsureSuccessStatusCode();
    }

    public async Task AssignUserRoleAsync(AssignUserRoleDTO dto)
    {
        var response = await _http.PostAsJsonAsync("api/User/AssignRole", dto);
        response.EnsureSuccessStatusCode();
    }

    // Helper for deserializing the 201 CreateUser response body
    private sealed record CreateUserResponse(int Id, string Message);
}
