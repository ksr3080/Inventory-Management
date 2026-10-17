using MediatR;
using HotelManagement.Infrastructure.Repositories.Interfaces;
using HotelManagement.Shared.DTOs.Forms;

namespace HotelManagement.Application.Forms.Query;

public class GetFormBreadCumQuery : IRequest<IReadOnlyList<FormBreadCumDTO>>
{
}

public class GetFormBreadCumQueryHandler : IRequestHandler<GetFormBreadCumQuery, IReadOnlyList<FormBreadCumDTO>>
{
    private readonly IFormRepository _formRepository;

    public GetFormBreadCumQueryHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }

    public Task<IReadOnlyList<FormBreadCumDTO>> Handle(GetFormBreadCumQuery request, CancellationToken cancellationToken)
    {
        var data = _formRepository.GetFormBreadCum();
        IReadOnlyList<FormBreadCumDTO> dtos = data.Select(x => new FormBreadCumDTO
        {
            FormId = x.FormId,
            FormName = x.FormName,
            FormCode = x.FormCode,
            ParentId = x.ParentId,
            BreadCum = x.BreadCum,
            FormUrl = x.FormUrl,
            DisplayIndex = x.DisplayIndex
        }).ToList();

        return Task.FromResult(dtos);
    }
}

