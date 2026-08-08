using FluentValidation;

namespace Inventory_Management.Application.Roles.Command;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("RoleName is required.")
            .MaximumLength(100).WithMessage("RoleName cannot exceed 100 characters.");

        RuleFor(x => x.RoleLevel)
            .GreaterThanOrEqualTo(0).WithMessage("RoleLevel must be greater than or equal to 0.");
    }
}
