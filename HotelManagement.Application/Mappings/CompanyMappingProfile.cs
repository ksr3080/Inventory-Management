using AutoMapper;
using HotelManagement.Application.Companies.Command;
using HotelManagement.Domain.Entities;
using HotelManagement.Shared.DTOs.Company;
namespace HotelManagement.Application.Mappings;

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
