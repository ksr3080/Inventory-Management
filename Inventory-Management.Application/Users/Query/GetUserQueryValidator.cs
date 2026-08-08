using FluentValidation;

namespace Inventory_Management.Application.Users.Query;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThanOrEqualTo(0).WithMessage("CompanyId must be greater than or equal to 0.");
    }
}
