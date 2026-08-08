using AutoMapper;
using Inventory_Management.Application.Users.Command;
using Inventory_Management.Shared.DTOs.Users;

namespace Inventory_Management.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // Input DTO → CQRS Command (used by UserController via IMapper)
        CreateMap<CreateUserDTO, CreateUserCommand>();

        CreateMap<UpdateUserDTO, UpdateUserCommand>();
    }
}