using FluentValidation;

namespace HotelManagement.Application.FormRulesPermission.Query;

public class GetFormRulesPermissionQueryValidator : AbstractValidator<GetFormRulesPermissionQuery>
{
    public GetFormRulesPermissionQueryValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId must be greater than 0.");
    }
}
