using FluentValidation;

namespace HotelManagement.Application.FormBusinessRules.Command;

public class UpdateFormBusinessRulesCommandValidator : AbstractValidator<UpdateFormBusinessRulesCommand>
{
    public UpdateFormBusinessRulesCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.FormId)
            .GreaterThan(0).WithMessage("FormId must be greater than 0.");

        RuleFor(x => x.Rules)
            .MaximumLength(100).WithMessage("Rules cannot exceed 100 characters.");

        RuleFor(x => x.RulesCode)
            .MaximumLength(100).WithMessage("Rules Code cannot exceed 100 characters.");

        RuleFor(x => x.UpdatedBy)
            .GreaterThan(0).WithMessage("UpdatedBy user id is required.");
    }
}
