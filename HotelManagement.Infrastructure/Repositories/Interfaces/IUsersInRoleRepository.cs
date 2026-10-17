using HotelManagement.Domain.Entities;

namespace HotelManagement.Infrastructure.Repositories.Interfaces;

public interface IUsersInRoleRepository
{
    /// <summary>Returns the existing UsersInRole record for a user (or null).</summary>
    Task<UsersInRole?> GetByUserIdAsync(int userId, CancellationToken cancellationToken);

    /// <summary>Returns the tracked entity for safe removal.</summary>
    Task<UsersInRole?> GetByUserIdTrackedAsync(int userId, CancellationToken cancellationToken);

    /// <summary>Inserts a new UsersInRole record.</summary>
    Task AddAsync(UsersInRole userInRole, CancellationToken cancellationToken);

    /// <summary>Removes an existing record.</summary>
    void Remove(UsersInRole userInRole);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
