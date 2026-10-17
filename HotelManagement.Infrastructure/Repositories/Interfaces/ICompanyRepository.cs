using HotelManagement.Domain.Entities;

namespace HotelManagement.Infrastructure.Repositories.Interfaces;

public interface ICompanyRepository
{
    Task<bool> EmailExistsAsync(string email,CancellationToken cancellationToken);
    Task<bool> GSTExistsAsync(string gstNo, CancellationToken cancellationToken);
    Task AddAsync(Company company, CancellationToken cancellationToken);
    Task<IReadOnlyList<Company>> GetAllAsync(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<Company?> GetByIdAsync(int id, CancellationToken cancellationToken);
    void Update(Company company);
    void Delete(Company company);
}
