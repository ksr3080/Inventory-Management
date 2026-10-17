using HotelManagement.Domain.Entities;
using HotelManagement.Shared.DTOs.FormBusinessRules;

namespace HotelManagement.Infrastructure.Repositories.Interfaces;

public interface IFormBusinessRulesRepository
{
    Task<IReadOnlyList<GetFormBusinessRulesDTO>> GetAllAsync(int formId, CancellationToken cancellationToken);

    Task AddAsync(FormBusinessRules rule, CancellationToken cancellationToken);

    Task<FormBusinessRules?> GetByIdAsync(int id, CancellationToken cancellationToken);

    void Update(FormBusinessRules rule);

    void Delete(FormBusinessRules rule);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
