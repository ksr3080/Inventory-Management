using MediatR;
using Microsoft.Extensions.Logging;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.Application.FormBusinessRules.Command;

public class UpdateFormBusinessRulesCommand : IRequest<bool>
{
    public int Id { get; set; }

    public int FormId { get; set; }

    public string? Rules { get; set; }

    public string? RulesCode { get; set; }

    public bool RulesValue { get; set; }

    public int UpdatedBy { get; set; }
}

public class UpdateFormBusinessRulesCommandHandler : IRequestHandler<UpdateFormBusinessRulesCommand, bool>
{
    private readonly IFormBusinessRulesRepository _repository;
    private readonly ILogger<UpdateFormBusinessRulesCommandHandler> _logger;

    public UpdateFormBusinessRulesCommandHandler(
        IFormBusinessRulesRepository repository,
        ILogger<UpdateFormBusinessRulesCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateFormBusinessRulesCommand request, CancellationToken cancellationToken)
    {
        var rule = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (rule is null)
        {
            return false;
        }

        rule.Update(
            request.FormId,
            request.Rules,
            request.RulesCode,
            request.RulesValue,
            request.UpdatedBy);

        _repository.Update(rule);
        await _repository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("FormBusinessRules {RuleId} updated successfully.", rule.Id);

        return true;
    }
}
