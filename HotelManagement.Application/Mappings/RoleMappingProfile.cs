using AutoMapper;
using HotelManagement.Application.Roles.Command;
using HotelManagement.Shared.DTOs.Roles;

namespace HotelManagement.Application.Mappings;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<CreateRoleDTO, CreateRoleCommand>();
        CreateMap<UpdateRoleDTO, UpdateRoleCommand>();
    }
}
