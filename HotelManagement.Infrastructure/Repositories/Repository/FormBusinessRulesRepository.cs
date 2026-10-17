using Microsoft.EntityFrameworkCore;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Repositories.Interfaces;
using HotelManagement.Shared.DTOs.FormBusinessRules;

namespace HotelManagement.Infrastructure.Repositories.Repository;

public class FormBusinessRulesRepository : IFormBusinessRulesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FormBusinessRulesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<GetFormBusinessRulesDTO>> GetAllAsync(int formId, CancellationToken cancellationToken)
    {
        var query = _dbContext.FormBusinessRules
            .AsNoTracking()
            .Where(x => !x.IsDeleted && (formId == 0 || x.FormId == formId))
            .Select(x => new GetFormBusinessRulesDTO
            {
                Id = x.Id,
                FormId = x.FormId,
                Rules = x.Rules,
                RulesCode = x.RulesCode,
                RulesValue = x.RulesValue,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted
            });

        return await query.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FormBusinessRules rule, CancellationToken cancellationToken)
    {
        await _dbContext.FormBusinessRules.AddAsync(rule, cancellationToken);
    }

    public async Task<FormBusinessRules?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.FormBusinessRules.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }

    public void Update(FormBusinessRules rule)
    {
        _dbContext.FormBusinessRules.Update(rule);
    }

    public void Delete(FormBusinessRules rule)
    {
        _dbContext.FormBusinessRules.Update(rule);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
