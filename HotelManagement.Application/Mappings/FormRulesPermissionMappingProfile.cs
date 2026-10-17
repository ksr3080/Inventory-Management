using AutoMapper;
using HotelManagement.Application.FormRulesPermission.Command;
using HotelManagement.Shared.DTOs.FormRulesPermission;

namespace HotelManagement.Application.Mappings;

public class FormRulesPermissionMappingProfile : Profile
{
    public FormRulesPermissionMappingProfile()
    {
        CreateMap<SaveFormRulesPermissionDTO, SaveFormRulesPermissionCommand>();
        CreateMap<SavePermissionItemDTO, SavePermissionItem>();
    }
}
