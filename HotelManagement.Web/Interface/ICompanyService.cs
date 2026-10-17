using HotelManagement.Shared.DTOs.Company;

namespace HotelManagement.Interface;

public interface ICompanyService
{
    Task CreateCompanyAsync (CreateCompanyDTO companyDTO);
    Task<IReadOnlyList<GetCompanyDTO>> GetCompanyAsync();
    Task UpdateCompanyAsync(UpdateCompanyDTO company);
    Task DeleteCompanyAsync(int id);
}
