using FluentValidation;

namespace HotelManagement.Application.FormBusinessRules.Command;

public class DeleteFormBusinessRulesCommandValidator : AbstractValidator<DeleteFormBusinessRulesCommand>
{
    public DeleteFormBusinessRulesCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
