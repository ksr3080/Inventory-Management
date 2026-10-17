using MediatR;
using HotelManagement.Infrastructure.Repositories.Interfaces;
using HotelManagement.Shared.DTOs.Forms;

namespace HotelManagement.Application.Forms.Query;

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
