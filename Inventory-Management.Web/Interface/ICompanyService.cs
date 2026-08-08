using Inventory_Management.Shared.DTOs.Company;

namespace Inventory_Management.Interface;

public interface ICompanyService
{
    Task CreateCompanyAsync (CreateCompanyDTO companyDTO);
    Task<IReadOnlyList<GetCompanyDTO>> GetCompanyAsync();
    Task UpdateCompanyAsync(UpdateCompanyDTO company);
    Task DeleteCompanyAsync(int id);
}
