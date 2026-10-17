using AutoMapper;
using HotelManagement.Application.Customers.Command;
using HotelManagement.Domain.Entities;
using HotelManagement.Shared.DTOs.Customer;

namespace HotelManagement.Application.Mappings;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, GetCustomerDTO>();

        CreateMap<CreateCustomerDTO, CreateCustomerCommand>();

        CreateMap<UpdateCustomerDTO, UpdateCustomerCommand>();
    }
}
