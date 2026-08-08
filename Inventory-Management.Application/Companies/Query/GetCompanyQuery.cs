using AutoMapper;
using MediatR;
using Inventory_Management.Infrastructure.Repositories.Interfaces;
using Inventory_Management.Shared.DTOs.Company;

namespace Inventory_Management.Application.Companies.Query;

public class GetCompanyQuery : IRequest<IReadOnlyList<GetCompanyDTO>>
{

}
public class GetCompanyHandler : IRequestHandler<GetCompanyQuery, IReadOnlyList<GetCompanyDTO>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetCompanyHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }
    public async Task<IReadOnlyList<GetCompanyDTO>> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<IReadOnlyList<GetCompanyDTO>>(companies);
    }
}
