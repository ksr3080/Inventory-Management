using MediatR;
using Microsoft.Extensions.Logging;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.Application.Forms.Command;

public class UpdateFormCommand : IRequest<bool>
{
    public int Id { get; set; }

    public string FormCode { get; set; }

    public string FormName { get; set; }

    public int ParentId { get; set; }

    public string? FormUrl { get; set; }

    public string? MenuIconClass { get; set; }

    public bool ShowInMenu { get; set; }

    public bool IsAddEdit { get; set; }

    public bool IsView { get; set; }

    public int DisplayIndex { get; set; }

    public int UpdatedBy { get; set; }
}

public class UpdateFormCommandHandler : IRequestHandler<UpdateFormCommand, bool>
{
    private readonly IFormRepository _formRepository;
    private readonly ILogger<UpdateFormCommandHandler> _logger;

    public UpdateFormCommandHandler(IFormRepository formRepository, ILogger<UpdateFormCommandHandler> logger)
    {
        _formRepository = formRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateFormCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.GetByIdAsync(request.Id, cancellationToken);

        if (form is null)
            return false;

        form.UpdateDetails(
            request.FormCode,
            request.FormName,
            request.ParentId,
            request.FormUrl,
            request.MenuIconClass,
            request.ShowInMenu,
            request.IsAddEdit,
            request.IsView,
            request.DisplayIndex,
            request.UpdatedBy);

        await _formRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Form {FormId} updated successfully.", form.Id);

        return true;
    }
}
