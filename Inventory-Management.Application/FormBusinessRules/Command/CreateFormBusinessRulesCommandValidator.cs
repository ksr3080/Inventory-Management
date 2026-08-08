using FluentValidation;

namespace Inventory_Management.Application.FormBusinessRules.Command;

public class CreateFormBusinessRulesCommandValidator : AbstractValidator<CreateFormBusinessRulesCommand>
{
    public CreateFormBusinessRulesCommandValidator()
    {
        RuleFor(x => x.FormId)
            .GreaterThan(0).WithMessage("FormId must be greater than 0.");

        RuleFor(x => x.Rules)
            .MaximumLength(100).WithMessage("Rules cannot exceed 100 characters.");

        RuleFor(x => x.RulesCode)
            .MaximumLength(100).WithMessage("Rules Code cannot exceed 100 characters.");

        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).WithMessage("CreatedBy user id is required.");
    }
}
