using HotelManagement.Interface;
using HotelManagement.Shared.DTOs.FormBusinessRules;

namespace HotelManagement.Service;

public class FormBusinessRulesService : IFormBusinessRulesService
{
    private readonly HttpClient _http;

    public FormBusinessRulesService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<GetFormBusinessRulesDTO>> GetFormBusinessRulesAsync(int formId = 0)
    {
        var url = formId > 0
            ? $"api/FormBusinessRules/GetFormBusinessRules?formId={formId}"
            : "api/FormBusinessRules/GetFormBusinessRules";

        IReadOnlyList<GetFormBusinessRulesDTO>? rules = await _http.GetFromJsonAsync<IReadOnlyList<GetFormBusinessRulesDTO>>(url);
        return rules ?? [];
    }

    public async Task CreateFormBusinessRuleAsync(CreateFormBusinessRulesDTO ruleDTO)
    {
        var response = await _http.PostAsJsonAsync("api/FormBusinessRules/CreateFormBusinessRule", ruleDTO);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateFormBusinessRuleAsync(UpdateFormBusinessRulesDTO ruleDTO)
    {
        var response = await _http.PutAsJsonAsync($"api/FormBusinessRules/UpdateFormBusinessRule/{ruleDTO.Id}", ruleDTO);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteFormBusinessRuleAsync(int ruleId)
    {
        var response = await _http.DeleteAsync($"api/FormBusinessRules/DeleteFormBusinessRule/{ruleId}");
        response.EnsureSuccessStatusCode();
    }
}
