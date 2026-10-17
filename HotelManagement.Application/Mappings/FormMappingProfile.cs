using AutoMapper;
using HotelManagement.Application.Forms.Command;
using HotelManagement.Shared.DTOs.Forms;

namespace HotelManagement.Application.Mappings;

public class FormMappingProfile : Profile
{
    public FormMappingProfile()
    {
        // Input DTO → CQRS Command (used by FormController via IMapper)
        CreateMap<CreateFormDTO, CreateFormCommand>();

        CreateMap<UpdateFormDTO, UpdateFormCommand>();
    }
}
