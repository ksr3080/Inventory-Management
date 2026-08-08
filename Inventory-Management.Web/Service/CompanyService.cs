using Inventory_Management.Interface;
using Inventory_Management.Shared.DTOs.Company;
using Inventory_Management.Shared.DTOs.Users;
using static System.Net.WebRequestMethods;

namespace Inventory_Management.Service;

public class CompanyService:ICompanyService
{
    private readonly HttpClient _http;
    public CompanyService(HttpClient http) { _http = http; }
    public async Task CreateCompanyAsync(CreateCompanyDTO user)
    {
        var response = await _http.PostAsJsonAsync("api/company/create", user);
        response.EnsureSuccessStatusCode();
    }
    public async Task<IReadOnlyList<GetCompanyDTO>> GetCompanyAsync()
    {
        IReadOnlyList<GetCompanyDTO>? companies = await _http.GetFromJsonAsync<IReadOnlyList<GetCompanyDTO>>("api/company/get");

        return companies ?? [];
    }
    public async Task UpdateCompanyAsync(UpdateCompanyDTO company)
    {
        var response = await _http.PutAsJsonAsync(
            $"api/company/update/{company.Id}", company);

        response.EnsureSuccessStatusCode();
    }
    public async Task DeleteCompanyAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/company/delete/{id}");

        response.EnsureSuccessStatusCode();
    }
}
