using Inventory_Management.Domain.Entities;
using Inventory_Management.Domain.StoreProcedureResponse;
using Inventory_Management.Shared.DTOs.Forms;

namespace Inventory_Management.Infrastructure.Repositories.Interfaces;

public interface IFormRepository
{
    Task<IReadOnlyList<GetFormDTO>> GetAllAsync(CancellationToken cancellationToken);

    Task AddAsync(Forms form, CancellationToken cancellationToken);

    Task<Forms?> GetByIdAsync(int id, CancellationToken cancellationToken);

    void Update(Forms form);

    void Delete(Forms form);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    List<SP_GetFormBreadCum> GetFormBreadCum();
}

