using MediatR;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.Application.Companies.Command;

public record DeleteCompanyCommand(int Id) : IRequest;
public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;

    public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (company is null)
            throw new Exception("Company not found.");

        _companyRepository.Delete(company);

        await _companyRepository.SaveChangesAsync(cancellationToken);
    }
}