using HotelManagement.Shared.DTOs.Users;

namespace HotelManagement.Interface;

public interface IUserService
{
    Task CreateUserAsync(CreateUserDTO userDTO);
    Task<int> CreateUserAndGetIdAsync(CreateUserDTO userDTO);
    Task<IReadOnlyList<GetUserDTO>> GetUsersAsync();
    Task UpdateUserAsync(UpdateUserDTO user);
    Task DeleteUserAsync(int userId);
    Task AssignUserRoleAsync(AssignUserRoleDTO dto);
}
