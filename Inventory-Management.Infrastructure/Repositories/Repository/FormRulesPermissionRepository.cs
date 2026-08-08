using Microsoft.EntityFrameworkCore;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Repositories.Interfaces;
using Inventory_Management.Shared.DTOs.FormRulesPermission;

namespace Inventory_Management.Infrastructure.Repositories.Repository;

public class FormRulesPermissionRepository : IFormRulesPermissionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FormRulesPermissionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<GetFormRulesPermissionDTO>> GetByRoleIdAsync(int roleId, CancellationToken cancellationToken)
    {
        return await _dbContext.FormRulesPermission
            .AsNoTracking()
            .Where(x => x.RoleId == roleId && !x.IsDeleted && x.IsActive)
            .Select(x => new GetFormRulesPermissionDTO
            {
                Id = x.Id,
                FormRulesId = x.FormRulesId,
                FormId = x.FormId,
                RoleId = x.RoleId,
                IsAllowRules = x.IsAllowRules
            })
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteByRoleIdAsync(int roleId, CancellationToken cancellationToken)
    {
        var existing = await _dbContext.FormRulesPermission
            .Where(x => x.RoleId == roleId && !x.IsDeleted)
            .ToListAsync(cancellationToken);

        if (existing.Any())
        {
            _dbContext.FormRulesPermission.RemoveRange(existing);
        }
    }

    public async Task AddRangeAsync(List<FormRulesPermission> permissions, CancellationToken cancellationToken)
    {
        await _dbContext.FormRulesPermission.AddRangeAsync(permissions, cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
