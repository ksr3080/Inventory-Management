using Microsoft.EntityFrameworkCore;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Repositories.Interfaces;
using HotelManagement.Shared.DTOs.Users;

namespace HotelManagement.Infrastructure.Repositories.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<GetUserDTO>> GetAllByCompanyAsync(int companyId, CancellationToken cancellationToken)
    {
        var query = from u in _dbContext.Users
                    join ur in _dbContext.UsersInRole on u.Id equals ur.UserId into urGroup
                    from ur in urGroup.DefaultIfEmpty() // left join
                    where !u.IsDeleted &&
                          (companyId == 0 || ur == null || ur.CompanyId == companyId)
                    select new GetUserDTO
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        EmailId = u.EmailId,
                        ContactNo = u.ContactNo,
                        UserName = u.UserName,
                        IsLoginAllowed = u.IsLoginAllowed,
                        UserType = u.UserType,
                        IsActive = u.IsActive,
                        IsDeleted = u.IsDeleted,
                        RoleId = ur != null ? ur.RoleId : 0
                    };

        return await query
            .AsNoTracking()
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Users user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public async Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        var emailid = email.Trim().ToLowerInvariant();
        return await _dbContext.Users.AnyAsync(x => x.EmailId == emailid && !x.IsDeleted, cancellationToken);
    }

    public async Task<Users?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }

    public async Task<Users?> GetByUserNameOrEmailAsync(string identifier, CancellationToken cancellationToken)
    {
        var cleanIdentifier = identifier.Trim().ToLowerInvariant();
        return await _dbContext.Users.FirstOrDefaultAsync(
            x => (x.UserName.ToLower() == cleanIdentifier || x.EmailId.ToLower() == cleanIdentifier) && !x.IsDeleted,
            cancellationToken);
    }

    public void Update(Users users)
    {
        _dbContext.Users.Update(users);
    }

    public void Delete(Users users)
    {
        _dbContext.Users.Update(users);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
