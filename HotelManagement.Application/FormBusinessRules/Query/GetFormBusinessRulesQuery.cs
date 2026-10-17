using MediatR;
using HotelManagement.Infrastructure.Repositories.Interfaces;
using HotelManagement.Shared.DTOs.FormBusinessRules;

namespace HotelManagement.Application.FormBusinessRules.Query;

public class GetFormBusinessRulesQuery : IRequest<IReadOnlyList<GetFormBusinessRulesDTO>>
{
    public int FormId { get; init; }

    public GetFormBusinessRulesQuery(int formId = 0)
    {
        FormId = formId;
    }
}

public class GetFormBusinessRulesQueryHandler : IRequestHandler<GetFormBusinessRulesQuery, IReadOnlyList<GetFormBusinessRulesDTO>>
{
    private readonly IFormBusinessRulesRepository _repository;

    public GetFormBusinessRulesQueryHandler(IFormBusinessRulesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<GetFormBusinessRulesDTO>> Handle(GetFormBusinessRulesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(request.FormId, cancellationToken);
    }
}
