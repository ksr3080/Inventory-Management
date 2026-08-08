using AutoMapper;
using Inventory_Management.Application.Roles.Command;
using Inventory_Management.Shared.DTOs.Roles;

namespace Inventory_Management.Application.Mappings;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<CreateRoleDTO, CreateRoleCommand>();
        CreateMap<UpdateRoleDTO, UpdateRoleCommand>();
    }
}
