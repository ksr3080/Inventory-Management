using MediatR;
using Microsoft.Extensions.Logging;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.Application.Forms.Command;

public class CreateFormCommand : IRequest<int>
{
    public string FormCode { get; set; }

    public string FormName { get; set; }

    public int ParentId { get; set; }

    public string? FormUrl { get; set; }

    public string? MenuIconClass { get; set; }

    public bool ShowInMenu { get; set; }

    public bool IsAddEdit { get; set; }

    public bool IsView { get; set; }

    public int DisplayIndex { get; set; }

    public int CreatedBy { get; set; }
}

public class CreateFormCommandHandler : IRequestHandler<CreateFormCommand, int>
{
    private readonly IFormRepository _formRepository;
    private readonly ILogger<CreateFormCommandHandler> _logger;

    public CreateFormCommandHandler(IFormRepository formRepository, ILogger<CreateFormCommandHandler> logger)
    {
        _formRepository = formRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateFormCommand request, CancellationToken cancellationToken)
    {
        var form = new Domain.Entities.Forms(
            request.FormCode,
            request.FormName,
            request.ParentId,
            request.FormUrl,
            request.MenuIconClass,
            request.ShowInMenu,
            request.IsAddEdit,
            request.IsView,
            request.DisplayIndex,
            request.CreatedBy);

        await _formRepository.AddAsync(form, cancellationToken);
        await _formRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Form {FormId} created successfully.", form.Id);

        return form.Id;
    }
}
