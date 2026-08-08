using Microsoft.EntityFrameworkCore;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.Infrastructure.Repositories.Repository;

public class UsersInRoleRepository : IUsersInRoleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UsersInRoleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UsersInRole?> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.UsersInRole
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task<UsersInRole?> GetByUserIdTrackedAsync(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.UsersInRole
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(UsersInRole userInRole, CancellationToken cancellationToken)
    {
        await _dbContext.UsersInRole.AddAsync(userInRole, cancellationToken);
    }

    public void Remove(UsersInRole userInRole)
    {
        _dbContext.UsersInRole.Remove(userInRole);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
