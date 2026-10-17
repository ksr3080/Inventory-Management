using MediatR;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.Application.Companies.Command;

public class CreateCompanyCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNo { get; set; }
    public string Address1 { get; set; }
    public string? Address2 { get; set; }
    public string State { get; set; }
    public string StateCode { get; set; }
    public string City { get; set; }
    public string Pincode { get; set; }
    public string GSTNo { get; set; }
    public string? Logo { get; set; }
    public int CreatedBy { get; set; }
}
public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
{
    private readonly ICompanyRepository _companyRepository;

    public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        string normalizedEmail = request.Email.Trim().ToLowerInvariant();

        bool emailExists = await _companyRepository.EmailExistsAsync(normalizedEmail, cancellationToken);

        if (emailExists)
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        var company = new Company(request.Name, normalizedEmail, request.PhoneNo, request.Address1, request.Address2, request.State, request.StateCode, request.City, request.Pincode, request.GSTNo, request.Logo, request.CreatedBy);

        await _companyRepository.AddAsync(company, cancellationToken);
        await _companyRepository.SaveChangesAsync(cancellationToken);

        return company.Id;
    }
}