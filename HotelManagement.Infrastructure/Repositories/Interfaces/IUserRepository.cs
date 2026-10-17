using HotelManagement.Domain.Entities;
using HotelManagement.Shared.DTOs.Users;

namespace HotelManagement.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IReadOnlyList<GetUserDTO>> GetAllByCompanyAsync(int companyId, CancellationToken cancellationToken);

    Task AddAsync(Users user, CancellationToken cancellationToken);

    Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken);

    Task<Users?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<Users?> GetByUserNameOrEmailAsync(string identifier, CancellationToken cancellationToken);

    void Update(Users users);

    void Delete(Users users);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
