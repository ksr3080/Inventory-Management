using AutoMapper;
using HotelManagement.Application.FormBusinessRules.Command;
using HotelManagement.Shared.DTOs.FormBusinessRules;

namespace HotelManagement.Application.Mappings;

public class FormBusinessRulesMappingProfile : Profile
{
    public FormBusinessRulesMappingProfile()
    {
        CreateMap<CreateFormBusinessRulesDTO, CreateFormBusinessRulesCommand>();
        CreateMap<UpdateFormBusinessRulesDTO, UpdateFormBusinessRulesCommand>();
    }
}
