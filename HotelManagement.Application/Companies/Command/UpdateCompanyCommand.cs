using MediatR;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.Application.Companies.Command;

public class UpdateCompanyCommand : IRequest
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public string Address1 { get; set; } = string.Empty;
    public string? Address2 { get; set; }
    public string State { get; set; } = string.Empty;
    public string StateCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
    public string GSTNo { get; set; } = string.Empty;
    public string? Logo { get; set; }
    public int UpdatedBy { get; set; }
}
public class UpdateCompanyCommandHandler
    : IRequestHandler<UpdateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;

    public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (company is null)
            throw new Exception("Company not found.");

        company.UpdateDetails(request.Name, request.Email, request.PhoneNo, request.Address1, request.Address2, request.State, request.StateCode,
            request.City, request.Pincode, request.GSTNo, request.Logo, request.UpdatedBy);

        _companyRepository.Update(company);

        await _companyRepository.SaveChangesAsync(cancellationToken);
    }
}
