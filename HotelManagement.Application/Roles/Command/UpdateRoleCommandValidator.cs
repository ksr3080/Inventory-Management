using FluentValidation;

namespace HotelManagement.Application.Roles.Command;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("RoleName is required.")
            .MaximumLength(100).WithMessage("RoleName cannot exceed 100 characters.");

        RuleFor(x => x.RoleLevel)
            .GreaterThanOrEqualTo(0).WithMessage("RoleLevel must be greater than or equal to 0.");
    }
}
