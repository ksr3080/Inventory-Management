using Microsoft.EntityFrameworkCore;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.Infrastructure.Repositories.Repository;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CompanyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Company company, CancellationToken cancellationToken)
    {
        await _dbContext.Companies.AddAsync(company, cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Companies.AnyAsync(Company => Company.Email == email, cancellationToken);
    }

    public async Task<IReadOnlyList<Company>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Companies
                   .AsNoTracking()
                   .Where(Company => !Company.IsDeleted)
                   .OrderByDescending(Company => Company.CreatedOn)
                   .ToListAsync(cancellationToken);
    }

    public async Task<bool> GSTExistsAsync(string gstNo, CancellationToken cancellationToken)
    {
        return await _dbContext.Companies.AnyAsync(company => company.GSTNo == gstNo, cancellationToken);
    }
    public async Task<Company?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Companies
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }

    public void Update(Company company)
    {
        _dbContext.Companies.Update(company);
    }

    public void Delete(Company company)
    {
        //company.SoftDelete();
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
