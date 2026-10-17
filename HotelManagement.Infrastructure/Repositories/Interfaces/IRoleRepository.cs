using HotelManagement.Domain.Entities;
using HotelManagement.Shared.DTOs.Roles;

namespace HotelManagement.Infrastructure.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<IReadOnlyList<GetRoleDTO>> GetAllAsync(CancellationToken cancellationToken);

    Task AddAsync(Role role, CancellationToken cancellationToken);

    Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken);

    void Update(Role role);

    void Delete(Role role);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
