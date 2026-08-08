using FluentValidation;

namespace Inventory_Management.Application.FormBusinessRules.Query;

public class GetFormBusinessRulesQueryValidator : AbstractValidator<GetFormBusinessRulesQuery>
{
    public GetFormBusinessRulesQueryValidator()
    {
        RuleFor(x => x.FormId)
            .GreaterThanOrEqualTo(0).WithMessage("FormId must be greater than or equal to 0.");
    }
}
