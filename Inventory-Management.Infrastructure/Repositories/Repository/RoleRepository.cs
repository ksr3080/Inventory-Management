using Microsoft.EntityFrameworkCore;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Repositories.Interfaces;
using Inventory_Management.Shared.DTOs.Roles;

namespace Inventory_Management.Infrastructure.Repositories.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<GetRoleDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Role
            .AsNoTracking()
            .Select(r => new GetRoleDTO
            {
                Id = r.Id,
                RoleName = r.RoleName,
                RoleLevel = r.RoleLevel,
                IsActive = r.IsActive
            })
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Role role, CancellationToken cancellationToken)
    {
        await _dbContext.Role.AddAsync(role, cancellationToken);
    }

    public async Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Role.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Update(Role role)
    {
        _dbContext.Role.Update(role);
    }

    public void Delete(Role role)
    {
        _dbContext.Role.Remove(role);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
