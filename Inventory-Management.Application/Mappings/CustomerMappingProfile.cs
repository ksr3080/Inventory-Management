using AutoMapper;
using Inventory_Management.Application.Customers.Command;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Shared.DTOs.Customer;

namespace Inventory_Management.Application.Mappings;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, GetCustomerDTO>();

        CreateMap<CreateCustomerDTO, CreateCustomerCommand>();

        CreateMap<UpdateCustomerDTO, UpdateCustomerCommand>();
    }
}
