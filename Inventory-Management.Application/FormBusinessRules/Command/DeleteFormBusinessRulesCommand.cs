using MediatR;
using Microsoft.Extensions.Logging;
using Inventory_Management.Infrastructure.Repositories.Interfaces;

namespace Inventory_Management.Application.FormBusinessRules.Command;

public class DeleteFormBusinessRulesCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteFormBusinessRulesCommandHandler : IRequestHandler<DeleteFormBusinessRulesCommand, bool>
{
    private readonly IFormBusinessRulesRepository _repository;
    private readonly ILogger<DeleteFormBusinessRulesCommandHandler> _logger;

    public DeleteFormBusinessRulesCommandHandler(
        IFormBusinessRulesRepository repository,
        ILogger<DeleteFormBusinessRulesCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteFormBusinessRulesCommand request, CancellationToken cancellationToken)
    {
        var rule = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (rule is null)
        {
            return false;
        }

        rule.Delete(0);

        _repository.Delete(rule);
        await _repository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("FormBusinessRules {RuleId} soft deleted successfully.", request.Id);

        return true;
    }
}
