using AutoMapper;
using Inventory_Management.Application.FormRulesPermission.Command;
using Inventory_Management.Shared.DTOs.FormRulesPermission;

namespace Inventory_Management.Application.Mappings;

public class FormRulesPermissionMappingProfile : Profile
{
    public FormRulesPermissionMappingProfile()
    {
        CreateMap<SaveFormRulesPermissionDTO, SaveFormRulesPermissionCommand>();
        CreateMap<SavePermissionItemDTO, SavePermissionItem>();
    }
}
