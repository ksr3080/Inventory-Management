using FluentValidation;
using HotelManagement.Shared.DTOs.Auth;

namespace HotelManagement.Application.Auth.Validation;

public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserNameOrEmail)
            .NotEmpty().WithMessage("User name or Email is required.")
            .MaximumLength(100).WithMessage("User name or Email cannot exceed 100 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
