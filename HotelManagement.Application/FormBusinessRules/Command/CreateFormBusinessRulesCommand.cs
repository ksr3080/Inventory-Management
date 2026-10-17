using MediatR;
using Microsoft.Extensions.Logging;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.Application.FormBusinessRules.Command;

public class CreateFormBusinessRulesCommand : IRequest<int>
{
    public int FormId { get; set; }

    public string? Rules { get; set; }

    public string? RulesCode { get; set; }

    public bool RulesValue { get; set; }

    public int CreatedBy { get; set; }
}

public class CreateFormBusinessRulesCommandHandler : IRequestHandler<CreateFormBusinessRulesCommand, int>
{
    private readonly IFormBusinessRulesRepository _repository;
    private readonly ILogger<CreateFormBusinessRulesCommandHandler> _logger;

    public CreateFormBusinessRulesCommandHandler(
        IFormBusinessRulesRepository repository,
        ILogger<CreateFormBusinessRulesCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateFormBusinessRulesCommand request, CancellationToken cancellationToken)
    {
        var rule = new Domain.Entities.FormBusinessRules(
            request.FormId,
            request.Rules,
            request.RulesCode,
            request.RulesValue,
            request.CreatedBy);

        await _repository.AddAsync(rule, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("FormBusinessRules {RuleId} created successfully.", rule.Id);

        return rule.Id;
    }
}
