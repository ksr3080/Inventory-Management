using FluentValidation;

namespace HotelManagement.Application.FormRulesPermission.Command;

public class SaveFormRulesPermissionCommandValidator : AbstractValidator<SaveFormRulesPermissionCommand>
{
    public SaveFormRulesPermissionCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId must be greater than 0.");

        RuleFor(x => x.Permissions)
            .NotNull().WithMessage("Permissions list cannot be null.");
    }
}
