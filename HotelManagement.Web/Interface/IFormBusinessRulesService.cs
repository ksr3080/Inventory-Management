using HotelManagement.Shared.DTOs.FormBusinessRules;

namespace HotelManagement.Interface;

public interface IFormBusinessRulesService
{
    Task<IReadOnlyList<GetFormBusinessRulesDTO>> GetFormBusinessRulesAsync(int formId = 0);
    Task CreateFormBusinessRuleAsync(CreateFormBusinessRulesDTO ruleDTO);
    Task UpdateFormBusinessRuleAsync(UpdateFormBusinessRulesDTO ruleDTO);
    Task DeleteFormBusinessRuleAsync(int ruleId);
}
