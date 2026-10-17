using MediatR;
using Microsoft.Extensions.Logging;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.Application.Forms.Command;

public class DeleteFormCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteFormCommandHandler : IRequestHandler<DeleteFormCommand, bool>
{
    private readonly IFormRepository _formRepository;
    private readonly ILogger<DeleteFormCommandHandler> _logger;

    public DeleteFormCommandHandler(IFormRepository formRepository, ILogger<DeleteFormCommandHandler> logger)
    {
        _formRepository = formRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteFormCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.GetByIdAsync(request.Id, cancellationToken);

        if (form is null)
            return false;

        form.Delete(0);

        _formRepository.Update(form);
        await _formRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Form {FormId} soft deleted successfully.", request.Id);

        return true;
    }
}
