using AutoMapper;
using Inventory_Management.Application.FormBusinessRules.Command;
using Inventory_Management.Shared.DTOs.FormBusinessRules;

namespace Inventory_Management.Application.Mappings;

public class FormBusinessRulesMappingProfile : Profile
{
    public FormBusinessRulesMappingProfile()
    {
        CreateMap<CreateFormBusinessRulesDTO, CreateFormBusinessRulesCommand>();
        CreateMap<UpdateFormBusinessRulesDTO, UpdateFormBusinessRulesCommand>();
    }
}
