using MediatR;
using Inventory_Management.Infrastructure.Repositories.Interfaces;
using Inventory_Management.Shared.DTOs.Forms;

namespace Inventory_Management.Application.Forms.Query;

public class GetFormQuery : IRequest<IReadOnlyList<GetFormDTO>>
{
    public GetFormQuery()
    {
    }
}

public class GetFormQueryHandler : IRequestHandler<GetFormQuery, IReadOnlyList<GetFormDTO>>
{
    private readonly IFormRepository _formRepository;

    public GetFormQueryHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }

    public async Task<IReadOnlyList<GetFormDTO>> Handle(GetFormQuery request, CancellationToken cancellationToken)
    {
        return await _formRepository.GetAllAsync(cancellationToken);
    }
}
