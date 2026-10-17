using FluentValidation;

namespace HotelManagement.Application.Forms.Command;

public class CreateFormCommandValidator : AbstractValidator<CreateFormCommand>
{
    public CreateFormCommandValidator()
    {
        RuleFor(x => x.FormCode)
            .NotEmpty().WithMessage("FormCode is required.")
            .MaximumLength(100).WithMessage("FormCode cannot exceed 100 characters.");

        RuleFor(x => x.FormName)
            .NotEmpty().WithMessage("FormName is required.")
            .MaximumLength(100).WithMessage("FormName cannot exceed 100 characters.");

        RuleFor(x => x.ParentId)
            .GreaterThanOrEqualTo(0).WithMessage("ParentId must be greater than or equal to 0.");

        RuleFor(x => x.FormUrl)
            .MaximumLength(256).WithMessage("FormUrl cannot exceed 256 characters.");

        RuleFor(x => x.MenuIconClass)
            .MaximumLength(100).WithMessage("MenuIconClass cannot exceed 100 characters.");

        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).WithMessage("CreatedBy user id is required.");
    }
}
