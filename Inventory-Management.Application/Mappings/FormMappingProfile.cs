using AutoMapper;
using Inventory_Management.Application.Forms.Command;
using Inventory_Management.Shared.DTOs.Forms;

namespace Inventory_Management.Application.Mappings;

public class FormMappingProfile : Profile
{
    public FormMappingProfile()
    {
        // Input DTO → CQRS Command (used by FormController via IMapper)
        CreateMap<CreateFormDTO, CreateFormCommand>();

        CreateMap<UpdateFormDTO, UpdateFormCommand>();
    }
}
