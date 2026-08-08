using Microsoft.EntityFrameworkCore;
using System.Data;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Domain.StoreProcedureResponse;
using Inventory_Management.Infrastructure.Repositories.Interfaces;
using Inventory_Management.Shared.DTOs.Forms;

namespace Inventory_Management.Infrastructure.Repositories.Repository;

public class FormRepository : IFormRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FormRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<GetFormDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = from f in _dbContext.Forms
                    .Where(f => !f.IsDeleted)
                    select new GetFormDTO
                    {
                        Id = f.Id,
                        FormCode = f.FormCode,
                        FormName = f.FormName,
                        ParentId = f.ParentId,
                        FormUrl = f.FormUrl,
                        MenuIconClass = f.MenuIconClass,
                        ShowInMenu = f.ShowInMenu,
                        IsAddEdit = f.IsAddEdit,
                        IsView = f.IsView,
                        DisplayIndex = f.DisplayIndex,
                        IsActive = f.IsActive,
                        IsDeleted = f.IsDeleted,
                    };

        return await query
            .AsNoTracking()
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Forms form, CancellationToken cancellationToken)
    {
        await _dbContext.Forms.AddAsync(form, cancellationToken);
    }

    public async Task<Forms?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Forms.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }

    public void Update(Forms form)
    {
        _dbContext.Forms.Update(form);
    }

    public void Delete(Forms form)
    {
        _dbContext.Forms.Update(form);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public List<SP_GetFormBreadCum> GetFormBreadCum()
    {
        var getdata = _dbContext.SP_GetFormBreadCum.FromSqlRaw("exec [dbo].[SP_FormBreadCum]").ToList();
        return getdata;
    }
}

