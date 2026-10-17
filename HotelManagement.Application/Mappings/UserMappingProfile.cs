using AutoMapper;
using HotelManagement.Application.Users.Command;
using HotelManagement.Shared.DTOs.Users;

namespace HotelManagement.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // Input DTO → CQRS Command (used by UserController via IMapper)
        CreateMap<CreateUserDTO, CreateUserCommand>();

        CreateMap<UpdateUserDTO, UpdateUserCommand>();
    }
}