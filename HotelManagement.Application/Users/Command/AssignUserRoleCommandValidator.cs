using FluentValidation;

namespace HotelManagement.Application.Users.Command;

public class AssignUserRoleCommandValidator : AbstractValidator<AssignUserRoleCommand>
{
    public AssignUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0.");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId must be greater than 0.");

        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");
    }
}
