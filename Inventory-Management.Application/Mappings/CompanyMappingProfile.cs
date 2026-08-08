using AutoMapper;
using Inventory_Management.Application.Companies.Command;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Shared.DTOs.Company;
namespace Inventory_Management.Application.Mappings;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        // Entity ? Output DTO
        CreateMap<Company, GetCompanyDTO>();
        // Input DTO ? CQRS Command
        CreateMap<CreateCompanyDTO, CreateCompanyCommand>();

        CreateMap<UpdateCompanyDTO, UpdateCompanyCommand>();
    }
}
